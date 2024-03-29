﻿using System;
using System.Linq;
using Controllers;
using Models;
using Models.Entities;
using Models.Battle;
using Models.Weapons;
using Models.Outfits;
using Models.Resistances;
using Models.Items.Usable;

namespace ConsoleGameSketch
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new Client();

            var musket = new Musket();
            var leather = new LeatherJacket();
            var player = new Person(
                "Igor",
                new AbilityBoard(0.3f, 0.3f, 0.5f),
                new EntityResistanceBoard(0.1f, 0.1f, 0.1f),
                new Inventory(new InventoryItem[] {
                    new FlameStaff(),
                    musket,
                    leather,
                    new Gunpowder(),
                    new Gunpowder(),
                    new Gunpowder(),
                    new Gunpowder(),
                    new Gunpowder(),
                    new Gunpowder(),
                    new Naginata(),
                    new MedicialHerb()
                }) { ActiveWeapon =  musket, Outfit = leather });

            var monk = CreateWitAmmunition<Monk, Naginata, RagOutfit>();
            var allias = new Entity[] {
                player,
                monk,
            };

            Game.Controlled.Add(player);
            Game.Controlled.Add(monk);

            var ogre = CreateWithWeapon<Ogre, WoodenClub>();
            Game.Controlled.Add(ogre);

            var shortBow = new ShortBow();
            var enemies = new Entity[]
            {
                CreateWitAmmunition<Goblin, Knife, RustyArmor>(),
                ogre,
                CreateWitAmmunition<Goblin, WoodenClub, RagOutfit>(),
                new Goblin(new Inventory(new InventoryItem[] { shortBow, new Arrow(), new WoodenClub() }) { ActiveWeapon = shortBow })
            };
            foreach (var e in enemies.Concat(allias))
            {
                e.Damaged += client.OnDamaged;
                e.Died += client.OnDied;
                e.Healed += client.OnHealed;
                e.Effector.Added += client.OnAffected;
            }

            var battle = new Battle(new Party(allias), new Party(enemies));
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

        private static Entity CreateWitAmmunition<E, W, O>() where E : Entity where W : Weapon where O : Outfit, new()
        {
            var weapon = (W)Activator.CreateInstance(typeof(W));
            var outfit = new O();
            var inventory = new Inventory(new InventoryItem[] { weapon })
            {
                ActiveWeapon = weapon,
                Outfit = outfit
            };
            return (E)Activator.CreateInstance(typeof(E), new object[] { inventory });
        }
    }
}
