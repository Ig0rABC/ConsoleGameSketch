using System;
using System.Linq;
using Models;
using Models.Items;
using Models.Weapons;
using Controllers;
using Menus.Options;

namespace Menus
{
    public class InventoryMenu : Menu
    {
        private readonly InventoryController _controller;

        public InventoryMenu(InventoryController controller)
        {
            _controller = controller;
            _controller.ChoosingAction += OnChoosingAction;
            _controller.Changed += OnChanged;
        }

        public void OnChanged(Controller controller)
        {
            _controller.Changed -= OnChanged;
            _controller.ChoosingAction -= OnChoosingAction;
        }

        public void OnChoosingAction(InventoryItem[] items)
        {
            Console.WriteLine("What do you want?");
            var options = Array.Empty<MenuOption>();
            var itemOptions = items.OfType<UsableItem>().Select(i => new UseItemOption(_controller, i));
            var weaponOptions = items.OfType<Weapon>().Select(w => new SetWeaponOption(_controller, w));
            options = options.Concat(itemOptions).Concat(weaponOptions).ToArray();
            OnPlayerGotInput(options);
        }
    }
}
