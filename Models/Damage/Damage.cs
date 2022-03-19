using System;
using Models.Resistances;

namespace Models.Damages
{
    public abstract class Damage
    {
        public float Power { get; private set; }

        public Damage(float power)
        {
            if (power <= 0)
                throw new ArgumentException("Damage power must be more than zero!", nameof(power));
            Power = power;
        }
        public static float CalculatePower(float power, float ability)
        {
            return power * ability * 2;
        }

        public abstract float SelectResistance(ResistanceBoard resistances);

        public abstract void ApplyResistance(ResistanceBoard resistances);
    }
}
