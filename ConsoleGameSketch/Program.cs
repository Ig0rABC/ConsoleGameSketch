using System;
using Controllers;
using Models;
using Models.Entities;
using Models.Battle;
using Models.Weapons;
using Models.Items.Usable;

namespace ConsoleGameSketch
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new Client();

            var player = new Person(
                "Igor",
                new AbilityBoard(24, 14, 10),
                new Inventory(new InventoryItem[] {
                    new Musket(),
                    new Gunpowder(),
                    new Gunpowder(),
                    new Gunpowder(),
                    new Gunpowder(),
                    new Gunpowder(),
                    new Gunpowder(),
                    new Naginata(),
                    new MedicialHerb()
                }));

            var monk = CreateWithWeapon<Monk, Naginata>();
            var allias = new Entity[] {
                monk,
                player
            };

            Game.Guided.Add(player);
            Game.Guided.Add(monk);

            var ogre = CreateWithWeapon<Ogre, WoodenClub>();
            Game.Guided.Add(ogre);

            var enemies = new Entity[]
            {
                CreateWithWeapon<Goblin, Knife>(),
                ogre,
                CreateWithWeapon<Goblin, WoodenClub>(),
                new Goblin(new Inventory(new InventoryItem[] { new ShortBow(), new Arrow(), new WoodenClub() }))
            };
            foreach (var e in enemies)
            {
                e.Damaged += client.OnDamaged;
                e.Died += client.OnDied;
            }
            foreach (Entity a in allias)
            {
                if (a is Person == false)
                {
                    a.Damaged += client.OnDamaged;
                    a.Died += client.OnDied;
                }
            }
            player.Damaged += client.OnDamaged;
            player.Died += client.OnDied;

            var battle = new Battle(allias, enemies);
            var controller = new BattleController(battle);

            client.Controller = controller;
            client.Start();
        }

        private static Entity CreateWithWeapon<E, W>() where E : Entity where W : Weapon, new()
        {
            var weapon = new W();
            var inventory = new Inventory(new InventoryItem[] { weapon })
            {
                ActiveWeapon = weapon
            };
            return (E)Activator.CreateInstance(typeof(E), new object[] { inventory });
        }
    }
}
