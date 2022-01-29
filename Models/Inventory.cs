using System;
using System.Collections.Generic;
using System.Linq;
using Models.Entities;
using Models.Items;
using Models.Weapons;

namespace Models
{
    public class Inventory {

        public static Inventory Empty => new(Array.Empty<InventoryItem>());

        public Weapon ActiveWeapon { get; set; }
        private List<InventoryItem> _items;

        public Inventory(IEnumerable<InventoryItem> items)
        {
            _items = items.ToList();
        }

        private bool IsWeaponRelevantForChange(Weapon weapon, Entity user)
        {
            return !weapon.Equals(ActiveWeapon) && weapon.CanUsed(user);
        }

        public IEnumerable<Weapon> GetWeaponsForChange(Entity user)
        {
            return GetAll<Weapon>().Where(w => IsWeaponRelevantForChange(w, user));
        }

        public Weapon GetWeaponForAutoChange(Entity user)
        {
            return GetAll<Weapon>().First(w => IsWeaponRelevantForChange(w, user));
        }

        public void UseItem(UsableItem item, Entity target)
        {
            if (!Has(item))
                throw new InvalidOperationException($"Specified item {item} must be in inventory for use");
            item.Use(target);
            _items.Remove(item);
        }

        public void UseItem<T>(Entity target) where T : UsableItem
        {
            GetOne<T>().Use(target);
        }

        public bool Has(InventoryItem item)
        {
            return _items.Contains(item);
        }

        public bool Has<T>(byte count = 1) where T : InventoryItem
        {
            return GetAll<T>().Count() >= count;
        }

        public T GetOne<T>() where T : InventoryItem
        {
            var items = GetAll<T>();
            if (!items.Any())
                return null;
            return items.First();
        }

        public IEnumerable<T> Get<T>(byte count) where T : InventoryItem
        {
            return GetAll<T>().Take(count);
        }

        public IEnumerable<T> GetAll<T>() where T : InventoryItem
        {
            return _items.OfType<T>();
        }

        public void PutIn(InventoryItem item)
        {
            _items.Add(item);
        }

        public T PutOutOne<T>() where T : InventoryItem
        {
            var item = GetOne<T>();
            _items.Remove(item);
            return item;
        }

        public IEnumerable<T> PutOut<T>(byte count) where T : InventoryItem
        {
            var items = Get<T>(count);
            _items = _items.Except(items).ToList();
            return items;
        }

        public IEnumerable<T> PutOutAll<T>() where T : InventoryItem
        {
            var items = GetAll<T>();
            _items = _items.Except(items).ToList();
            return items;
        }
    }
}
