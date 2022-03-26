using Models.Entities;
using Controllers;

namespace Commands
{
    public sealed class AutoAttackCommand : ICommand
    {
        private readonly BattleController _controller;
        private readonly Entity _attacker;

        public AutoAttackCommand(BattleController controller, Entity attacker)
        {
            _controller = controller;
            _attacker = attacker;
        }

        public void Execute()
        {
            _controller.AutoMove(_attacker);
        }

    }
}
