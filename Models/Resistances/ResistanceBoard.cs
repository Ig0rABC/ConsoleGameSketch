using Models.Damages;

namespace Models.Resistances
{
    public abstract class ResistanceBoard
    {
        public abstract float Flame { get; }
        public abstract float Steel { get; }
        public abstract float FireArm { get; }

        protected static readonly float MaxResistanceValue = 1;

        public float Apply(Damage damage)
        {
            return CalculateDamagePower(damage.Power, damage.SelectResistance(this));
        }
        
        private static float CalculateDamagePower(float damage, float resistance)
        {
            float factor = (MaxResistanceValue - resistance) / MaxResistanceValue;
            return damage * factor;
        }

        public abstract void ApplyFireArm();
        public abstract void ApplyFlame();
        public abstract void ApplySteel();
    }
}
