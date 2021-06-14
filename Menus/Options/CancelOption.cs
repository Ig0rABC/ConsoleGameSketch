using Controllers;
using Commands;

namespace Menus.Options
{
    public sealed class CancelOption : MenuOption
    {
        public CancelOption(Controller controller) : base("Cancel", new CancelCommand(controller))
        {

        }
    }
}
