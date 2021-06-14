using Controllers;

namespace Commands
{
    public class OpenInventoryCommand : ICommand
    {
        private readonly BattleController _controller;

        public OpenInventoryCommand(BattleController controller)
        {
            _controller = controller;
        }

        public void Execute()
        {
            _controller.OpenInventory();
        }
    }
}
