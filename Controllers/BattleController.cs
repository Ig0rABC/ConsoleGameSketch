using Models.Battle;
using Models.Entities;

namespace Controllers
{
    public sealed class BattleController : Controller
    {
        public Battle Battle { get; }

        public delegate void PlayerGotMoveHandler(Entity[] enemies);
        public event PlayerGotMoveHandler PlayerGotMove;

        public delegate void AttackedHandler(Entity attacker, Entity victim);
        public event AttackedHandler Attacked;

        private bool _isAuto;

        public BattleController(Battle battle) : base()
        {
            Battle = battle;
            _isAuto = false;
            Battle.Won += OnChange;
        }

        public BattleController(Battle battle, Controller next) : base(next)
        {
            Battle = battle;
            _isAuto = false;
            Battle.Won += OnChange;
        }

        public override void Update()
        {
            if (Battle.Next() is Player && !_isAuto)
                PlayerGotMove?.Invoke(Battle.Enemies);
            else
               AutoAttack();
        }

        public void Attack(Entity victim)
        {
            Attacked?.Invoke(Battle.Attacker, victim);
            victim.ApplyDamage(Battle.Attacker.Strength);
        }

        public void SwitchToAuto()
        {
            _isAuto = true;
            AutoAttack();
        }

        public void AutoAttack()
        {
            Attack(Battle.WeakestEnemy);
        }
    }
}
