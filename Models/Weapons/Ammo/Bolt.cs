using Models.Damages;

namespace Models.Weapons
{
    public sealed class Bolt : Ammo
    {
        public Bolt() : base("Crossbow Bolt", 9)
        {

        }
        public override Damage GetDamage()
        {
            return new SteelDamage(Power);
        }
    }
}
