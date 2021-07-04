using Models.Items;
using Controllers;
using Commands;

namespace Menus.Options
{
    public sealed class UseItemOption : MenuOption
    {
        public UseItemOption(InventoryController controller, UsableItem item) : base($"Use {item.Name}", new UseItemCommand(controller, item))
        {

        }
    }
}
