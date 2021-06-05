using System;

namespace Models.Weapons
{
    public abstract class MagicWeapon : Weapon
    {
        public override byte Damage => (byte)(base.Damage * Math.Floor(Level));
        public float Level { get; private set; }
        public byte RequiredMana { get; }

        public MagicWeapon(string name, byte damage, byte requiredMana) : base(name, damage)
        {
            Level = 1;
            RequiredMana = requiredMana;
        }

        public override void Use()
        {
            if (Level < 10)
                Level += 0.34f;
        }
    }
}
