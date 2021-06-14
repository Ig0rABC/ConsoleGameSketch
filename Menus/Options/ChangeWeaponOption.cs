using Controllers;
using Commands;

namespace Menus.Options
{
    public sealed class ChangeWeaponOption : MenuOption
    {
        public ChangeWeaponOption(InventoryController controller) : base($"Change weapon", new ChangeWeaponCommand(controller))
        {

        }
    }
}
