using Models.Entities;

namespace Models.Weapons
{
    public abstract class RangedWeapon<T> : Weapon where T : Ammo
    {
        public RangedWeapon(string name, byte damage) : base(name, damage)
        {

        }

        public override byte GetDamage(AbilityBoard userAbilities)
        {
            var baseDamage = base.GetDamage(userAbilities);
            return (byte)(baseDamage + userAbilities.Accuracy);
        }

        public override void Use(Entity user) {
            user.Inventory.PutOut<T>();
            user.Abilities.ApplyAccuracy();
        }

        public override bool CanUsed(Entity user)
        {
            return user.Inventory.Has<T>();
        }
    }
}
