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

        private readonly List<Effect> _effects;

        public Effector(List<Effect> effects)
        {
            _effects = effects;
        }

        public void Update(Entity target)
        {
            foreach (var effect in _effects.OrderByDescending(e => e.Priority))
            {
                effect.Update();
                Applied?.Invoke(target, effect);
            }
        }

        public void Add(Entity target, Effect effect)
        {
            var stocked = FindSimilar(effect);
            if (stocked is null == false)
            {
                if (stocked.Priority > effect.Priority)
                {
                    return;
                }
                Override(stocked, effect);
            }
            else
            {
                _effects.Add(effect);
            }
            effect.Apply(target);
            effect.Expired += OnExpired;
            Added?.Invoke(target, effect);
        }

        public void Clear<T>() where T : Effect
        {
            foreach (var effect in _effects.Where(e => e is T))
                effect.Dispose();
        }

        private void OnExpired(Effect effect)
        {
            _effects.Remove(effect);
            effect.Expired -= OnExpired;
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
