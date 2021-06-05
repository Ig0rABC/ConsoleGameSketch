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
            if ((_battle.Attacker.Weapon is RangedWeapon && _battle.Attacker.Ammo == 0)
                || (_battle.Attacker.Weapon is MagicWeapon && _battle.Attacker.Mana < (_battle.Attacker.Weapon as MagicWeapon).RequiredMana))
                Missed?.Invoke(_battle.Attacker);
            else if (_battle.Attacker is Player && !_isAuto)
                PlayerGotMove?.Invoke(_battle.Enemies);
            else
               AutoAttack();
        }

        public void Attack(Entity victim)
        {
            Attacked?.Invoke(_battle.Attacker, victim);
            if (_battle.Attacker.Weapon is RangedWeapon)
                _battle.Attacker.Ammo--;
            if (_battle.Attacker.Weapon is MagicWeapon)
                _battle.Attacker.Mana -= (_battle.Attacker.Weapon as MagicWeapon).RequiredMana;
            victim.ApplyDamage(_battle.Attacker.Damage);
            _battle.Attacker.Weapon.Use();
        }

        public void SwitchToAuto()
        {
            _isAuto = true;
            AutoAttack();
        }

        public void AutoAttack()
        {
            Attack(_battle.WeakestEnemy);
        }
    }
}
