﻿using Models.Entities;
using Models.Damages;

namespace Models.Weapons
{
    public sealed class FlameStaff : MagicWeapon
    {
        public FlameStaff() : base("Flame Staff", 18, 10)
        {

        }

        public override Damages.Damage GetDamage(Entity user)
        {
            return new FlameDamage((byte)(Power + user.Abilities.Magic));
        }
    }
}
