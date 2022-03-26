using Models.Entities;
using Controllers;

namespace Commands
{
    public sealed class AutoBattleCommand : ICommand
    {
        private readonly BattleController _controller;
        private readonly Entity _attacker;

        public AutoBattleCommand(BattleController controller, Entity attacker)
        {
            _controller = controller;
            _attacker = attacker;
        }

        public void Execute()
        {
            _controller.SwitchToAuto(_attacker);
        }

    }
}
