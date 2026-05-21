using UnityEngine;

namespace GameWideSystems.InputManager.GestureReaders.Pointer
{
    public class Press : IPointerGesture
    {
        public InputType InputType => InputType.Pointer;
        public Vector2 SourcePosition { get; set; }

        public Press(Vector2 sourcePosition)
        {
            SourcePosition = sourcePosition;
        }
    }
}