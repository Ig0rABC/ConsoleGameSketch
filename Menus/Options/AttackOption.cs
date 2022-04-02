using Models.Entities;
using Controllers;
using Commands;

namespace Menus.Options
{
    public sealed class AttackOption : MenuOption
    {
        public AttackOption(BattleController controller, Entity attacker, Entity target) : base($"Attack {View.GetEntityLabel(target)}", new AttackCommand(controller, attacker, target))
        {
        }
    }
}
