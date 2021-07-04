using System;

namespace Models.Entities
{
    public abstract class Entity
    {
        public string Name { get; }
        public byte Health { get; private set; }
        public byte Mana { get; set; }
        public AbilityBoard Abilities { get; }
        public Inventory Inventory { get; }

        public const byte HealthLimit = 100;
        public const byte ManaLimit = 100;

        public bool IsAlive => Health > 0;
        public bool CanAttack => Inventory.CanUseActiveWeapon;
        public byte Damage => Inventory.ActiveWeaponDamage;

        public delegate void DamageTakenHandler(Entity self, byte damage);
        public event DamageTakenHandler Damaged;

        public delegate void DiedHandler(Entity self);
        public event DiedHandler Died;

        public Entity(string name, AbilityBoard abilites, Inventory inventory)
        {
            Name = name;
            Abilities = abilites;
            Inventory = inventory;
            Inventory.Owner = this;
            Health = HealthLimit;
            Mana = ManaLimit;
        }

        public void ApplyDamage(byte damage)
        {
            if (!IsAlive)
            {
                throw new InvalidOperationException($"{this} is already dead");
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

        public void Heal(byte recovery)
        {
            if (!IsAlive)
            {
                throw new InvalidOperationException($"{this} is dead and cannot be healed");
            }
            else if (Health + recovery > HealthLimit)
            {
                Health = HealthLimit;
            }
            else
            {
                Health += recovery;
            }
        }

        public void UseWeapon()
        {
            Inventory.ActiveWeapon.Use(this);
        }
    }
}
