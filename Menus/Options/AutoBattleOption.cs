using Models.Entities;
using Controllers;
using Commands;

namespace Menus.Options
{
    public sealed class AutoBattleOption : MenuOption
    {
        public AutoBattleOption(BattleController controller, Entity attacker) : base("Switch to auto battle mode", new AutoBattleCommand(controller, attacker))
        {

        }
    }
}
