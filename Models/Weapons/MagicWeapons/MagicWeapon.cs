using System;
using Models.Entities;

namespace Models.Weapons
{
    public abstract class MagicWeapon : Weapon
    {
        public byte Level => (byte)Math.Floor(_level);
        public byte RequiredMana { get; }

        private float _level;

        public MagicWeapon(string name, byte damage, byte requiredMana) : base(name, damage)
        {
            _level = 1;
            RequiredMana = requiredMana;
        }

        public override byte GetDamage(AbilityBoard userAbilities)
        {
            var baseDamage = base.GetDamage(userAbilities);
            return (byte)(baseDamage * (1 + Level / 15) + userAbilities.Magic);
        }

        public override void Use(Entity user)
        {
            user.Mana -= RequiredMana;
            user.Abilities.ApplyMagic();
            if (Level < 10)
                _level += 0.34f;
        }

        public override bool CanUsed(Entity user)
        {
            return user.Mana >= RequiredMana;
        }
    }
}
