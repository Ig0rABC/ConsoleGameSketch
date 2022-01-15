using System;
using Models.Entities;

namespace Models.Weapons
{
    public abstract class MagicWeapon : Weapon
    {
        public override byte Power => (byte)(base.Power + Level);
        public byte Level => (byte)Math.Floor(_level);
        public byte RequiredMana { get; }

        private float _level;

        public MagicWeapon(string name, byte power, byte requiredMana) : base(name, power)
        {
            _level = 1;
            RequiredMana = requiredMana;
        }

        public override void Use(Entity user)
        {
            user.Mana -= RequiredMana;
            user.Abilities.ApplyMagic();
            if (Level < 10)
                _level += 1 / 3;
        }

        public override bool CanUsed(Entity user)
        {
            return user.Mana >= RequiredMana;
        }
    }
}
