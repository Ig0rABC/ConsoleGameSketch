
namespace Models.Weapons
{
    public abstract class MeleeWeapon : Weapon

    {
        public float Condition { get; private set; }
        public override byte Damage => (byte)(base.Damage * Condition);

        public MeleeWeapon(string name, byte damage) : base(name, damage)
        {
            Condition = 1;
        }

        public override void Use()
        {
            if (Condition > 0)
                Condition -= 0.125f;
        }
    }
}
