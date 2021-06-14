using Controllers;

namespace Commands
{
    public sealed class CancelCommand : ICommand
    {
        private readonly Controller _controller;

        public CancelCommand(Controller controller)
        {
            _controller = controller;
        }

        public void Execute()
        {
            _controller.DoNothing();
        }
    }
}
