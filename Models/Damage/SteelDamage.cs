using Models.Entities;

namespace Models.Damages
{
    public sealed class SteelDamage : Damage
    {
        public SteelDamage(byte power) : base(power)
        {
        }

        public override byte SelectResistance(Resistances resistances)
        {
            return resistances.Steel;
        }

        public override void ApplyResistance(Resistances resistances)
        {
            resistances.ApplySteel();
        }
    }
}
