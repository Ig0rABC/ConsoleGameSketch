using System;
using System.Linq;
using System.Collections.Generic;
using Models.Entities;
using Models.Weapons;
using Models.Items;
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
            _controller.PlayerGotMove -= OnPlayerGotMove;
            _controller.Attacked -= OnAttacked;
            _controller.Missed -= OnMissed;
            _controller.ChangedWeapon -= OnChangedWeapon;
            _controller.Changed -= OnChanged;
        }

        private void OnAttacked(Entity attacker, Entity target)
        {
            Console.WriteLine($"{attacker.Name} {GetAttackVerb(attacker.Inventory.ActiveWeapon)} the {target.Name} with a {attacker.Inventory.ActiveWeapon.Name}.");
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

        private void OnPlayerGotMove(Entity attacker, IEnumerable<Entity> enemies, IEnumerable<Entity> allies)
        {
            Console.WriteLine("Your party:");
            foreach (var ally in allies)
            {
                Console.WriteLine($"{ally.Name} ({ally.Health.Percent} HP) with a {ally.Inventory.ActiveWeapon.Name} ({ally.InstantiateDamage().Power * 100} PWR)");
            }
            Console.WriteLine($"\n{attacker.Name}'s move..");
            var options = enemies.Select(e => new AttackOption(_controller, attacker, e)).ToList<MenuOption>();
            if (attacker.Inventory.Has<UsableItem>() || attacker.Inventory.GetWeaponsForChange(attacker).Any())
            {
                var option = new OpenInventoryOption(_controller, attacker);
                options.Add(option);
            }
            if (attacker.CanAttack)
            {
                var option = new AutoAttackOption(_controller, attacker);
                options.Add(option);
            }
            else
            {
                Console.WriteLine("To attack you must change weapon!");
            }
            options.Add(new AutoBattleOption(_controller, attacker));
            OnPlayerGotInput(options);
        }
    }
}
