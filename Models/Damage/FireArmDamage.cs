using Models.Entities;

namespace Models.Damages
{
    public sealed class FireArmDamage : Damage
    {
        public FireArmDamage(byte power) : base(power)
        {
        }

        public override byte SelectResistance(Resistances resistances)
        {
            return resistances.FireArm;
        }

        public override void ApplyResistance(Resistances resistances)
        {
            resistances.ApplyFireArm();
        }
    }
}
