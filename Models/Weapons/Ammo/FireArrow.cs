using System.Collections.Generic;
using Models.Damages;
using Models.Effects;

namespace Models.Weapons
{
    public sealed class FireArrow : Ammo
    {
        public FireArrow() : base("Fire arrow", 0.19f)
        {

        }

        public override Damage InstantiateDamage(float weaponPower)
        {
            return new SteelDamage(Power + weaponPower);
        }

        public override IEnumerable<Effect> InstantiateEffects()
        {
            return new [] { new Burning(5, 0.1f) };
        }
    }
}
