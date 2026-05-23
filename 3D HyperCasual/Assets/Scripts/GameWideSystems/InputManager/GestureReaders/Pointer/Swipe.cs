using UnityEngine;

namespace GameWideSystems.InputManager.GestureReaders.Pointer
{
    public class Swipe : IPointerGesture
    {
        public InputType InputType => InputType.Pointer;
        public Vector2 SourcePosition { get; set; }
        public Vector2 Direction { get; set; }
        public Vector2Int DirectionInt { get; set; }

        public Swipe(Vector2 sourcePosition, Vector2 direction, Vector2Int directionInt)
        {
            SourcePosition = sourcePosition;
            Direction = direction;
            DirectionInt = directionInt;
        }
        
    }
}