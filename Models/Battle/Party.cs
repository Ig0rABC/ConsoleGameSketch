using System.Linq;
using System.Collections.Generic;
using Models.Entities;

namespace Models.Battle
{
    public class Party : IUpdatable
    {
        public byte IterationsCount { get; private set; }
        
        public delegate void DefeatedHandler();
        public event DefeatedHandler Defeated;
        public IEnumerable<Entity> AliveMembers => _members.Where(m => !m.Health.IsEmpty());

        public Entity Current => AliveMembers.ElementAt(_index);
        
        private readonly IEnumerable<Entity> _members;
        private byte _index;

        public Party(IEnumerable<Entity> members)
        {
            _members = members;
            _index = 0;
            IterationsCount = 0;
            foreach (var member in _members)
                member.Died += OnDied;
        }

        public void Update()
        {
            foreach (var entity in _members)
                entity.Update();
        }

        public void MoveNext()
        {
            if (++_index >= AliveMembers.Count())
                StartNewIteration();
        }

        private void StartNewIteration()
        {
            _index = 0;
            IterationsCount++;
        }

        private void OnDied(Entity dead, float damage)
        {
            if (!AliveMembers.Any())
            {
                Defeated?.Invoke();
                Unsubscribe();
            }
        }

        private void Unsubscribe()
        {
            foreach (var member in _members)
                member.Died -= OnDied;
        }
    }
}
