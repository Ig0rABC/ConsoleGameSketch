using System;
using System.Linq;
using System.Collections.Generic;
using Models.Entities;

namespace Models.Battle
{
    public class Party
    {
        public byte IterationsCount { get; private set; }
        
        public delegate void DefeatedHandler();
        public event DefeatedHandler Defeated;
        public IEnumerable<Entity> AliveMembers => _members.Where(m => m.IsAlive);
        
        private readonly IEnumerable<Entity> _members;
        private byte _attackerIndex;

        public Party(IEnumerable<Entity> members)
        {
            _members = members;
            _attackerIndex = 0;
            IterationsCount = 0;
            foreach (var member in _members)
                member.Died += OnDied;
        }

        public Entity Next()
        {
            var attacker = AliveMembers.ElementAt(_attackerIndex++);
            if (_attackerIndex >= AliveMembers.Count())
            {
                StartNewIteration();
            }
            return attacker;
        }

        public bool Has(Entity entity)
        {
            if (entity == null)
                throw new ArgumentException();
            return _members.Contains(entity);
        }

        public Entity FindRelevantTarget(Entity attacker)
        {
            Entity victim = AliveMembers.First();
            foreach (var member in AliveMembers.Skip(1))
                if (member.Health > victim.Health)
                    victim = member;
            return victim;
        }

        private void StartNewIteration()
        {
            _attackerIndex = 0;
            IterationsCount++;
        }

        private void OnDied(Entity dead)
        {
            if (!AliveMembers.Any())
                Defeated?.Invoke();
        }
    }
}
