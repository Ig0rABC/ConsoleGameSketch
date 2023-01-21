using System;
using Models.Damages;
using Models.Resistances;
using Models.Effects;

namespace Models.Entities
{
    public abstract class Entity : IUpdatable
    {
        public string Name { get; }
        public float Health => _health.Value;
        public float Mana => _mana.Value;

        public bool Alive => !_health.IsEmpty();
        
        public readonly AbilityBoard Abilities;
        public readonly EntityResistanceBoard Resistances;
        public readonly Inventory Inventory;
        public readonly Effector Effector;

        public bool CanAttack => Inventory.ActiveWeapon.CanUsed(this);

        public delegate void DamagedHandler(Entity self, float damage);
        public event DamagedHandler Damaged;

        public delegate void HealedHandler(Entity self, float recovery);
        public event HealedHandler Healed;

        public delegate void DiedHandler(Entity self, float damage);
        public event DiedHandler Died;

        private readonly StateBar _health;
        private readonly StateBar _mana;

        public Entity(string name, AbilityBoard abilites, EntityResistanceBoard resistances, Inventory inventory)
        {
            Name = name;
            Abilities = abilites;
            Resistances = resistances;
            Inventory = inventory;
            Effector = new();
            _mana = new();
            _health = new();
            Subscribe();
        }

        public void Update()
        {
            Effector.Update(this);
        }

        public void Apply(Damage damage)
        {
            if (!Alive)
                throw new InvalidOperationException($"{this} is already dead");
            Inventory.Outfit?.ApplyDamage(damage);
            damage.Apply(Resistances, _health);
        }

        public void Apply(Effect effect)
        {
            Effector.Add(this, effect);
        }

        public void Cast(float requiredMana)
        {
            _mana.Take(requiredMana);
            Abilities.ApplyMagic();
        }

        public void Heal(float recovery)
        {
            if (!Alive)
                throw new InvalidOperationException($"{this} is dead and cannot be healed");
            _health.Restore(recovery);
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

        private void OnRestored(float recovery)
        {
            Healed?.Invoke(this, recovery);
        }

        private void OnDied(float damage)
        {
            Died?.Invoke(this, damage);
            Unsubscribe();
        }

        private void Subscribe()
        {
            _health.Taken += OnDamaged;
            _health.Restored += OnRestored;
            _health.Emptied += OnDied;
        }

        private void Unsubscribe()
        {
            _health.Taken -= OnDamaged;
            _health.Restored -= OnRestored;
            _health.Emptied -= OnDied;
        }
    }
}
