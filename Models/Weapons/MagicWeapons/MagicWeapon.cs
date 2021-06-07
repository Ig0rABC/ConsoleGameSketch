using System;
using Models.Entities;

namespace Models.Weapons
{
    public abstract class MagicWeapon : Weapon
    {
        public float Level { get; private set; }
        public byte RequiredMana { get; }

        public MagicWeapon(string name, byte damage, byte requiredMana) : base(name, damage)
        {
            Level = 1;
            RequiredMana = requiredMana;
        }

        public override byte GetDamage(AbilityBoard userAbilities)
        {
            return (byte)(base.GetDamage(userAbilities) * Math.Floor(Level) + userAbilities.Magic);
        }

        public override void Use(Entity user)
        {
            user.Mana -= RequiredMana;
            user.Abilities.ApplyMagic();
            if (Level < 10)
                Level += 0.34f;
        }

        public override bool CanUsed(Entity user)
        {
            return user.Mana >= RequiredMana;
        }
    }
}
