using Models.Weapons;

namespace Models.Entities
{
    public sealed class Player : Entity
    {
        public Player(string name, byte strength, Weapon weapon) : base(name, strength, weapon)
        {

        }
    }
}
