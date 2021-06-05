
namespace Models.Weapons
{
    public abstract class RangedWeapon : Weapon
    {
        public RangedWeapon(string name, byte damage) : base(name, damage)
        {

        }

        public override void Use() { }
    }
}
