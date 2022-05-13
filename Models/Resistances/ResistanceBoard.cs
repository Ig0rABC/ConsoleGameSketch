
namespace Models.Resistances
{
    public abstract class ResistanceBoard
    {
        public static readonly float MaxResistanceValue = 1;

        public abstract float GetFlame(StateBar health);
        public abstract float GetSteel(StateBar health);
        public abstract float GetFireArm(StateBar health);
        
        public abstract void ApplyFireArm();
        public abstract void ApplyFlame();
        public abstract void ApplySteel();
    }
}
