using System;

namespace Spotnose
{
    public class Evt
    {
        private event Action Event;

        public void Invoke()
        {
            Event?.Invoke();
        }
        
        public void AddListener(Action listener)
        {
            Event += listener;
        }
        
        public void RemoveListener(Action listener)
        {
            Event -= listener;
        }
    }

    public class Evt<T>
    {
        private event Action<T> Event;
        
        public void Invoke(T arg)
        {
            Event?.Invoke(arg);
        }
        
        public void AddListener(Action<T> listener)
        {
            Event += listener;
        }
        
        public void RemoveListener(Action<T> listener)
        {
            Event -= listener;
        }
    }
}
