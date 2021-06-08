using System;
using Controllers;
using Models;
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

            var player = new Player("Igor", new AbilityBoard(24, 14, 10), new Inventory(new Item[] { new Musket(), new Gunpowder(), new Gunpowder(), new Gunpowder(), new Gunpowder() }));

            var allias = new Entity[] {
                CreateWithWeapon<Monk, Naginata>(),
                player
            };

            var enemies = new Entity[]
            {
                CreateWithWeapon<Goblin, Knife>(),
                CreateWithWeapon<Ogre, WoodenClub>(),
                CreateWithWeapon<Goblin, WoodenClub>(),
                CreateWithWeapon<Goblin, ShortBow>()
            };
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

        private static Entity CreateWithWeapon<E, W>() where E : Entity where W : Weapon, new()
        {
            var weapon = new W();
            var inventory = new Inventory(new Item[] { weapon })
            {
                ActiveWeapon = weapon
            };
            return (E)Activator.CreateInstance(typeof(E), new object[] { inventory });
        }
    }
}
