using Controllers;
using Models.Entities;
using Commands;

namespace Menus
{
    public sealed class AttackOption : MenuOption
    {
        public AttackOption(BattleController controller, Entity victim) : base($"Attack {victim.Name} ({victim.Health} HP, {victim.Strength} Strength)", new AttackCommand(controller, victim))
        {

        }
    }
}
