using Models.Damages;

namespace Models.Weapons
{
    public abstract class Ammo : InventoryItem
    {
        public byte Power { get; }

        public Ammo(string name, byte power) : base(name)
        {
            Power = power;
        }

        public abstract Damage GetDamage();
    }
}
