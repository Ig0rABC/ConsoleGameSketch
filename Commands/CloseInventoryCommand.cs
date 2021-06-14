using Controllers;

namespace Commands
{
    public sealed class CloseInventoryCommand : ICommand
    {
        private readonly InventoryController _controller;

        public CloseInventoryCommand(InventoryController controller)
        {
            _controller = controller;
        }

        public void Execute()
        {
            _controller.Close();
        }
    }
}
