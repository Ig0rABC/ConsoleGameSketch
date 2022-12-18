using Models.Entities;

namespace Models.Effects
{
    public abstract class Effect : IUpdatable
    {
        public delegate void ExpiredHandler(Effect self);
        public event ExpiredHandler Expired;

        public byte Priority { get; }

        private Entity _target;

        public void Update()
        {
            Tick(_target);
        }

        public void Apply(Entity target)
        {
            _target = target;
            OnApplied(target);
        }

        public void Dispose()
        {
            OnDispose(_target);
            Expired?.Invoke(this);
        }

        protected abstract void OnApplied(Entity target);

        protected abstract void Tick(Entity target);

        protected abstract void OnDispose(Entity target);
    }
}
