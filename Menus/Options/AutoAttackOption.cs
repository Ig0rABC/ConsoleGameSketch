using Controllers;
using Commands;

namespace Menus.Options
{
    public sealed class AutoAttackOption : MenuOption
    {
        public AutoAttackOption(BattleController controller) : base("Auto Attack", new AutoAttackCommand(controller))
        {

        }
    }
}
