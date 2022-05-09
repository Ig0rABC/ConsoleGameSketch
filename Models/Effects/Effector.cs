using System;
using System.Linq;
using System.Collections.Generic;
using Models.Entities;

namespace Models.Effects
{
    public class Effector
    {
        public delegate void AddedHandler(Entity target, Effect effect);
        public event AddedHandler Added;

        public delegate void AppliedHandler(Entity target, Effect effect);
        public event AppliedHandler Applied;

        private readonly Entity _owner;
        private readonly List<Effect> _effects;

        public Effector(Entity owner)
        {
            _owner = owner;
            _effects = new();
        }

        public void ApplyEffects(Entity target)
        {
            foreach (var effect in _effects.ToArray())
            {
                if (effect.IsOver)
                {
                    _effects.Remove(effect);
                }
                else
                {
                    effect.Apply(target);
                    Applied?.Invoke(_owner, effect);
                }
            }
        }

        public void Add(Effect effect)
        {
            var stocked = FindSimilar(effect);
            if (stocked is null == false && stocked.Power < effect.Power)
                Override(stocked, effect);
            else
                _effects.Add(effect);
            Added?.Invoke(_owner, effect);
        }

        public void Clear<T>()
        {
            _effects.RemoveAll(effect => effect is T);
        }

        private Effect FindSimilar(Effect effect)
        {
            return _effects.FirstOrDefault(e => e.GetType() == effect.GetType());  
        }

        private void Override(Effect from, Effect to)
        {
            var index = _effects.IndexOf(from);
            if (index == -1)
                throw new InvalidOperationException();
            _effects[index] = to;
        }
    }
}
