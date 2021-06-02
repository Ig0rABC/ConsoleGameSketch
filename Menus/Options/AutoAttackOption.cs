using Controllers;
using Models.Entities;
using Commands;

namespace Menus
{
    public sealed class AutoAttackOption : MenuOption
    {
        public AutoAttackOption(BattleController controller) : base("Auto Attack", new AutoAttackCommand(controller))
        {

        }
    }
}
