using System;
using Models.Entities;

namespace Models.Weapons
{
    public abstract class MagicWeapon : Weapon
    {
        public override float Power => base.Power * Level / 3;
        public byte Level => (byte)Math.Floor(_level);
        public float RequiredMana { get; }

        private float _level;

        public MagicWeapon(string name, float power, float requiredMana) : base(name, power)
        {
            _level = 1;
            RequiredMana = requiredMana;
        }

        public override void Use(Entity user)
        {
            user.Cast(RequiredMana);
            if (Level < 10)
                _level += 0.33f;
        }

        public override bool CanUsed(Entity user)
        {
            return user.Mana >= RequiredMana;
        }
    }
}
