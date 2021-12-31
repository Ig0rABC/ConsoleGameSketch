using Controllers;
using Commands;

namespace Menus.Options
{
    public sealed class CloseInventoryOption : MenuOption
    {
        public CloseInventoryOption(InventoryController controller) : base("Quit", new CloseInventoryCommand(controller))
        {

        }
    }
}
