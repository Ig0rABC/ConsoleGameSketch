using Models.Damages;

namespace Models.Weapons
{
    public sealed class Arrow : Ammo
    {
        public Arrow() : base("Arrow", 5)
        {

        }

        public override Damage GetDamage()
        {
            return new SteelDamage(Power);
        }
    }
}
