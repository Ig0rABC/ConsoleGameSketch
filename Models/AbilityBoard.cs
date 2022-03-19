using System;

namespace Models
{
    public class AbilityBoard
    {
        public float Strength { get; private set; }
        public float Accuracy { get; private set; }
        public float Magic { get; private set; }
        public static AbilityBoard Empty => new(0, 0, 0);

        private static readonly float MaxAbilityValue = 1;
        private static readonly float IncreasePerApply = 0.00625f;

        public AbilityBoard(float strength, float accuracy, float magic)
        {
            Strength = strength;
            Accuracy = accuracy;
            Magic = magic;
        }

        public void ApplyStrength()
        {
            if (Strength < MaxAbilityValue)
                Strength += IncreasePerApply;
        }

        public void ApplyAccuracy()
        {
            if (Accuracy < MaxAbilityValue)
                Strength += IncreasePerApply;
        }

        public void ApplyMagic()
        {
            if (Magic < MaxAbilityValue)
                Strength += IncreasePerApply;
        }
    }
}
