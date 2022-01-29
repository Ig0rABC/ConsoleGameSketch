using System;
using System.Linq;
using System.Collections.Generic;
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

        public void OnChoosingAction(IEnumerable<InventoryItem> items)
        {
            Console.WriteLine("What do you want?");
            var itemOptions = items.OfType<UsableItem>().Select(i => new UseItemOption(_controller, i));
            var weaponOptions = items.OfType<Weapon>().Select(w => new SetWeaponOption(_controller, w));
            var options = new List<MenuOption>(itemOptions).Concat(weaponOptions);
            OnPlayerGotInput(options);
        }
    }
}
