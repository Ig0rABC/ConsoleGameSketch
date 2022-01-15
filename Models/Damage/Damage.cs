using Models.Entities;

namespace Models.Damages
{
    public abstract class Damage
    {
        public byte Power { get; private set; }

        public Damage(byte power)
        {
            Power = power;
        }

        public void Add(byte power)
        {
            Power += power;
        }

        public abstract byte SelectResistance(Resistances resistances);

        public abstract void ApplyResistance(Resistances resistances);
    }
}
