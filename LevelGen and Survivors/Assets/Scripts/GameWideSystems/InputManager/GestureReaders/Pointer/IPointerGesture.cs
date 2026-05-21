using UnityEngine;

namespace GameWideSystems.InputManager.GestureReaders.Pointer
{
    public interface IPointerGesture : IGesture
    { 
        public Vector2 SourcePosition { get; }
    }
}