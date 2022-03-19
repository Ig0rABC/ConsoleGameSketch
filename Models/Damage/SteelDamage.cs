using Models.Resistances;

namespace Models.Damages
{
    public sealed class SteelDamage : Damage
    {
        public SteelDamage(float power) : base(power)
        {
        }

        public override float SelectResistance(ResistanceBoard resistances)
        {
            return resistances.Steel;
        }

        public override void ApplyResistance(ResistanceBoard resistances)
        {
            resistances.ApplySteel();
        }
    }
}
