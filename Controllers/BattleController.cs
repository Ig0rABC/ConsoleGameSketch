using System;
using Models.Battle;
using Models.Entities;
using Models.Weapons;

namespace Controllers
{
    public sealed class BattleController : Controller
    {

        public delegate void PlayerGotMoveHandler(Entity[] enemies);
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
            if (_battle.Next() is Player && !_isAuto)
            {
                var enemies = Attacker.CanAttack ? _battle.Enemies : Array.Empty<Entity>();
                PlayerGotMove?.Invoke(enemies);
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
                Attacker.Inventory.TakeWeaponWhichCanUsed();
                ChangedWeapon?.Invoke(Attacker, Attacker.Inventory.ActiveWeapon);
            }
            catch
            {
                Missed?.Invoke(Attacker);
            }
        }
    }
}
