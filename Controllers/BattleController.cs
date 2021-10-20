using System;
using Models;
using Models.Battle;
using Models.Entities;
using Models.Weapons;

namespace Controllers
{
    public sealed class BattleController : Controller
    {

        public delegate void PlayerGotMoveHandler(Entity attacker, Entity[] enemies, Entity[] allias);
        public event PlayerGotMoveHandler PlayerGotMove;

        public delegate void AttackedHandler(Entity attacker, Entity victim);
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
        }

        public BattleController(Battle battle, Controller next) : base(next)
        {
            _battle = battle;
            _isAuto = false;
            _battle.Won += OnChange;
        }

        public override void Update()
        {
            _battle.Next();
            if (Game.IsGuided(_battle.Attacker) && !_isAuto)
            {
                var enemies = Attacker.CanAttack ? _battle.Enemies : Array.Empty<Entity>();
                PlayerGotMove?.Invoke(_battle.Attacker, enemies, _battle.Allies);
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

        public void Attack(Entity victim)
        {
            Attacked?.Invoke(Attacker, victim);
            victim.ApplyDamage(Attacker.Damage);
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
                Attack(_battle.SuitableVictim);
                return;
            }
            try
            {
                var weapon = Attacker.Inventory.WeaponForAutoChange;
                ChangedWeapon?.Invoke(Attacker, weapon);
                Attacker.Inventory.ActiveWeapon = weapon;
            }
            catch
            {
                Missed?.Invoke(Attacker);
            }
        }
    }
}
