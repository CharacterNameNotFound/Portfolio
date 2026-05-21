using System;
using UnityEngine.InputSystem;

namespace GameWideSystems.InputManager.ReadingCores.Pointer
{
    public class PointerCore : IInputReadingCore
    {
        public event Action<IGesture> OnGestureRead;
        
        private readonly MainInputs _mainInputs;
        private readonly IPointerInputConfigurationsProvider _pointerInputConfigurations;
        private readonly PointerReaderEventHost _pointerReaderEventHost;
        
        private PointerBuffer _pointerBuffer;

        public PointerCore(IPointerInputConfigurationsProvider pointerInputConfigurations, MainInputs mainInputs)
        {
            _mainInputs = mainInputs;
            _pointerReaderEventHost = new PointerReaderEventHost();
            
            _pointerInputConfigurations = pointerInputConfigurations;

            _mainInputs.Pointer.Press.performed += Press;
            _mainInputs.Pointer.Press.canceled += Release;

            _pointerReaderEventHost.OnGesture += BroadcastGesture;
            _pointerReaderEventHost.OnFinalized += FinalizeGestureReading;
        }

        public void Dispose()
        {
            _mainInputs?.Dispose();
        }

        public void Activate()
        {
            _mainInputs.Pointer.Enable();
        }

        public void Deactivate()
        {
            _mainInputs.Pointer.Disable();
        }
        
        public void Tick()
        {
            _pointerBuffer?.OnUpdate();
        }

        private void Press(InputAction.CallbackContext inputEvent)
        {
            _pointerBuffer = new PointerBuffer(_pointerReaderEventHost, _pointerInputConfigurations, _mainInputs);
            
            _pointerBuffer.OnPress(inputEvent);
        }

        private void Release(InputAction.CallbackContext inputEvent)
        {
            _pointerBuffer?.OnRelease(inputEvent);
        }
        
        public void FinalizeGestureReading()
        {
            _pointerBuffer = null;
        }
        
        private void BroadcastGesture(IGesture gesture)
        {
            OnGestureRead?.Invoke(gesture);
        }


    }
}