
namespace Models.Weapons
{
    public abstract class Weapon
    {
        public string Name { get; }
        public byte Damage { get; }

        public Weapon(string name, byte damage)
        {
            Name = name;
            Damage = damage;
        }
    }
}
