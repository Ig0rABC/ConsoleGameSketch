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
        public byte ActiveWeaponDamage => ActiveWeapon.GetDamage(_owner.Abilities);
        public bool CanUseActiveWeapon => ActiveWeapon.CanUsed(_owner);
        public IEnumerable<Weapon> WeaponsForChange => _items.OfType<Weapon>().Where(IsWeaponRelevantForChange);
        public Weapon WeaponForAutoChange => _items.OfType<Weapon>().First(IsWeaponRelevantForChange);

        private Entity _owner;
        private Weapon _activeWeapon;
        private List<InventoryItem> _items;

        public Inventory(IEnumerable<InventoryItem> items)
        {
            _items = items.ToList();
        }

        public Entity Owner {
            set
            {
                if (_owner != null)
                    throw new InvalidOperationException("Cannot set owner if he is already set");
                _owner = value;
            }
        }

        private bool IsWeaponRelevantForChange(Weapon weapon)
        {
            return !weapon.Equals(_activeWeapon) && weapon.CanUsed(_owner);
        }

        public Weapon ActiveWeapon
        {
            get
            {
                if (_activeWeapon == null)
                    _activeWeapon = WeaponForAutoChange;
                return _activeWeapon;
            }
            set
            {
                if (!Has(value))
                    throw new InvalidOperationException($"Specified weapon {value} must be in inventory for activate");
                _activeWeapon = value;
            }
        }

        public void UseItem(UsableItem item)
        {
            if (!Has(item))
                throw new InvalidOperationException($"Specified item {item} must be in inventory for use");
            item.Use(_owner);
            _items.Remove(item);
        }

        public void UseItem<T>() where T : UsableItem
        {
            GetOne<T>().Use(_owner);
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
            return _items.OfType<T>().First();
        }

        public IEnumerable<T> Get<T>(byte count) where T : InventoryItem
        {
            return _items.OfType<T>().Take(count);
        }

        public IEnumerable<T> GetAll<T>() where T : InventoryItem
        {
            return _items.OfType<T>();
        }

        public void PutIn(InventoryItem item)
        {
            _items.Add(item);
        }

        public IEnumerable<T> PutOut<T>(byte count = 1) where T : InventoryItem
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
