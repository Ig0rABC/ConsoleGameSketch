using System.Collections.Generic;
using Models;
using Models.Items;
using Models.Weapons;

namespace Controllers
{
    public sealed class InventoryController : Controller
    {

        public delegate void ChoosingActionHandler(IEnumerable<InventoryItem> items);
        public event ChoosingActionHandler ChoosingAction;

        private readonly Inventory _inventory;

        public InventoryController(Inventory inventory, Controller next) : base(next)
        {
            _inventory = inventory;
        }

        public override void Update()
        {
            var items = _inventory.GetAll<InventoryItem>();
            ChoosingAction?.Invoke(items);
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
            _inventory.UseItem(item);
            OnChange();
        }
    }
}
