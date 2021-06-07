using Models.Entities;

namespace Models.Weapons
{
    public abstract class RangedWeapon : Weapon
    {
        public RangedWeapon(string name, byte damage) : base(name, damage)
        {

        }

        public override byte GetDamage(AbilityBoard userAbilities)
        {
            return (byte)(base.GetDamage(userAbilities) + userAbilities.Accuracy);
        }

        public override void Use(Entity user) {
            user.Ammo--;
            user.Abilities.ApplyAccuracy();
        }

        public override bool CanUsed(Entity user)
        {
            return user.Ammo > 0;
        }
    }
}
