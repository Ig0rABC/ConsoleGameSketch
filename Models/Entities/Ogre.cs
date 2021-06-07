using Models.Weapons;

namespace Models.Entities
{
    public sealed class Ogre : Entity
    {
        public Ogre(Weapon weapon) : base("Ogre", new AbilityBoard(11, 2, 4), weapon)
        {

        }
    }
}
