﻿using System;
using System.Collections.Generic;
using Models.Damages;
using Models.Effects;

namespace Models.Weapons
{
    public sealed class Gunpowder : Ammo
    {
        public Gunpowder() : base("Gunpowder", 0.42f)
        {

        }

        public override Damage InstantiateDamage(float weaponPower)
        {
            return new FireArmDamage(Power + weaponPower);
        }

        public override IEnumerable<Effect> InstantiateEffects()
        {
            return Array.Empty<Effect>();
        }
    }
}
