using System.Collections.Generic;
using Menus.Options;

namespace Menus
{
    abstract public class Menu
    {
        public delegate void PlayerGotInputHandler(IEnumerable<MenuOption> options);
        public event PlayerGotInputHandler PlayerGotInput;

        protected void OnPlayerGotInput(IEnumerable<MenuOption> options)
        {
            PlayerGotInput?.Invoke(options);
        }
    }
}
