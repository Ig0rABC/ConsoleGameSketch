using Models.Resistances;

namespace Models.Damages
{
    public sealed class FireArmDamage : Damage
    {
        public FireArmDamage(float power) : base(power)
        {
        }

        public override float SelectResistance(ResistanceBoard resistances, StateBar health)
        {
            return resistances.GetFireArm(health);
        }

        protected override void ApplyResistance(ResistanceBoard resistances)
        {
            resistances.ApplyFireArm();
        }
    }
}
