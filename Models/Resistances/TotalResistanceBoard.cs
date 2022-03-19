
namespace Models.Resistances
{
    public class TotalResistanceBoard : ResistanceBoard
    {
        public override float Flame => _entityResistances.Flame;
        public override float Steel => _entityResistances.Steel;
        public override float FireArm => _entityResistances.FireArm;

        private readonly EntityResistanceBoard _entityResistances;
        private readonly Inventory _inventory;

        public TotalResistanceBoard(EntityResistanceBoard entityResistances, Inventory inventory)
        {
            _entityResistances = entityResistances;
            _inventory = inventory;
        }

        public override void ApplyFlame()
        {
            _entityResistances.ApplyFlame();
        }

        public override void ApplySteel()
        {
            _entityResistances.ApplySteel();
        }

        public override void ApplyFireArm()
        {
            _entityResistances.ApplyFireArm();
        }
    }
}
