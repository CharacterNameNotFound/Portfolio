using System;
using GameWideSystems.InputManager.GestureReaders.Keyboard;

namespace GameWideSystems.InputManager.ReadingCores.Keyboard
{
    public class KeyboardReader : IInputReadingCore
    {
        public event Action<IGesture> OnGestureRead;

        private bool _isActive;
        
        public KeyboardReader()
        {
            UnityEngine.InputSystem.Keyboard.current.onTextInput += CurrentOnTextInput;
        }

        private void CurrentOnTextInput(char obj)
        {
            if (!_isActive)
            {
                return;
            }
            
            OnGestureRead?.Invoke(new KeyboardText(obj));
        }

        public void Dispose()
        {
        }

        public void Tick()
        {
        }

        public void Activate()
        {
            _isActive = true;
        }

        public void Deactivate()
        {
            _isActive = false;
        }
        
    }
}