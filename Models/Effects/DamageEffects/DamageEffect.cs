using System;
using Models.Entities;
using Models.Damages;

namespace Models.Effects
{
    public class DamageEffect<T> : TempEffect where T : Damage
    {
        public readonly float Power;

        public DamageEffect(byte duration, float power, byte delay = 1) : base(duration, delay)
        {
            Power = power;
        }

        protected override void OnTick(Entity target)
        {
            var damage = (Damage)Activator.CreateInstance(typeof(T), Power);
            if (!target.Health.IsEmpty())
                target.Apply(damage);
        }
    }
}
