namespace GameWideSystems.InputManager
{
    public interface IInputHost
    {
        public void AddInputLayer(IInputHandlerLayer inputHandlerLayer);
        public bool RemoveInputLayer(IInputHandlerLayer inputHandlerLayer);
        public void HostInputEvent(IGesture gesture);
        public void SetIgnoreInputs(bool isLocked);
    }
}