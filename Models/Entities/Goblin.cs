
namespace Models.Entities
{
    public sealed class Goblin : Entity
    {
        public Goblin(Inventory inventory) : base("Goblin", new AbilityBoard(8, 4, 2), inventory)
        {

        }
    }
}
