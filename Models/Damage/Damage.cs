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

        public void Apply(ResistanceBoard resistances, StateBar health)
        {
            var resistance = SelectResistance(resistances, health);
            ApplyResistance(resistances);
            var factor = CalculateDecreaseFactor(ResistanceBoard.MaxResistanceValue, resistance);
            Amplify(factor);
            health.Take(Power);
        }

        public abstract float SelectResistance(ResistanceBoard resistances, StateBar health);

        protected abstract void ApplyResistance(ResistanceBoard resistances);

        private static float CalculateDecreaseFactor(float maxResistanceValue, float resistance)
        {
            return (maxResistanceValue - resistance) / maxResistanceValue;
        }


        private void Amplify(float factor)
        {
            if (factor < 0)
                throw new ArgumentException("Damage power cannot be less than 0.", nameof(factor));
            Power *= factor;
        }
    }
}
