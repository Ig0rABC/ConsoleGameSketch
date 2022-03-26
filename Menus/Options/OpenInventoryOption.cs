using Models.Entities;
using Controllers;
using Commands;

namespace Menus.Options
{
    public sealed class OpenInventoryOption : MenuOption
    {
        public OpenInventoryOption(BattleController controller, Entity owner) : base("Open Inventory", new OpenInventoryCommand(controller, owner))
        {

        }
    }
}
