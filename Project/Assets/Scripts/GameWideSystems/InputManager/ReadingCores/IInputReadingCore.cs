using System;
using Zenject;

namespace GameWideSystems.InputManager.ReadingCores
{
    public interface IInputReadingCore : IDisposable, ITickable
    {
        public event Action<IGesture> OnGestureRead;

        public void Activate();
        public void Deactivate();
    }
}