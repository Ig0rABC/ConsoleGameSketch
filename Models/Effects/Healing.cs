using Models.Entities;

namespace Models.Effects
{
    public class Healing : Effect
    {
        public Healing(byte count, float power) : base(count, power)
        {

        }

        protected override void ApplySelf(Entity target, float power)
        {
            target.Heal(power);
        }
    }
}
