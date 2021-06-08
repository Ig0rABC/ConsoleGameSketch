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
            var baseDamage = base.GetDamage(userAbilities);
            var damageDecreaseFactor = Condition > 0.375f ? Condition : 0.375f;
            return (byte)(baseDamage * damageDecreaseFactor + userAbilities.Strength);
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
