using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Game.Utilities.SceneDataProviding
{
    public class SceneAddressableDataProvider : ScriptableObject, ISceneAddressableDataProvider
    {
        [field: SerializeField] public AssetReference Main { get; private set; }
        
        public AssetReference MainScene => Main;
    }
}