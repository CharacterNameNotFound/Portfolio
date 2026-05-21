using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Game.GameMode.Session.Inputs
{
    public class ScreenJoystickLayerConfigs : ScriptableObject
    {
        [field: SerializeField] public AssetReferenceGameObject JoystickView { get; private set; }
        
        
        
    }
}