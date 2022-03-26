using Models.Entities;
using Controllers;
using Commands;

namespace Menus.Options
{
    public sealed class AttackOption : MenuOption
    {
        public AttackOption(BattleController controller, Entity attacker, Entity target) : base($"Attack {target.Name} ({target.Health.Percent} HP) with a {target.Inventory.ActiveWeapon.Name} ({target.InstantiateDamage().Power * 100} PWR)", new AttackCommand(controller, attacker, target))
        {

        }
    }
}
