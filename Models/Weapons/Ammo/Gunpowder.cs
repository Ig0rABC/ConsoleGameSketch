using Models.Damages;

namespace Models.Weapons
{
    public sealed class Gunpowder : Ammo
    {
        public Gunpowder() : base("Gunpowder", 20)
        {

        }

        public override Damage GetDamage()
        {
            return new FireArmDamage(Power);
        }
    }
}
