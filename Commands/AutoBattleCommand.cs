using Controllers;

namespace Commands
{
    public sealed class AutoBattleCommand : ICommand
    {
        private BattleController _controller;

        public AutoBattleCommand(BattleController controller)
        {
            _controller = controller;
        }

        public void Execute()
        {
            _controller.SwitchToAuto();
        }

    }
}
