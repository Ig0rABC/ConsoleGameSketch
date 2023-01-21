using Models.Entities;

namespace Models.Effects
{
    public class Healing : TempEffect
    {
        public readonly float Power;

        public Healing(byte duration, float power, byte delay = 0) : base(duration, delay)
        {
            Power = power;
        }

        protected override void OnTick(Entity target)
        {
            if (target.Alive)
                target.Heal(Power);
        }
    }
}
