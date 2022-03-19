using Models.Resistances;

namespace Models.Entities
{
    public sealed class Goblin : Entity
    {
        public Goblin(Inventory inventory) : base("Goblin", new AbilityBoard(0.2f, 0.3f, 0.05f), new EntityResistanceBoard(0.08f, 0.05f, 0.05f), inventory)
        {

        }
    }
}
