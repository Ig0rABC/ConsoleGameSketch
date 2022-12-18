using System.Collections.Generic;
using Models.Entities;
using Models.Effects;
using Controllers;
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
                View.Separate();
                _controller.Update();
            }
        }

        public void Stop()
        {
            _isRunning = false;
        }

        private void OnPlayerGotInput(IEnumerable<MenuOption> options)
        {
            var option = View.ChooseOption(options);
            option.Execute();
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
                View.GameOver();
                Stop();
                return;
            }
            _controller = controller;
            _menu = MenuFactory.Create(_controller);
            _controller.Changed += OnControllerChanged;
            _menu.PlayerGotInput += OnPlayerGotInput;
        }

        public void OnDamaged(Entity victim, float damage)
        {
            View.OnDamaged(victim, damage);
        }

        public void OnHealed(Entity entity, float recovery)
        {
            View.OnHealed(entity, recovery);
        }

        public void OnDied(Entity dead, float damage)
        {
            dead.Damaged -= OnDamaged;
            dead.Healed -= OnHealed;
            dead.Died -= OnDied;
            View.OnDied(dead, damage);
        }

        public void OnAffected(Entity target, Effect effect)
        {
            View.OnAffected(target, effect);
        }
    }
}
