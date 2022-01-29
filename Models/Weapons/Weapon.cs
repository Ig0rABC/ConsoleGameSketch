using Models.Damages;
using Models.Entities;

namespace Models.Weapons
{
    public abstract class Weapon : InventoryItem
    {
        public virtual byte Power { get; }

        public Weapon(string name, byte power) : base(name)
        {
            Power = power;
        }

        public abstract Damage InstantiateDamage(Entity user);

        public abstract void Use(Entity user);

        public abstract bool CanUsed(Entity user);
    }
}
