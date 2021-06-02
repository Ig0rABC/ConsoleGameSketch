using Controllers;
using Models.Entities;

namespace Commands
{
    public sealed class AttackCommand : ICommand
    {
        private BattleController _controller;
        private Entity _victim;

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
