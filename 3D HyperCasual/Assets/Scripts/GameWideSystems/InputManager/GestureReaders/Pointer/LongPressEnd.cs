using UnityEngine;

namespace GameWideSystems.InputManager.GestureReaders.Pointer
{
    public class LongPressEnd : IPointerGesture
    {
        public InputType InputType => InputType.Pointer;
        public Vector2 SourcePosition { get; set; }
        public Vector2 CurrentPosition { get; set; }

        public LongPressEnd(Vector2 sourcePosition, Vector2 currentPosition)
        {
            SourcePosition = sourcePosition;
            CurrentPosition = currentPosition;
        }
        
    }
}