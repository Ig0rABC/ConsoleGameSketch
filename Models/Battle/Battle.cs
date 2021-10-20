using Models.Entities;

namespace Models.Battle
{
    public class Battle
    {
        public Entity Attacker { get; private set; }

        public delegate void EndedHandler();
        public event EndedHandler Defeated;
        public event EndedHandler Won;

        public Entity[] Enemies => Opposition.AliveMembers;
        public Entity[] Allies
        {
            get
            {
                if (_allies.IterationsCount == 0)
                    return _allies.AliveMembers;
                else
                    return (_allies.Has(Attacker) ? _allies : _enemies).AliveMembers;
            }
        }

        public Entity SuitableVictim => Opposition.ChooseVictim(Attacker);

        private readonly Party _allies;
        private readonly Party _enemies;

        public Battle(Entity[] allies, Entity[] enemies)
        {
            _allies = new Party(allies);
            _enemies = new Party(enemies);
            _allies.Defeated += OnDefeated;
            _enemies.Defeated += OnWon;
        }

        public Entity Next()
        {
            Attacker = MovingParty.Next();
            return Attacker;
        }

        private Party Opposition => _allies.Has(Attacker) ? _enemies : _allies;
        private Party MovingParty => _allies.IterationsCount == _enemies.IterationsCount ? _allies : _enemies;

        private void OnDefeated()
        {
            Defeated?.Invoke();
        }

        private void OnWon()
        {
            Won?.Invoke();
        }
    }
}
