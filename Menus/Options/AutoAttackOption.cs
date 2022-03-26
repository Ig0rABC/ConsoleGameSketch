using Models.Entities;
using Controllers;
using Commands;

namespace Menus.Options
{
    public sealed class AutoAttackOption : MenuOption
    {
        public AutoAttackOption(BattleController controller, Entity attacker) : base("Auto Attack", new AutoAttackCommand(controller, attacker))
        {

        }
    }
}
