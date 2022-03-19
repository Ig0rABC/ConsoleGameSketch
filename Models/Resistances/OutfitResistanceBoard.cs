using System;

namespace Models.Resistances
{
    class OutfitResistanceBoard : ResistanceBoard
    {
        public override float Flame => _flame * Math.Max(_outfitCondition.Value, 0.125f);
        public override float Steel => _steel * Math.Max(_outfitCondition.Value, 0.125f);
        public override float FireArm => _fireArm * Math.Max(_outfitCondition.Value, 0.125f);

        private StateBar _outfitCondition;

        private readonly float _flame;
        private readonly float _steel;
        private readonly float _fireArm;

        public OutfitResistanceBoard(float flame, float steel, float fireArm)
        {
            _flame = flame;
            _steel = steel;
            _fireArm = fireArm;
        }

        public void SetOutfitCondition(StateBar outfitCondition)
        {
            _outfitCondition = outfitCondition;
        }

        public override void ApplyFlame()
        {
            DecreaseOutfitCondition(_flame);
        }

        public override void ApplySteel()
        {
            DecreaseOutfitCondition(_steel);
        }

        public override void ApplyFireArm()
        {
            DecreaseOutfitCondition(_fireArm);
        }

        private void DecreaseOutfitCondition(float resistance)
        {
            if (!_outfitCondition.IsEmpty())
            {
                // TODO: Calculate decrease value by specific resistance
                _outfitCondition.Take(0.03125f);
            }
        }
    }
}
