using Models.Weapons;
using Controllers;
using Commands;

namespace Menus.Options
{
    public sealed class SetWeaponOption : MenuOption
    {
        public SetWeaponOption(InventoryController controller, MeleeWeapon weapon) : base($"Take {weapon.Name} ({View.GetPercent(weapon.Condition)} CND)", new SetWeaponCommand(controller, weapon))
        {

        }
        public SetWeaponOption(InventoryController controller, Weapon weapon) : base($"Take {weapon.Name}", new SetWeaponCommand(controller, weapon))
        {

        }
    }
}
