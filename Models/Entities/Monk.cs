using Models.Resistances;

namespace Models.Entities
{
    public sealed class Monk : Entity
    {
        public Monk(Inventory inventory) : base("Monk", new AbilityBoard(0.35f, 0.5f, 0.4f), new EntityResistanceBoard(0.1f, 0.05f, 0.05f), inventory)
        {

        }
    }
}
