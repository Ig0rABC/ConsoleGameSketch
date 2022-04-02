using System;
using System.Linq;
using System.Collections.Generic;
using Models;
using Models.Entities;
using Models.Damages;
using Models.Weapons;
using Models.Outfits;
using Menus.Options;

namespace Menus
{
    public static class View
    {
        public static void Separate()
        {
            Console.WriteLine();
        }

        public static MenuOption ChooseOption(IEnumerable<MenuOption> options)
        {
            var optionsCount = (byte)options.Count();
            for (var i = 0; i < optionsCount; i++)
                Console.WriteLine($"{i + 1}. {options.ElementAt(i).Label}");
            var index = InputOptionIndex(optionsCount);
            return options.ElementAt(index);
        }

        public static byte GetPercent(StateBar bar)
        {
            return GetPercent(bar.Value);
        }

        public static byte GetPercent(float value)
        {
            return (byte)Math.Round(value * 100, 0, MidpointRounding.AwayFromZero);
        }

        public static string GetEntityLabel(Entity entity)
        {
            var weapon = entity.Inventory.ActiveWeapon;
            var outfit = entity.Inventory.Outfit;
            var labels = new List<string>()
            {
                entity.Name,
                WrapDetails(GetHealthLabel(entity))
            };
            if (outfit is null == false)
                labels.Add($"in {GetOufitLabel(outfit)}");
            if (weapon is null == false)
                labels.Add($"with {GetWeaponLabel(weapon, entity)}");
            return string.Join(' ', labels);
        }

        public static string GetHealthLabel(Entity entity)
        {
            return $"{GetPercent(entity.Health)} HP";
        }

        public static string GetWeaponLabel(Weapon weapon, Entity owner)
        {
            return GetWeaponLabel(weapon, owner.InstantiateDamage().Power);
        }

        public static string GetWeaponLabel(Weapon weapon)
        {
            return GetWeaponLabel(weapon, weapon.Power);
        }

        public static string GetOufitLabel(Outfit outfit)
        {
            return outfit.Name;
        }

        public static void GameOver()
        {
            Console.WriteLine("Game over!");
        }

        public static void OnDamaged(Entity victim, float damage)
        {
            Console.WriteLine($"{victim.Name} recieved {damage * 100} damage");
        }

        public static void OnRecovered(Entity entity, float recovery)
        {
            Console.WriteLine($"{entity.Name} restored {recovery * 100} HP");
        }

        public static void OnDied(Entity dead, float damage)
        {
            Console.WriteLine($"{dead.Name} recieved {damage * 100} damage and died");
        }

        private static string WrapDetails(string info)
        {
            return $"({info})";
        }

        private static string GetWeaponLabel(Weapon weapon, float power)
        {
            return $"{weapon.Name} {WrapDetails($"{GetPercent(power)} PWR")}";
        }

        private static byte InputOptionIndex(byte optionsCount)
        {
            Console.Write("Input an option number:>");
            var input = Console.ReadLine();
            bool result = byte.TryParse(input, out byte index);
            if (!result || index == 0 || index > optionsCount)
            {
                Separate();
                Console.WriteLine($"Enter a number from 1 to {optionsCount}!");
                return InputOptionIndex(optionsCount);
            }
            return (byte)(index - 1);
        }
    }
}
