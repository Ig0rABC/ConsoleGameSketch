using Models.Damages;

namespace Models.Effects
{
    public class Burning : DamageEffect<FlameDamage>
    {
        public Burning(byte count, float power) : base(count, power)
        {

        }
    }
}
