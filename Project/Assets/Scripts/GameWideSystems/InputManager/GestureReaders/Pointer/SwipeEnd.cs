using UnityEngine;

namespace GameWideSystems.InputManager.GestureReaders.Pointer
{
    public class SwipeEnd : IPointerGesture
    {
        public InputType InputType => InputType.Pointer;
        public Vector2 SourcePosition { get; private set; }
        public Vector2 CurrentPosition { get; private set; }

        public SwipeEnd(Vector2 sourcePosition, Vector2 currentPosition)
        {
            SourcePosition = sourcePosition;
            CurrentPosition = currentPosition;
        }
        
    }
}