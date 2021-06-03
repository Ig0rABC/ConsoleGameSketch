using System;
using Controllers;
using Models.Entities;
using Models.Battle;
using Models.Weapons;

namespace ConsoleGameSketch
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new Client();

            var player = new Player("Igor", 24, new Sword());

            var allias = new Entity[] { new Monk(new Naginata()), player };

            var enemies = new Entity[] { new Goblin(new Knife()), new Ogre(new WoodenClub()), new Goblin(new WoodenClub()), new Goblin(new ShortBow()) };
            foreach (var e in enemies)
            {
                e.Damaged += client.OnDamaged;
                e.Died += client.OnDied;
            }
            foreach (Entity a in allias)
            {
                if (a is Player == false)
                {
                    a.Damaged += client.OnDamaged;
                    a.Died += client.OnDied;
                }
            }
            player.Damaged += client.OnPlayerDamaged;
            player.Died += client.OnPlayerDied;

            var battle = new Battle(allias, enemies);
            var controller = new BattleController(battle);

            client.Controller = controller;
            client.Start();
        }
    }
}
