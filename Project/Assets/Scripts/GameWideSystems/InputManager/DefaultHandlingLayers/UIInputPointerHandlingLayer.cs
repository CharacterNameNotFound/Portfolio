using GameWideSystems.InputManager.GestureReaders.Pointer;
using Utils.Pointers;

namespace GameWideSystems.InputManager.DefaultHandlingLayers
{
    public class UIInputPointerHandlingLayer : IInputHandlerLayer
    {
        public int Index => -1;
        public InputType InputType => InputType.Pointer;
        
        public bool TryHandle(IGesture gesture)
        {
            if (gesture is not Press && gesture is not Tap && gesture is not Pressed)
            {
                return false;
            }
            

            IPointerGesture pointerGesture = (IPointerGesture) gesture;
            
            return IsPointerOverUIChecker.IsPointerOverUI(pointerGesture.SourcePosition);
        }
    }
}