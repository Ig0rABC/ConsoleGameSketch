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

        public void OnDamaged(Entity victim, byte damage)
        {
            Console.WriteLine($"{victim.Name} recieved {damage} damage");
        }

        public void OnDied(Entity dead)
        {
            Console.WriteLine($"{dead.Name} is died");
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
            if (controller == null)
            {
                Stop();
                return;
            }
            if (_controller != null)
            {
                _controller.Changed -= OnControllerChanged;
                _menu.PlayerGotInput -= OnPlayerGotInput;
            }
            _controller = controller;
            _menu = MenuFactory.Create(_controller);
            _controller.Changed += OnControllerChanged;
            _menu.PlayerGotInput += OnPlayerGotInput;
        }
    }
}
