using Models.Entities;

namespace Models.Damages
{
    public sealed class FlameDamage : Damage
    {
        public FlameDamage(byte power) : base(power)
        {
        }

        public override byte SelectResistance(Resistances resistances)
        {
            return resistances.Flame;
        }

        public override void ApplyResistance(Resistances resistances)
        {
            resistances.ApplyFlame();
        }
    }
}
