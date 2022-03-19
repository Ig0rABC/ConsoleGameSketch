using Models.Resistances;

namespace Models.Damages
{
    public sealed class FireArmDamage : Damage
    {
        public FireArmDamage(float power) : base(power)
        {
        }

        public override float SelectResistance(ResistanceBoard resistances)
        {
            return resistances.FireArm;
        }

        public override void ApplyResistance(ResistanceBoard resistances)
        {
            resistances.ApplyFireArm();
        }
    }
}
