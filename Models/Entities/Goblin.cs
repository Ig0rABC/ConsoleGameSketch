
namespace Models.Entities
{
    public sealed class Goblin : Entity
    {
        public Goblin(Inventory inventory) : base("Goblin", new AbilityBoard(8, 4, 2), new Resistances { Flame = 8, Steel = 5, FireArm = 1 }, inventory)
        {

        }
    }
}
