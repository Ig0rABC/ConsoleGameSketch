using System;
using System.Collections.Generic;
using Models.Damages;
using Models.Effects;

namespace Models.Weapons
{
    public sealed class Arrow : Ammo
    {
        public Arrow() : base("Arrow", 0.19f)
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
