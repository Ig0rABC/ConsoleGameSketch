
namespace Models
{
    public class AbilityBoard
    {
        public byte Strength { get; private set; }
        public byte Accuracy { get; private set; }
        public byte Magic { get; private set; }
        public static AbilityBoard Empty => new(0, 0, 0);

        public AbilityBoard(byte strength, byte accuracy, byte magic)
        {
            Strength = strength;
            Accuracy = accuracy;
            Magic = magic;
        }

        public void ApplyStrength()
        {
            Strength++;
        }

        public void ApplyAccuracy()
        {
            Accuracy++;
        }

        public void ApplyMagic()
        {
            Magic++;
        }
    }
}
