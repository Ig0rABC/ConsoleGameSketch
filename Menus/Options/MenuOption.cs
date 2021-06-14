using Commands;

namespace Menus.Options
{
    public class MenuOption
    {
        public string Label { get; }
        private readonly ICommand _command;

        public MenuOption(string label, ICommand command)
        {
            Label = label;
            _command = command;
        }

        public void Execute()
        {
            _command.Execute();
        }
    }
}
