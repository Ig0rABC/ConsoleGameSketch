using Controllers;

namespace Commands
{
    public sealed class ChangeWeaponCommand : ICommand
    {
        private readonly InventoryController _controller;

        public ChangeWeaponCommand(InventoryController controller)
        {
            _controller = controller;
        }

        public void Execute()
        {
            _controller.ChangeWeapon();
        }
    }
}
