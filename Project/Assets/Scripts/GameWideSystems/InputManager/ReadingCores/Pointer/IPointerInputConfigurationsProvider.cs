namespace GameWideSystems.InputManager.ReadingCores.Pointer
{
    public interface IPointerInputConfigurationsProvider
    {
        public float TapToLongPressThreshold { get; }
        public float SwipeLengthThreshold { get; }
    }
}