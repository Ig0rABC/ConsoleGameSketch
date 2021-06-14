using Models.Weapons;
using Controllers;
using Commands;

namespace Menus.Options
{
    public sealed class SetWeaponOption : MenuOption
    {
        public SetWeaponOption(InventoryController controller, Weapon weapon) : base($"Take {weapon.Name}", new SetWeaponCommand(controller, weapon))
        {

        }
    }
}
