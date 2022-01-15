
namespace Models.Entities
{
    public sealed class Monk : Entity
    {
        public Monk(Inventory inventory) : base("Monk", new AbilityBoard(16, 9, 12), new Resistances { Flame = 7, Steel = 5, FireArm = 1 }, inventory)
        {

        }
    }
}
