using System.Collections.Generic;
using Models.Damages;
using Models.Effects;

namespace Models.Weapons
{
    public abstract class Ammo : InventoryItem
    {
        public float Power { get; }

        public Ammo(string name, float power) : base(name)
        {
            Power = power;
        }

        public abstract Damage InstantiateDamage(float weaponPower);
        public abstract IEnumerable<Effect> InstantiateEffects();
    }
}
