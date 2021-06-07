using Models.Weapons;

namespace Models.Entities
{
    public sealed class Player : Entity
    {
        public Player(string name, AbilityBoard abilities, Weapon weapon) : base(name, abilities, weapon)
        {

        }
    }
}
