using Models.Damages;
using Models.Entities;

namespace Models.Weapons
{
    public abstract class Weapon : InventoryItem
    {
        public virtual float Power { get; }

        public Weapon(string name, float power) : base(name)
        {
            Power = power;
        }

        public abstract Damage InstantiateDamage(Entity user);

        public abstract void Use(Entity user);

        public abstract bool CanUsed(Entity user);
    }
}
