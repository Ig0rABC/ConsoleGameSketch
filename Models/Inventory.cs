using System;
using System.Collections.Generic;
using System.Linq;
using Models.Entities;
using Models.Weapons;

namespace Models
{
    public class Inventory {

        public Entity Owner { private get; set; }
        public byte ActiveWeaponDamage => ActiveWeapon.GetDamage(Owner.Abilities);
        public bool CanUseActiveWeapon => ActiveWeapon.CanUsed(Owner);
        public Weapon[] InactiveWeapons => _items.OfType<Weapon>().Where(w => !w.Equals(ActiveWeapon)).ToArray();

        private Weapon _activeWeapon;
        private readonly List<InventoryItem> _items;

        public Inventory(InventoryItem[] items)
        {
            _items = new List<InventoryItem>(items);
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
                    throw new ArgumentException($"Specified weapon {value} must be in inventory");
                _activeWeapon = value;
            }
        }

        public void TakeWeaponWhichCanUsed()
        {
            _activeWeapon = _items.OfType<Weapon>().First(w => w.CanUsed(Owner));
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
