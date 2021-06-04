using System;
using System.Linq;
using Models.Entities;
using Models.Weapons;
using Controllers;

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

            _controller.Battle.Won += OnWon;
        }

        private void OnWon()
        {
            Console.WriteLine("You won!");
        }

        private void OnAttacked(Entity attacker, Entity victim)
        {
            Console.WriteLine($"{attacker.Name} {GetAttackVerb(attacker.Weapon)} the {victim.Name} with a {attacker.Weapon.Name}");
        }

        private string GetAttackVerb(Weapon weapon) => weapon switch
        {
            MeleeWeapon => "striked",
            RangedWeapon => "shot",
            _ => "attacked",
        };

        private void OnPlayerGotMove(Entity[] enemies)
        {
            Console.WriteLine("Your move..");
            var options = enemies.Select(e => new AttackOption(_controller, e)).ToList<MenuOption>();
            options.Add(new AutoAttackOption(_controller));
            options.Add(new AutoBattleOption(_controller));
            OnPlayerGotInput(options.ToArray());
        }
    }
}
