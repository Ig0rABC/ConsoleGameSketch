using Models.Entities;

namespace Models.Weapons
{
    public abstract class MeleeWeapon : Weapon

    {
        public float Condition { get; private set; }

        public MeleeWeapon(string name, byte damage) : base(name, damage)
        {
            Condition = 1;
        }

        public override byte GetDamage(AbilityBoard userAbilities)
        {
            return (byte)(base.GetDamage(userAbilities) * (Condition > 0.33f ? Condition : 0.33f) + userAbilities.Strength);
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
