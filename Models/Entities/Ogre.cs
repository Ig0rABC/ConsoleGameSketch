
namespace Models.Entities
{
    public sealed class Ogre : Entity
    {
        public Ogre(Inventory inventory) : base("Ogre", new AbilityBoard(11, 2, 4), inventory)
        {

        }
    }
}
