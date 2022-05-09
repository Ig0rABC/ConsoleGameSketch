using System;
using Models.Entities;
using Models.Damages;

namespace Models.Effects
{
    public class DamageEffect<T> : Effect where T : Damage
    {
        public DamageEffect(byte count, float power) : base(count, power)
        {
        }

        protected override void ApplySelf(Entity target, float power)
        {
            var damage = (Damage)Activator.CreateInstance(typeof(T), power);
            target.ApplyDamage(damage);
        }
    }
}
