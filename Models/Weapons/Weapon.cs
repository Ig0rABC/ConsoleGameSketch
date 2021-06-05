
namespace Models.Weapons
{
    public abstract class Weapon
    {
        public string Name { get; }
        public virtual byte Damage => _damage;

        private readonly byte _damage;

        public Weapon(string name, byte damage)
        {
            Name = name;
            _damage = damage;
        }

        public abstract void Use();
    }
}
