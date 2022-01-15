using System;
using System.Collections.Generic;
using Models;
using Models.Battle;
using Models.Entities;
using Models.Weapons;

namespace Controllers
{
    public sealed class BattleController : Controller
    {

        public delegate void PlayerGotMoveHandler(Entity attacker, IEnumerable<Entity> enemies, IEnumerable<Entity> allias);
        public event PlayerGotMoveHandler PlayerGotMove;

        public delegate void AttackedHandler(Entity attacker, Entity target);
        public event AttackedHandler Attacked;

        public delegate void MissedHandler(Entity loser);
        public event MissedHandler Missed;

        public delegate void ChangedWeaponHandler(Entity entity, Weapon weapon);
        public event ChangedWeaponHandler ChangedWeapon;

        private Entity Attacker => _battle.Attacker;

        private readonly Battle _battle;
        private bool _isAuto;

        public BattleController(Battle battle) : base()
        {
            _battle = battle;
            _isAuto = false;
            _battle.Won += OnChange;
            _battle.Defeated += () => OnChange(null);
        }

        public BattleController(Battle battle, Controller next) : base(next)
        {
            _battle = battle;
            _isAuto = false;
            _battle.Won += OnChange;
            _battle.Defeated += () => OnChange(null);
        }

        public override void Update()
        {
            _battle.Next();
            if (Game.Guided.Contains(_battle.Attacker) && !_isAuto)
            {
                PlayerGotMove?.Invoke(_battle.Attacker, _battle.Enemies, _battle.Allies);
            }
            else
            {
                AutoMove();
            }
        }

        public void OpenInventory()
        {
            var controller = new InventoryController(Attacker.Inventory, this);
            OnChange(controller);
        }

        public void Attack(Entity target)
        {
            Attacked?.Invoke(Attacker, target);
            target.ApplyDamage(Attacker.Damage);
            Attacker.UseWeapon();
        }

        public void SwitchToAuto()
        {
            _isAuto = true;
            AutoMove();
        }

        public void AutoMove()
        {
            if (Attacker.CanAttack)
            {
                Attack(_battle.RelevantTarget);
                return;
            }
            var weapon = Attacker.Inventory.WeaponForAutoChange;
            if (weapon == null)
            {
                Missed?.Invoke(Attacker);
                return;
            }
            ChangedWeapon?.Invoke(Attacker, weapon);
            Attacker.Inventory.ActiveWeapon = weapon;
        }
    }
}
