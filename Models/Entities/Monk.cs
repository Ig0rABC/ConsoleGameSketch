using Models.Weapons;

namespace Models.Entities
{
    public sealed class Monk : Entity
    {
        public Monk(Weapon weapon) : base("War Monk", 16, weapon)
        {

        }
    }
}
