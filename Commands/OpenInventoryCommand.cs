using Models.Entities;
using Controllers;

namespace Commands
{
    public class OpenInventoryCommand : ICommand
    {
        private readonly BattleController _controller;
        private readonly Entity _owner;

        public OpenInventoryCommand(BattleController controller, Entity owner)
        {
            _controller = controller;
            _owner = owner;
        }

        public void Execute()
        {
            _controller.OpenInventory(_owner);
        }
    }
}
