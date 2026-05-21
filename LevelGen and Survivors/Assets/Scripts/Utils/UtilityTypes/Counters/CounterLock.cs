using System;

namespace Utils.UtilityTypes.Counters
{
    public class CounterLock
    {
        private int _counter;

        public CounterLock(bool isLocked)
        {
            _counter = isLocked ? 1 : 0;
        }
        
        public void Toggle(bool unlock)
        {
            if (unlock)
            {
                Decrement();
                return;
            }
            
            Increment();
        }

        public void Increment(int increment = 1)
        {
            _counter += increment;
        }

        public void Decrement(int decrement = 1)
        {
            _counter -= decrement;
        }
        
        public bool IsUnlocked()
        {
            return !IsLocked();
        }
        
        public bool IsLocked()
        {
            return _counter > 0;
        }
        
    }
}