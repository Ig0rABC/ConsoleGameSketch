using Models.Damages;

namespace Models.Entities
{
    public class Resistances
    {
        public byte Flame;
        public byte Steel;
        public byte FireArm;

        public static readonly byte MaxResistanceValue = 100;

        public static Resistances Empty => new() { Flame = 0, Steel = 0, FireArm = 0 };

        public static byte CalculateDamagePower(decimal damage, decimal resistance)
        {
            decimal factor = (MaxResistanceValue - resistance) / MaxResistanceValue;
            return (byte)(damage * factor);
        }

        public byte Apply(Damage damage)
        {
            return CalculateDamagePower(damage.Power, damage.SelectResistance(this));
        }

        public void ApplyFlame()
        {
            Flame++;
        }

        public void ApplySteel()
        {
            Steel++;
        }

        public void ApplyFireArm()
        {
            FireArm++;
        }
    }
}