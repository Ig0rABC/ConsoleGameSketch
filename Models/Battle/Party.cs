using System;
using System.Linq;
using Models.Entities;

namespace Models.Battle
{
    public class Party
    {
        public byte IterationsCount { get; private set; }
        
        public delegate void DefeatedHandler();
        public event DefeatedHandler Defeated;
        public Entity[] AliveMembers => _members.Where(m => m.IsAlive).ToArray();
        
        private readonly Entity[] _members;
        private byte _attackerIndex;

        public Party(Entity[] members)
        {
            _members = members;
            IterationsCount = 0;
            _attackerIndex = 0;
            foreach (var member in _members)
                member.Died += OnDied;
        }

        public Entity Next()
        {
            var attacker = AliveMembers[_attackerIndex++];
            if (_attackerIndex >= AliveMembers.Length)
            {
                IterationsCount++;
                _attackerIndex = 0;
            }
            return attacker;
        }

        public bool Has(Entity entity)
        {
            if (entity == null)
                throw new ArgumentException();
            return _members.Contains(entity);
        }

        public Entity Weakest
        {
            get
            {
                byte minHealth = AliveMembers.Min(m => m.Health);
                return AliveMembers.First(m => m.Health == minHealth);
            }
        }

        private void OnDied(Entity dead)
        {
            if (!AliveMembers.Any())
                Defeated?.Invoke();
            else if (_attackerIndex >= AliveMembers.Length)
                _attackerIndex = (byte)(AliveMembers.Length - 1);
        }
    }
}
