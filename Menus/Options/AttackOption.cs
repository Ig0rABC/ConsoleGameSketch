using Models.Entities;
using Controllers;
using Commands;

namespace Menus.Options
{
    public sealed class AttackOption : MenuOption
    {
        public AttackOption(BattleController controller, Entity victim) : base($"Attack {victim.Name} with a {victim.Inventory.ActiveWeapon.Name} ({victim.Health} HP, {victim.Damage.Power} Dmg.)", new AttackCommand(controller, victim))
        {

        }
    }
}
