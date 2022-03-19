using Models.Entities;
using Models.Damages;

namespace Models.Weapons
{
    public sealed class FlameStaff : MagicWeapon
    {
        public FlameStaff() : base("Flame Staff", 0.33f, 0.125f)
        {

        }

        public override Damage InstantiateDamage(Entity user)
        {
            var damagePower = Damage.CalculatePower(Power, user.Abilities.Magic);
            return new FlameDamage(damagePower);
        }
    }
}
