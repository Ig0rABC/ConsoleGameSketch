using System;
using Models.Entities;
using Models.Damages;

namespace Models.Weapons
{
    public abstract class MeleeWeapon : Weapon
    {
        public static readonly float MinDecreasePowerCondition = 0.375f;
        public override byte Power => (byte)(base.Power * Math.Max(Condition, MinDecreasePowerCondition));
        public float Condition { get; private set; }

        public MeleeWeapon(string name, byte power) : base(name, power)
        {
            Condition = 1;
        }

        public override Damage GetDamage(Entity user)
        {
            var power = (byte)(Power + user.Abilities.Strength);
            return new SteelDamage(power);
        }

        public override void Use(Entity user)
        {
            user.Abilities.ApplyStrength();
            if (Condition > 0)
                Condition -= 0.125f;
        }

        public override bool CanUsed(Entity user)
        {
            return true;
        }
    }
}
