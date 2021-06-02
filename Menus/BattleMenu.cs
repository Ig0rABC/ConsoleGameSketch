using System;
using System.Collections.Generic;
using System.Linq;
using Models.Entities;
using Controllers;
using Models.Battle;

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
            Console.WriteLine($"{attacker.Name} is attacking {victim.Name}");
        }

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
