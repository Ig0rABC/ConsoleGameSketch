using Models.Entities;

namespace Models.Effects
{
    public abstract class TempEffect : Effect
    {
        public bool IsOver => Duration == 0;
        public byte Duration { get; private set; }

        private byte _delay;

        public TempEffect(byte duration, byte delay)
        {
            Duration = duration;
            _delay = delay;
        }

        protected override void Tick(Entity target)
        {
            if (_delay > 0)
            {
                _delay--;
            }
            else
            {
                Duration--;
                OnTick(target);
                if (IsOver)
                {
                    Dispose();
                }
            }
        }

        protected override void OnApplied(Entity target)
        {
            if (_delay == 0)
                Tick(target);
        }

        protected abstract void OnTick(Entity target);

        protected override void OnDispose(Entity target)
        {

        }
    }
}
