using Models.Weapons;

namespace Models.Entities
{
    public sealed class Goblin : Entity
    {
        public Goblin(Weapon weapon) : base("Goblin", new AbilityBoard(8, 4, 2), weapon)
        {

        }
    }
}
