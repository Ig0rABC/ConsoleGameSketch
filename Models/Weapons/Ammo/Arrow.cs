using Models.Damages;

namespace Models.Weapons
{
    public sealed class Arrow : Ammo
    {
        public Arrow() : base("Arrow", 0.19f)
        {

        }

        public override Damage InstantiateDamage(float weaponPower)
        {
            return new SteelDamage(Power + weaponPower);
        }
    }
}
