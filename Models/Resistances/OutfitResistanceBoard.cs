using System;

namespace Models.Resistances
{
    public class OutfitResistanceBoard : ResistanceBoard
    {
        public static OutfitResistanceBoard Empty => new (0, 0, 0);

        private readonly float _flame;
        private readonly float _steel;
        private readonly float _fireArm;

        public OutfitResistanceBoard(float flame, float steel, float fireArm)
        {
            _flame = flame;
            _steel = steel;
            _fireArm = fireArm;
        }

        public override float GetFlame(StateBar outfitCondition)
        {
            return _flame * Math.Max(outfitCondition.Value, 0.125f);
        }

        public override float GetSteel(StateBar outfitCondition)
        {
            return _steel * Math.Max(outfitCondition.Value, 0.125f);
        }

        public override float GetFireArm(StateBar outfitCondition)
        {
            return _fireArm * Math.Max(outfitCondition.Value, 0.125f);
        }

        public override void ApplyFlame()
        {
        }

        public override void ApplySteel()
        {
        }

        public override void ApplyFireArm()
        {
        }
    }
}
