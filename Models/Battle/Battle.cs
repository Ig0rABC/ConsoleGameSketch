using System.Collections.Generic;
using Models.Entities;

namespace Models.Battle
{
    public class Battle
    {
        public Entity Attacker { get; private set; }

        public delegate void EndedHandler();
        public event EndedHandler Defeated;
        public event EndedHandler Won;

        public IEnumerable<Entity> Enemies => VictimParty.AliveMembers;
        public IEnumerable<Entity> Allies
        {
            get
            {
                if (_playerParty.IterationsCount == 0)
                    return _playerParty.AliveMembers;
                else
                    return (_playerParty.Has(Attacker) ? _playerParty : _npcParty).AliveMembers;
            }
        }

        public Entity SuitableVictim => VictimParty.ChooseVictim(Attacker);

        private readonly Party _playerParty;
        private readonly Party _npcParty;

        public Battle(Party playerParty, Party npcParty)
        {
            _playerParty = playerParty;
            _npcParty = npcParty;
            _playerParty.Defeated += OnDefeated;
            _npcParty.Defeated += OnWon;
        }

        public Entity Next()
        {
            Attacker = AttackerParty.Next();
            return Attacker;
        }

        private Party VictimParty => _playerParty.Has(Attacker) ? _npcParty : _playerParty;
        private Party AttackerParty => _playerParty.IterationsCount == _npcParty.IterationsCount
            ? _playerParty
            : _npcParty;

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
