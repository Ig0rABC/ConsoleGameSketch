using System;
using System.Linq;
using System.Collections.Generic;
using Controllers;
using Models.Entities;
using Menus;
using Menus.Options;

namespace ConsoleGameSketch
{
    public class Client
    {
        private bool _isRunning;
        private Controller _controller;
        private Menu _menu;
        
        public Client()
        {
            _isRunning = false;
        }

        public Controller Controller
        {
            set => OnControllerChanged(value);
        }

        public void Start()
        {
            _isRunning = true;
            while (_isRunning)
            {
                Console.WriteLine();
                _controller.Update();
            }
        }

        public void Stop()
        {
            _isRunning = false;
        }

        public void OnDamaged(Entity victim, float damage)
        {
            Console.WriteLine($"{victim.Name} recieved {damage * 100} damage");
        }

        public void OnRecovered(Entity victim, float recovery)
        {
            Console.WriteLine($"{victim.Name} restored {recovery * 100} HP");
        }

        public void OnDied(Entity dead, float damage)
        {
            Console.WriteLine($"{dead.Name} recieved {damage * 100} damage and died");
        }

        private void OnPlayerGotInput(IEnumerable<MenuOption> options)
        {
            byte index = 1;
            foreach (var option in options)
            {
                Console.WriteLine($"{index++}. {option.Label}");
            }
            var optionsCount = (byte)options.Count();
            var optionNumber = InputOptionNumber(optionsCount);
            options.ElementAt(optionNumber - 1).Execute();
        }

        private byte InputOptionNumber(byte optionCount)
        {
            Console.Write("Input an option number:>");
            var input = Console.ReadLine();
            try
            {
                var optionNumber = byte.Parse(input);
                if (optionNumber > optionCount)
                    throw new OverflowException();
                return optionNumber;
            }
            catch (FormatException)
            {
                Console.WriteLine("Input a number!");
            }
            catch (OverflowException)
            {
                if (input.StartsWith('-'))
                    Console.WriteLine("Input a positive number!");
                else
                    Console.WriteLine($"In all {optionCount} options!");
            }
            return InputOptionNumber(optionCount);
        }

        private void OnControllerChanged(Controller controller)
        {
            if (_controller is null == false)
            {
                _controller.Changed -= OnControllerChanged;
                _menu.PlayerGotInput -= OnPlayerGotInput;
            }
            if (controller is null)
            {
                Console.WriteLine("Game over!");
                Stop();
                return;
            }
            _controller = controller;
            _menu = MenuFactory.Create(_controller);
            _controller.Changed += OnControllerChanged;
            _menu.PlayerGotInput += OnPlayerGotInput;
        }
    }
}
