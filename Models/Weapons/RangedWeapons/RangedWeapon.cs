using Models.Entities;
using Models.Damages;

namespace Models.Weapons
{
    public abstract class RangedWeapon<T> : Weapon where T : Ammo
    {
        public RangedWeapon(string name, float power) : base(name, power)
        {

        }

        public override Damage InstantiateDamage(Entity user)
        {
            var ammo = user.Inventory.GetOne<T>();
            float weaponPower = Damage.CalculatePower(Power, user.Abilities.Accuracy);
            if (ammo == null)
            {
                // TODO: use specific damage type
                return new SteelDamage(weaponPower);
            }
            return ammo.InstantiateDamage(weaponPower);
        }

        public override void Use(Entity user) {
            user.Inventory.PutOutOne<T>();
            user.Abilities.ApplyAccuracy();
        }

        public override bool CanUsed(Entity user)
        {
            return user.Inventory.Has<T>();
        }
    }
}
