using System;
using Models.Entities;
using Models.Damages;

namespace Models.Weapons
{
    public abstract class MeleeWeapon : Weapon
    {
        public override float Power => base.Power * Math.Max(Condition.Value, 0.375f);
        public readonly StateBar Condition;

        public MeleeWeapon(string name, float power) : base(name, power)
        {
            Condition = new StateBar();
        }

        public override Damage InstantiateDamage(Entity user)
        {
            var power = Damage.CalculatePower(Power, user.Abilities.Strength);
            return new SteelDamage(power);
        }

        public override void Use(Entity user)
        {
            user.Abilities.ApplyStrength();
            Condition.Take(0.125f);
        }

        public override bool CanUsed(Entity user)
        {
            return true;
        }
    }
}
