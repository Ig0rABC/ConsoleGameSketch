using System;
using System.Linq;
using Models.Entities;
using Models.Weapons;
using Controllers;
using Menus.Options;

namespace Menus
{
    public sealed class BattleMenu : Menu
    {
        private readonly BattleController _controller;

        public BattleMenu(BattleController controller)
        {
            _controller = controller;
            _controller.PlayerGotMove += OnPlayerGotMove;
            _controller.Attacked += OnAttacked;
            _controller.Missed += OnMissed;
            _controller.ChangedWeapon += OnChangedWeapon;
            _controller.Changed += OnChanged;
        }

        private void OnChanged(Controller next)
        {
            if (next is InventoryController == false)
                Console.WriteLine("You won!");
            _controller.PlayerGotMove -= OnPlayerGotMove;
            _controller.Attacked -= OnAttacked;
            _controller.Missed -= OnMissed;
            _controller.ChangedWeapon -= OnChangedWeapon;
            _controller.Changed -= OnChanged;
        }

        private void OnAttacked(Entity attacker, Entity victim)
        {
            Console.WriteLine($"{attacker.Name} {GetAttackVerb(attacker.Inventory.ActiveWeapon)} the {victim.Name} with a {attacker.Inventory.ActiveWeapon.Name}.");
        }

        private void OnChangedWeapon(Entity attacker, Weapon weapon)
        {
            Console.WriteLine($"{attacker.Name} took up the {weapon.Name}.");
        }

        private void OnMissed(Entity loser)
        {
            Console.WriteLine($"{loser.Name} is missed move.");
        }

        private static string GetAttackVerb(Weapon weapon) => weapon switch
        {
            MeleeWeapon => "striked",
            RangedWeapon<Gunpowder> => "fired at",
            RangedWeapon<Arrow> => "shot at",
            RangedWeapon<Bolt> => "shot at",
            _ => "attacked",
        };

        private void OnPlayerGotMove(Entity[] enemies)
        {
            Console.WriteLine("Your move..");
            if (enemies.Length == 0)
                Console.WriteLine("To attack you must change weapon");
            var options = enemies.Select(e => new AttackOption(_controller, e)).ToList<MenuOption>();
            options.Add(new AutoAttackOption(_controller));
            options.Add(new AutoBattleOption(_controller));
            options.Add(new OpenInventoryOption(_controller));
            OnPlayerGotInput(options.ToArray());
        }
    }
}
