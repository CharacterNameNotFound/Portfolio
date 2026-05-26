using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Game.GameMode.Session.Controller
{
    public class SessionInitializationConfigs : ScriptableObject
    {
        [field: SerializeField] public int CubesPooledCount { get; private set; }
        [field: SerializeField] public AssetReferenceGameObject GameFieldAssetReference { get; set; }
        [field: SerializeField] public int Lives { get; private set; }
    }
}