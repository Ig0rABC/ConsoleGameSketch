using System.Linq;
using Models;
using Models.Weapons;

namespace Controllers
{
    public sealed class InventoryController : Controller
    {

        public delegate void ChoosingActionHandler(InventoryItem[] items);
        public event ChoosingActionHandler ChoosingAction;

        private readonly Inventory _inventory;

        public InventoryController(Inventory inventory, Controller next) : base(next)
        {
            _inventory = inventory;
        }

        public override void Update()
        {
            ChoosingAction?.Invoke(_inventory.GetAll<InventoryItem>());
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
    }
}
