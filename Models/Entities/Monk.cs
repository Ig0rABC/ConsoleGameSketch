
namespace Models.Entities
{
    public sealed class Monk : Entity
    {
        public Monk(Inventory inventory) : base("Monk", new AbilityBoard(16, 9, 12), inventory)
        {

        }
    }
}
