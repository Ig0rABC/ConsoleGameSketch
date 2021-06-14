using Models;
using Models.Weapons;
using Models.Items;

namespace Controllers
{
    public sealed class InventoryController : Controller
    {
        public bool CanChooseWeapon => _inventory.Has<Weapon>(2);

        public delegate void ChoosingActionHandler();
        public event ChoosingActionHandler ChoosingAction;

        public delegate void ChangingWeaponHandler(Weapon[] weapons);
        public event ChangingWeaponHandler ChangingWeapon;

        private readonly Inventory _inventory;

        public InventoryController(Inventory inventory, Controller next) : base(next)
        {
            _inventory = inventory;
        }

        public override void Update()
        {
            ChoosingAction?.Invoke();
        }
        
        public void Close()
        {
            OnChange();
        }

        public void ChangeWeapon()
        {
            ChangingWeapon?.Invoke(_inventory.InactiveWeapons);
        }

        public void SetWeapon(Weapon weapon)
        {
            _inventory.ActiveWeapon = weapon;
            OnChange();
        }
    }
}
