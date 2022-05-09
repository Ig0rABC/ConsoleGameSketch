using Models.Entities;
using Models.Damages;
using System.Collections.Generic;
using Models.Effects;

namespace Models.Weapons
{
    public sealed class FlameStaff : MagicWeapon
    {
        public FlameStaff() : base("Flame Staff", 0.45f, 0.125f)
        {

        }

        public override Damage InstantiateDamage(Entity user)
        {
            var damagePower = Damage.CalculatePower(Power, user.Abilities.Magic);
            return new FlameDamage(damagePower);
        }

        public override IEnumerable<Effect> InstantiateEffects()
        {
            return new Effect[] { new Burning(5, Power) };
        }
    }
}
