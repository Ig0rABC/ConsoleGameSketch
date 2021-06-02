
namespace Menus
{
    abstract public class Menu
    {
        public delegate void PlayerGotInputHandler(MenuOption[] options);
        public event PlayerGotInputHandler PlayerGotInput;

        protected void OnPlayerGotInput(MenuOption[] options)
        {
            PlayerGotInput?.Invoke(options);
        }
    }
}
