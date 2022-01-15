using Models.Entities;
using Models.Damages;

namespace Models.Weapons
{
    public abstract class RangedWeapon<T> : Weapon where T : Ammo
    {
        public RangedWeapon(string name, byte power) : base(name, power)
        {

        }

        public override Damage GetDamage(Entity user)
        {
            var ammo = user.Inventory.GetOne<T>();
            var weaponPower = (byte)(Power + user.Abilities.Accuracy);
            if (ammo == null)
                return new SteelDamage(weaponPower);
            var damage = ammo.GetDamage();
            damage.Add(weaponPower);
            return damage;
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
