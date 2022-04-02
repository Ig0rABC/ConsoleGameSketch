using System;
using Models.Damages;
using Models.Resistances;

namespace Models.Entities
{
    public abstract class Entity
    {
        public string Name { get; }
        public readonly StateBar Health;
        public readonly StateBar Mana;
        public readonly AbilityBoard Abilities;
        public readonly TotalResistanceBoard TotalResistances;
        public readonly Inventory Inventory;

        public bool CanAttack => Inventory.ActiveWeapon.CanUsed(this);

        public delegate void DamagedHandler(Entity self, float damage);
        public event DamagedHandler Damaged;

        public delegate void RecoveredHandler(Entity self, float damage);
        public event DamagedHandler Recovered;

        public delegate void DiedHandler(Entity self, float damage);
        public event DiedHandler Died;

        public Entity(string name, AbilityBoard abilites, EntityResistanceBoard resistances, Inventory inventory)
        {
            Name = name;
            Abilities = abilites;
            Inventory = inventory;
            TotalResistances = new TotalResistanceBoard(resistances, inventory);
            Mana = new StateBar();
            Health = new StateBar();
            Health.Taken += OnDamaged;
            Health.Restored += OnRecovered;
            Health.Emptied += OnDied;
        }

        public Damage InstantiateDamage()
        {
            return Inventory.ActiveWeapon.InstantiateDamage(this);
        }

        public void ApplyDamage(Damage damage)
        {
            if (Health.IsEmpty())
            {
                throw new InvalidOperationException($"{this} is already dead");
            }
            var damagePower = TotalResistances.Apply(damage);
            Health.Take(damagePower);
            damage.ApplyResistance(TotalResistances);
        }

        public void Heal(float recovery)
        {
            if (Health.IsEmpty())
            {
                throw new InvalidOperationException($"{this} is dead and cannot be healed");
            }
            Health.Restore(recovery);
        }

        public void UseWeapon()
        {
            if (!CanAttack)
                throw new InvalidOperationException();
            Inventory.ActiveWeapon.Use(this);
        }

        private void OnDamaged(float damage)
        {
            Damaged?.Invoke(this, damage);
        }

        private void OnRecovered(float recovery)
        {
            Recovered?.Invoke(this, recovery);
        }

        private void OnDied(float damage)
        {
            Died?.Invoke(this, damage);
            Unsubscribe();
        }

        private void Unsubscribe()
        {
            Health.Taken -= OnDamaged;
            Health.Restored -= OnRecovered;
            Health.Emptied -= OnDied;
        }
    }
}
