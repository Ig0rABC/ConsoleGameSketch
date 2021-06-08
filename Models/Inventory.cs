using System;
using System.Collections.Generic;
using System.Linq;
using Models.Weapons;

namespace Models
{
    public class Inventory {

        private readonly List<Item> _items;
        private Weapon _activeWeapon;

        public Inventory(Item[] items)
        {
            _items = new List<Item>(items);
        }

        public void PutIn(Item item)
        {
            _items.Add(item);
        }

        public Weapon ActiveWeapon
        {
            get => _activeWeapon ?? Get<Weapon>()[0];
            set
            {
                if (!_items.Contains(value))
                    throw new ArgumentException($"Specified weapon {value} must be in inventory");
                _activeWeapon = value;
            }
        }

        public T[] Get<T>(byte count = 1) where T : Item
        {
            return _items.OfType<T>().Take(count).ToArray();
        }

        public T[] GetAll<T>() where T : Item
        {
            return _items.OfType<T>().ToArray();
        }

        public T[] PutOut<T>(byte count = 1) where T : Item
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

        public bool Has<T>(byte count = 1) where T : Item
        {
            return _items.OfType<T>().Count() >= count;
        }
    }
}
