using System.Collections.Generic;
using Models.Damages;
using Models.Entities;
using Models.Effects;

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

        public abstract IEnumerable<Effect> InstantiateEffects();

        public abstract void Use(Entity user);

        public abstract bool CanUsed(Entity user);
    }
}
