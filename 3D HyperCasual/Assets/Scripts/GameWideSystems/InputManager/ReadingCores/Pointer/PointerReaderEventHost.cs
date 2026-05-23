using System;

namespace GameWideSystems.InputManager.ReadingCores.Pointer
{
    public class PointerReaderEventHost
    {
        public event Action<IGesture> OnGesture;
        public event Action OnFinalized;

        public void BroadcastGesture(IGesture gesture)
        {
            OnGesture?.Invoke(gesture);
        }

        public void BroadcastFinalization()
        {
            OnFinalized?.Invoke();
        }
        
    }
}