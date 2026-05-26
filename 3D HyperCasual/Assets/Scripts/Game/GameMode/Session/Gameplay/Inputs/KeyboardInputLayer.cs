using GameWideSystems.InputManager;
using GameWideSystems.InputManager.GestureReaders.Keyboard;
using UnityEngine;

namespace Game.GameMode.Session.Gameplay.Inputs
{
    public class KeyboardInputLayer : IInputHandlerLayer
    {
        public int Index => 10;
        public InputType InputType => InputType.Keyboard;

        private InputBuffer _inputBuffer;

        public KeyboardInputLayer(InputBuffer inputBuffer)
        {
            _inputBuffer = inputBuffer;
        }

        public bool TryHandle(IGesture gesture)
        {
            if (gesture is not KeyboardAny)
            {
                return false;
            }
            
            _inputBuffer.ActivatedLines[0] |= UnityEngine.InputSystem.Keyboard.current.qKey.wasPressedThisFrame;
            _inputBuffer.ActivatedLines[1] |= UnityEngine.InputSystem.Keyboard.current.wKey.wasPressedThisFrame;
            _inputBuffer.ActivatedLines[2] |= UnityEngine.InputSystem.Keyboard.current.eKey.wasPressedThisFrame;
            _inputBuffer.ActivatedLines[3] |= UnityEngine.InputSystem.Keyboard.current.rKey.wasPressedThisFrame;
            
            return true;
        }
    }
}