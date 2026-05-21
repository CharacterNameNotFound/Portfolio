namespace GameWideSystems.InputManager
{
    public interface IInputHandlerLayer
    {
        public int Index { get; }
        public InputType InputType { get; } 
        public bool TryHandle(IGesture gesture);
    }
}