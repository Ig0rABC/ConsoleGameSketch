using System;
using System.Linq;
using System.Collections.Generic;
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

        public void OnChoosingAction(IEnumerable<UsableItem> items, IEnumerable<Weapon> weapons)
        {
            Console.WriteLine("What do you want?");
            var itemOptions = items.Select(i => new UseItemOption(_controller, i));
            var weaponOptions = weapons.Select(w => new SetWeaponOption(_controller, w));
            var options = itemOptions
                .Cast<MenuOption>()
                .Concat(weaponOptions)
                .Append(new CloseInventoryOption(_controller));
            OnPlayerGotInput(options);
        }
    }
}
