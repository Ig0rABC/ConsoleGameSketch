using System;
using Models.Weapons;

namespace Models.Entities
{
    public abstract class Entity
    {
        public string Name { get; }
        public byte Health { get; private set; }
        public AbilityBoard Abilities { get; }
        public Weapon Weapon { get; }
        public byte Ammo { get; set; }
        public byte Mana { get; set; }

        public bool IsAlive => Health > 0;
        public bool CanAttack => Weapon.CanUsed(this);
        public byte Damage => Weapon.GetDamage(Abilities);

        public delegate void DamageTakenHandler(Entity self, byte damage);
        public event DamageTakenHandler Damaged;

        public delegate void DiedHandler(Entity self);
        public event DiedHandler Died;

        public Entity(string name, AbilityBoard abilites, Weapon weapon)
        {
            Name = name;
            Abilities = abilites;
            Weapon = weapon;
            Health = 100;
            Mana = 100;
            Ammo = 3;
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

        public void UseWeapon()
        {
            Weapon.Use(this);
        }
    }
}
