using Models.Weapons;

namespace Models.Entities
{
    public sealed class Monk : Entity
    {
        public Monk(Weapon weapon) : base("War Monk", new AbilityBoard(16, 9, 12), weapon)
        {

        }
    }
}
