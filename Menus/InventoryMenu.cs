using System;
using System.Collections.Generic;
using System.Linq;
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
            _controller.ChangingWeapon += OnChangingWeapon;
            _controller.Changed += OnChanged;
        }

        public void OnChanged(Controller controller)
        {
            _controller.Changed -= OnChanged;
            _controller.ChoosingAction -= OnChoosingAction;
            _controller.ChangingWeapon -= OnChangingWeapon;
        }

        public void OnChoosingAction()
        {
            Console.WriteLine("What do you want?");
            var options = new List<MenuOption>();
            if (_controller.CanChooseWeapon)
                options.Add(new ChangeWeaponOption(_controller));
            options.Add(new CloseInventoryOption(_controller));
            OnPlayerGotInput(options.ToArray());
        }

        public void OnChangingWeapon(Weapon[] weapons)
        {
            Console.WriteLine("Your weapons:");
            var options = weapons.Select(w => new SetWeaponOption(_controller, w)).ToList<MenuOption>();
            options.Add(new CancelOption(_controller));
            OnPlayerGotInput(options.ToArray());
        }
    }
}
