using System.Collections.Generic;
using System.Linq;
using Models;
using Models.Entities;
using Models.Items;
using Models.Weapons;

namespace Controllers
{
    public sealed class InventoryController : Controller
    {

        public delegate void ChoosingActionHandler(IEnumerable<InventoryItem> items);
        public event ChoosingActionHandler ChoosingAction;

        private readonly Inventory _inventory;
        private readonly Entity _owner;

        public InventoryController(Inventory inventory, Entity owner, Controller next) : base(next)
        {
            _inventory = inventory;
            _owner = owner;
        }

        public override void Update()
        {
            var items = _inventory.GetAll<UsableItem>();
            var weapons = _inventory.GetWeaponsForChange(_owner);
            var options = new List<InventoryItem>(items).Concat(weapons);
            ChoosingAction?.Invoke(options);
        }
        
        public void Close()
        {
            OnChange();
        }

        public void SetWeapon(Weapon weapon)
        {
            _inventory.ActiveWeapon = weapon;
            OnChange();
        }

        public void UseItem(UsableItem item)
        {
            _inventory.UseItem(item, _owner);
            OnChange();
        }
    }
}
