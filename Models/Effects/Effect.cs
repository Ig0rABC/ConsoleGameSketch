using System;
using Models.Entities;

namespace Models.Effects
{
    public abstract class Effect
    {
        public byte Duration { get; private set; }
        public float Power => _initialPower / (_initialDuration - Duration + 1);
        public bool IsOver => Duration < 1;

        private readonly byte _initialDuration;
        private readonly float _initialPower;

        public Effect(byte duration, float power)
        {
            _initialDuration = duration;
            _initialPower = power;
            Duration = _initialDuration;
        }

        public void Apply(Entity target)
        {
            if (IsOver)
                throw new InvalidOperationException("Effect cannot be applied when its duration expires.");
            Duration--;
            ApplySelf(target, Power);
        }

        protected abstract void ApplySelf(Entity target, float power);
    }
}
