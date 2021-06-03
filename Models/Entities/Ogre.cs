using Models.Weapons;

namespace Models.Entities
{
    public sealed class Ogre : Entity
    {
        public Ogre(Weapon weapon) : base("Ogre", 11, weapon)
        {

        }
    }
}
