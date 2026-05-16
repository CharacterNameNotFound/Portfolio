using System;
using GameWideSystems.InputManager.ReadingCores;

namespace GameWideSystems.InputManager
{
    public class InputReader : IDisposable
    {
        private readonly IInputReadingCore[] inputReadingCores;
        private readonly IInputHost inputHost;

        public InputReader(IInputReadingCore[] inputReadingCores, IInputHost inputHost)
        {
            this.inputReadingCores = inputReadingCores;
            this.inputHost = inputHost;
            
            ActivateCores();
            BindToReadingCores();
        }
        
        public void Dispose()
        {
            UnbindFromReadingCores();
            DeactivateCores();
        }

        private void ActivateCores()
        {
            foreach (IInputReadingCore readingCore in inputReadingCores)
            {
                readingCore.Activate();
            }
        }

        private void BindToReadingCores()
        {
            foreach (IInputReadingCore readingCore in inputReadingCores)
            {
                readingCore.OnGestureRead += HostGesture;
            }
        }

        private void HostGesture(IGesture gesture)
        {
            inputHost.HostInputEvent(gesture);        
        }
        
        private void UnbindFromReadingCores()
        {
            foreach (IInputReadingCore readingCore in inputReadingCores)
            {
                readingCore.OnGestureRead -= HostGesture;
            }
        }

        private void DeactivateCores()
        {
            foreach (IInputReadingCore readingCore in inputReadingCores)
            {
                readingCore.Deactivate();
            }
        }
    }
}