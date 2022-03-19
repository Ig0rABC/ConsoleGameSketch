
namespace Models.Resistances
{
    public class EntityResistanceBoard : ResistanceBoard
    {
        public override float Flame => _flame;
        public override float Steel => _steel;
        public override float FireArm => _fireArm;
        public static EntityResistanceBoard Empty => new(0, 0, 0);

        private float _flame;
        private float _steel;
        private float _fireArm;

        private static readonly float IncreasePerApply = 0.003125f;

        public EntityResistanceBoard(float flame, float steel, float fireArm)
        {
            _flame = flame;
            _steel = steel;
            _fireArm = fireArm;
        }

        public override void ApplyFlame()
        {
            if (_flame < MaxResistanceValue)
                _flame += IncreasePerApply;
        }

        public override void ApplySteel()
        {
            if (_steel < MaxResistanceValue)
                _steel += IncreasePerApply;
        }

        public override void ApplyFireArm()
        {
            if (_fireArm < MaxResistanceValue)
                _fireArm += IncreasePerApply;
        }
    }
}