using UnityEngine;

namespace GameWideSystems.InputManager.GestureReaders.Pointer
{
    public class LongPressStart : IPointerGesture
    {
        public InputType InputType => InputType.Pointer;
        public Vector2 SourcePosition { get; set; }

        public LongPressStart(Vector2 sourcePosition)
        {
            SourcePosition = sourcePosition;
        }
    }
}