
using Models.Entities;

namespace Models.Weapons
{
    public abstract class Weapon
    {
        public string Name { get; }

        private readonly byte _damage;

        public Weapon(string name, byte damage)
        {
            Name = name;
            _damage = damage;
        }

        public virtual byte GetDamage(AbilityBoard userAbilities)
        {
            return _damage;
        }

        public abstract void Use(Entity user);

        public abstract bool CanUsed(Entity user);
    }
}
