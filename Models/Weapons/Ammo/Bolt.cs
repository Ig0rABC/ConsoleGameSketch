using Models.Damages;

namespace Models.Weapons
{
    public sealed class Bolt : Ammo
    {
        public Bolt() : base("Crossbow Bolt", 0.28f)
        {

        }
        public override Damage InstantiateDamage(float weaponPower)
        {
            return new SteelDamage(Power + weaponPower);
        }
    }
}
