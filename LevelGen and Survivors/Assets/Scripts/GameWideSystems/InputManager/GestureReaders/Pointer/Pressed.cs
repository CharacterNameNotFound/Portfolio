using UnityEngine;

namespace GameWideSystems.InputManager.GestureReaders.Pointer
{
    public class Pressed : IPointerGesture
    {
        public InputType InputType => InputType.Pointer;
        public Vector2 SourcePosition { get; private set; }
        public Vector2 CurrentPosition { get; private set; }

        public Pressed(Vector2 sourcePosition, Vector2 currentPosition)
        {
            SourcePosition = sourcePosition;
            CurrentPosition = currentPosition;
        }
    }
}