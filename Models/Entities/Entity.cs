using System;
using Models.Damages;

namespace Models.Entities
{
    public abstract class Entity
    {
        public static readonly byte MaxHealth = 100;
        public static readonly byte MaxMana = 100;

        public string Name { get; }
        public byte Health { get; private set; }
        public byte Mana { get; set; }
        public AbilityBoard Abilities { get; }
        public Resistances Resistances { get; }
        public Inventory Inventory { get; }

        public bool IsAlive => Health > 0;
        public bool CanAttack => Inventory.CanUseActiveWeapon;
        public Damage Damage => Inventory.ActiveWeaponDamage;

        public delegate void DamagedHandler(Entity self, byte damage);
        public event DamagedHandler Damaged;

        public delegate void DiedHandler(Entity self);
        public event DiedHandler Died;

        public Entity(string name, AbilityBoard abilites, Resistances resistances, Inventory inventory)
        {
            Name = name;
            Abilities = abilites;
            Resistances = resistances;
            Inventory = inventory;
            Inventory.Owner = this;
            Health = MaxHealth;
            Mana = MaxMana;
        }

        public void ApplyDamage(Damages.Damage damage)
        {
            if (!IsAlive)
            {
                throw new InvalidOperationException($"{this} is already dead");
            }
            var damagePower = Resistances.Apply(damage);
            if (damagePower >= Health)
            {
                Health = 0;
                Died?.Invoke(this);
            }
            else
            {
                Health -= damagePower;
                damage.ApplyResistance(Resistances);
                Damaged?.Invoke(this, damagePower);
            }
        }

        public void Heal(byte recovery)
        {
            if (!IsAlive)
            {
                throw new InvalidOperationException($"{this} is dead and cannot be healed");
            }
            else if (Health + recovery > MaxHealth)
            {
                Health = MaxHealth;
            }
            else
            {
                Health += recovery;
            }
        }

        public void UseWeapon()
        {
            if (!CanAttack)
                throw new InvalidOperationException();
            Inventory.ActiveWeapon.Use(this);
        }
    }
}
