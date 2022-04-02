
namespace Models
{
    public class StateBar
    {
        public float Value => _value;

        public delegate void TakenHandler(float value);
        public event TakenHandler Taken;

        public delegate void RestoredHandler(float value);
        public event TakenHandler Restored;

        public delegate void EmptiedHandler(float value);
        public event EmptiedHandler Emptied;

        private float _value;

        public StateBar()
        {
            _value = 1;
        }

        public void Take(float value)
        {
            if (_value < value)
            {
                _value = 0;
                Emptied?.Invoke(value);
            }
            else
            {
                _value -= value;
                Taken?.Invoke(value);
            }
        }

        public void Restore(float value)
        {
            if (_value + value > 1)
            {
                _value = 1;
            }
            else
            {
                _value += value;
            }
            Restored?.Invoke(value);
        }

        public void Fill()
        {
            _value = 1;
        }

        public bool IsFull()
        {
            return _value == 1;
        }

        public void ToEmpty()
        {
            var value = _value;
            _value = 0;
            Emptied?.Invoke(value);
        }

        public bool IsEmpty()
        {
            return _value == 0;
        }
    }
}
