using Models.Resistances;

namespace Models.Damages
{
    public sealed class FlameDamage : Damage
    {
        public FlameDamage(float power) : base(power)
        {
        }

        public override float SelectResistance(ResistanceBoard resistances, StateBar health)
        {
            return resistances.GetFlame(health);
        }

        protected override void ApplyResistance(ResistanceBoard resistances)
        {
            resistances.ApplyFlame();
        }
    }
}
