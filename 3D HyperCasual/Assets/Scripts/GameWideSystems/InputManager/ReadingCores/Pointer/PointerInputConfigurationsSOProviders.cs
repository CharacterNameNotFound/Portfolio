using UnityEngine;

namespace GameWideSystems.InputManager.ReadingCores.Pointer
{
    public class PointerInputConfigurationsSOProviders : ScriptableObject, IPointerInputConfigurationsProvider
    {
        [field: SerializeField] public float TapToLongPressThreshold { get; private set; }
        [field: SerializeField] public float SwipeLengthThreshold { get; private set; }
    }
}