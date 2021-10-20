using System;
using System.Collections.Generic;
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

        private void OnPlayerGotMove(Entity attacker, Entity[] enemies, Entity[] allies)
        {
            Console.WriteLine("Your party..");
            foreach (var ally in allies)
                 Console.WriteLine($"{ally.Name} with a {ally.Inventory.ActiveWeapon.Name} ({ally.Health} HP, {ally.Damage} Dmg.)");
            Console.WriteLine($"\n{attacker.Name}'s move..");
            if (enemies.Length == 0)
                Console.WriteLine("To attack you must change weapon");
            var options = new List<MenuOption>();
            foreach (var e in enemies)
                options.Add(new AttackOption(_controller, e));
            options.Add(new OpenInventoryOption(_controller));
            options.Add(new AutoAttackOption(_controller));
            options.Add(new AutoBattleOption(_controller));
            OnPlayerGotInput(options.ToArray());
        }
    }
}
