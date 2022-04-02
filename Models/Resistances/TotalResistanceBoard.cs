
namespace Models.Resistances
{
    public class TotalResistanceBoard : ResistanceBoard
    {
        public override float Flame => _entityResistances.Flame + Armor.Flame;
        public override float Steel => _entityResistances.Steel + Armor.Steel;
        public override float FireArm => _entityResistances.FireArm + Armor.FireArm;

        private ResistanceBoard Armor => _inventory.Outfit is null ? OutfitResistanceBoard.Empty : _inventory.Outfit.Resistances;
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
            Armor?.ApplyFlame();
        }

        public override void ApplySteel()
        {
            _entityResistances.ApplySteel();
            Armor?.ApplySteel();
        }

        public override void ApplyFireArm()
        {
            _entityResistances.ApplyFireArm();
            Armor?.ApplyFireArm();
        }
    }
}
