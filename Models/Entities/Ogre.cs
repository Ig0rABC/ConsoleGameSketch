
namespace Models.Entities
{
    public sealed class Ogre : Entity
    {
        public Ogre(Inventory inventory) : base("Ogre", new AbilityBoard(11, 2, 4), new Resistances { Flame = 14, Steel = 9, FireArm = 32 }, inventory)
        {

        }
    }
}
