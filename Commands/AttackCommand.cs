using Controllers;
using Models.Entities;

namespace Commands
{
    public sealed class AttackCommand : ICommand
    {
        private readonly BattleController _controller;
        private readonly Entity _attacker;
        private readonly Entity _target;

        public AttackCommand(BattleController controller, Entity attacker, Entity target)
        {
            _controller = controller;
            _attacker = attacker;
            _target = target;
        }

        public void Execute()
        {
            _controller.Attack(_attacker, _target);
        }

    }
}
