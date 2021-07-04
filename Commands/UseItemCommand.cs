using Models.Items;
using Controllers;

namespace Commands
{
    public sealed class UseItemCommand : ICommand
    {
        private readonly UsableItem _item;
        private readonly InventoryController _controller;

        public UseItemCommand(InventoryController controller, UsableItem item)
        {
            _item = item;
            _controller = controller;
        }

        public void Execute()
        {
            _controller.UseItem(_item);
        }
    }
}
