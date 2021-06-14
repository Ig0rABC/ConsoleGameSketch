using Models.Weapons;
using Controllers;

namespace Commands
{
    public sealed class SetWeaponCommand : ICommand
    {
        private readonly InventoryController _controller;
        private readonly Weapon _weapon;

        public SetWeaponCommand(InventoryController controller, Weapon weapon)
        {
            _controller = controller;
            _weapon = weapon;
        }

        public void Execute()
        {
            _controller.SetWeapon(_weapon);
        }
    }
}
