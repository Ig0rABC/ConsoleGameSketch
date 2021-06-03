using Models.Weapons;

namespace Models.Entities
{
    public sealed class Goblin : Entity
    {
        public Goblin(Weapon weapon) : base("Goblin", 8, weapon)
        {

        }
    }
}
