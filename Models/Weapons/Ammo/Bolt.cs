using System;
using System.Collections.Generic;
using Models.Damages;
using Models.Effects;

namespace Models.Weapons
{
    public sealed class Bolt : Ammo
    {
        public Bolt() : base("Crossbow Bolt", 0.28f)
        {

        }

        public override Damage InstantiateDamage(float weaponPower)
        {
            return new SteelDamage(Power + weaponPower);
        }

        public override IEnumerable<Effect> InstantiateEffects()
        {
            return Array.Empty<Effect>();
        }
    }
}
