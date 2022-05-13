using Models.Resistances;

namespace Models.Damages
{
    public sealed class SteelDamage : Damage
    {
        public SteelDamage(float power) : base(power)
        {
        }

        public override float SelectResistance(ResistanceBoard resistances, StateBar health)
        {
            return resistances.GetSteel(health);
        }

        protected override void ApplyResistance(ResistanceBoard resistances)
        {
            resistances.ApplySteel();
        }
    }
}
