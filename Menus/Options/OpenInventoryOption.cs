using Controllers;
using Commands;

namespace Menus.Options
{
    public sealed class OpenInventoryOption : MenuOption
    {
        public OpenInventoryOption(BattleController controller) : base("Open Inventory", new OpenInventoryCommand(controller))
        {

        }
    }
}
