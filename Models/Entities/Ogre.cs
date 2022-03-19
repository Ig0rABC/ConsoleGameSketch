using Models.Resistances;

namespace Models.Entities
{
    public sealed class Ogre : Entity
    {
        public Ogre(Inventory inventory) : base("Ogre", new AbilityBoard(0.48f, 0.2f, 0.1f), new EntityResistanceBoard(0.3f, 0.1f, 0.2f), inventory)
        {

        }
    }
}
