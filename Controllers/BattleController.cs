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

        private readonly Battle _battle;
        private bool _isAuto;

        public BattleController(Battle battle) : this(battle, null)
        {
        }

        public BattleController(Battle battle, Controller next) : base(next)
        {
            _battle = battle;
            _isAuto = false;
            _battle.Won += OnWon;
            _battle.Defeated += OnDefeated;
        }

        public override void Update()
        {
            Entity attacker = _battle.MovingParty.Current;
            if (Game.Controlled.Contains(attacker) && !_isAuto)
            {
                var enemies = _battle.TargetParty.AliveMembers;
                var allies = _battle.MovingParty.AliveMembers;
                PlayerGotMove?.Invoke(attacker, enemies, allies);
            }
            else
            {
                AutoMove(attacker);
            }
            _battle.Update();
            _battle.MoveNext();
        }

        public void OpenInventory(Entity owner)
        {
            var controller = new InventoryController(owner.Inventory, owner, this);
            OnChange(controller);
        }

        public void Attack(Entity attacker, Entity target)
        {
            Attacked?.Invoke(attacker, target);
            var damage = attacker.Inventory.ActiveWeapon.InstantiateDamage(attacker);
            var effects = attacker.Inventory.ActiveWeapon.InstantiateEffects();
            foreach (var effect in effects)
                target.Apply(effect);
            target.Apply(damage);
            attacker.UseWeapon();
        }

        public void SwitchToAuto(Entity attacker)
        {
            _isAuto = true;
            AutoMove(attacker);
        }

        public void AutoMove(Entity attacker)
        {
            if (attacker.CanAttack)
            {
                Entity target = _battle.FindRelevantTarget(attacker);
                Attack(attacker, target);
                return;
            }
            var weapon = attacker.Inventory.GetWeaponForAutoChange(attacker);
            if (weapon == null)
            {
                Missed?.Invoke(attacker);
                return;
            }
            ChangedWeapon?.Invoke(attacker, weapon);
            attacker.Inventory.ActiveWeapon = weapon;
        }

        private void OnWon()
        {
            OnChange();
            Unsubscribe();
        }

        private void OnDefeated()
        {
            OnChange(null);
            Unsubscribe();
        }

        private void Unsubscribe()
        {
            _battle.Won -= OnWon;
            _battle.Defeated -= OnDefeated;
        }
    }
}
