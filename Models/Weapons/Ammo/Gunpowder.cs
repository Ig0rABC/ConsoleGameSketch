using Models.Damages;

namespace Models.Weapons
{
    public sealed class Gunpowder : Ammo
    {
        public Gunpowder() : base("Gunpowder", 0.42f)
        {

        }

        public override Damage InstantiateDamage(float weaponPower)
        {
            return new FireArmDamage(Power + weaponPower);
        }
    }
}
