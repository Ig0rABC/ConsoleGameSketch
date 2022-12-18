using Models.Damages;

namespace Models.Effects
{
    public class Burning : DamageEffect<FlameDamage>
    {
        public Burning(byte count, float power, byte delay = 1) : base(count, power, delay)
        {

        }
    }
}
