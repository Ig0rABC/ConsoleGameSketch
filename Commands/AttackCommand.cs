using Controllers;
using Models.Entities;

namespace Commands
{
    public sealed class AttackCommand : ICommand
    {
        private readonly BattleController _controller;
        private readonly Entity _victim;

        public AttackCommand(BattleController controller, Entity victim)
        {
            _controller = controller;
            _victim = victim;
        }

        public void Execute()
        {
            _controller.Attack(_victim);
        }

    }
}
