using Models.Entities;

namespace Models.Battle
{
    public class Battle
    {
        public delegate void EndedHandler();
        public event EndedHandler Defeated;
        public event EndedHandler Won;

        public Party TargetParty => MovingParty == _playerParty ? _npcParty : _playerParty;
        public Party MovingParty => _playerParty.IterationsCount == _npcParty.IterationsCount
            ? _playerParty
            : _npcParty;

        private readonly Party _playerParty;
        private readonly Party _npcParty;

        public Battle(Party playerParty, Party npcParty)
        {
            _playerParty = playerParty;
            _npcParty = npcParty;
            _playerParty.Defeated += OnDefeated;
            _npcParty.Defeated += OnWon;
        }

        public void MoveNext()
        {
            MovingParty.MoveNext();
        }

        public Entity FindRelevantTarget(Entity attacker)
        {
            return TargetParty.FindRelevantTarget(attacker);
        }

        private void OnWon()
        {
            Won?.Invoke();
            Unsubscribe();
        }

        private void OnDefeated()
        {
            Defeated?.Invoke();
            Unsubscribe();
        }

        private void Unsubscribe()
        {
            _playerParty.Defeated -= OnDefeated;
            _npcParty.Defeated -= OnWon;
        }
    }
}
