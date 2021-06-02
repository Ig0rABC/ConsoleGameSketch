using System;

namespace Models.Entities
{
    public abstract class Entity
    {
        public string Name { get; }
        public byte Health { get; private set; }
        public byte Strength { get; }

        public bool IsAlive => Health > 0;

        public delegate void DamageTakenHandler(Entity self, byte damage);
        public event DamageTakenHandler Damaged;

        public delegate void DiedHandler(Entity self);
        public event DiedHandler Died;

        public Entity(string name, byte strength)
        {
            Name = name;
            Strength = strength;
            Health = 100;
        }


        public void ApplyDamage(byte damage)
        {
            if (Health == 0)
            {
                throw new Exception($"{this} is already dead");
            }
            else if (damage >= Health)
            {
                Health = 0;
                Died?.Invoke(this);
            }
            else
            {
                Health -= damage;
                Damaged?.Invoke(this, damage);
            }
        }
    }
}
