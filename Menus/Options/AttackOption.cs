using Models.Entities;
using Controllers;
using Commands;

namespace Menus.Options
{
    public sealed class AttackOption : MenuOption
    {
        public AttackOption(BattleController controller, Entity victim) : base($"Attack {victim.Name} ({victim.Health.Percent} HP) with a {victim.Inventory.ActiveWeapon.Name} ({victim.Inventory.ActiveWeapon.Power * 100} PWR)", new AttackCommand(controller, victim))
        {

        }
    }
}
