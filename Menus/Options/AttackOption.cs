using Controllers;
using Models.Entities;
using Commands;

namespace Menus
{
    public sealed class AttackOption : MenuOption
    {
        public AttackOption(BattleController controller, Entity victim) : base($"Attack {victim.Name} with {victim.Weapon.Name} ({victim.Health} HP, {victim.Damage} Dmg.)", new AttackCommand(controller, victim))
        {

        }
    }
}
