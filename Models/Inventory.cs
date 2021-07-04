using System;
using System.Collections.Generic;
using System.Linq;
using Models.Entities;
using Models.Items;
using Models.Weapons;

namespace Models
{
    public class Inventory {

        public byte ActiveWeaponDamage => ActiveWeapon.GetDamage(_owner.Abilities);
        public bool CanUseActiveWeapon => ActiveWeapon.CanUsed(_owner);
        public Weapon[] WeaponsForChange => _items.OfType<Weapon>().Where(w => !w.Equals(ActiveWeapon) && w.CanUsed(_owner)).ToArray();
        public Weapon WeaponForAutoChange => WeaponsForChange.First();

        private Entity _owner;
        private Weapon _activeWeapon;
        private readonly List<InventoryItem> _items;

        public Inventory(InventoryItem[] items)
        {
            _items = new List<InventoryItem>(items);
        }

        public Entity Owner {
            set
            {
                if (_owner != null)
                    throw new InvalidOperationException("Cannot set owner if he is already set");
                _owner = value;
            }
        }

        public Weapon ActiveWeapon
        {
            get
            {
                if (_activeWeapon == null)
                    _activeWeapon = _items.OfType<Weapon>().First();
                return _activeWeapon;
            }
            set
            {
                if (!_items.Contains(value))
                    throw new InvalidOperationException($"Specified weapon {value} must be in inventory for activate");
                _activeWeapon = value;
            }
        }

        public void UseItem(UsableItem item)
        {
            if (!_items.Contains(item))
                throw new InvalidOperationException($"Specified item {item} must be in inventory for use");
            item.Use(_owner);
            _items.Remove(item);
        }

        public void PutIn(InventoryItem item)
        {
            _items.Add(item);
        }

        public T[] Get<T>(byte count = 1) where T : InventoryItem
        {
            return _items.OfType<T>().Take(count).ToArray();
        }

        public T[] GetAll<T>() where T : InventoryItem
        {
            return _items.OfType<T>().ToArray();
        }

        public T[] PutOut<T>(byte count = 1) where T : InventoryItem
        {
            var items = new List<T>();
            foreach (var item in _items.OfType<T>())
            {
                _items.Remove(item);
                items.Add(item);
                if (items.Count == count)
                    break;
            }
            return items.ToArray();
        }

        public bool Has<T>(byte count = 1) where T : InventoryItem
        {
            return _items.OfType<T>().Count() >= count;
        }
    }
}
