using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;

namespace GameWideSystems.InputManager
{
    public class InputControlFacade
    {
        private IInputHost _inputHost;
        private InputSystemUIInputModule _inputModule;
        private Logger.Logger _logger;

        private int _inputLockLevel = 0;

        private InputSystemUIInputModule LazyUIInputModule
        {
            get
            {
                if (_inputModule) return _inputModule;
                _inputModule = EventSystem.current.GetComponent<InputSystemUIInputModule>();

                return _inputModule;
            }
        }

        public InputControlFacade(IInputHost inputHost, Logger.Logger logger)
        {
            _inputHost = inputHost;
            _logger = logger;
        }

        public void SetInputsAvailable(bool isActive)
        {
            _inputLockLevel += isActive ? -1 : 1;

            if (_inputLockLevel < 0)
            {
                _logger.Warn("Number of UI unlock calls exceeded number of lock calls");
            }
            
            bool newState = _inputLockLevel < 1;
            LazyUIInputModule.enabled = newState;
            _inputHost.SetIgnoreInputs(!newState);
        }
    }
}