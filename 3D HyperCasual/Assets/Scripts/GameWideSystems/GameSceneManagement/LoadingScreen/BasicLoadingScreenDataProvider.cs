using UnityEngine;
using UnityEngine.AddressableAssets;
using Utils.UtilityTypes.AssetReferencing;

namespace GameWideSystems.GameSceneManager.LoadingScreen
{
    public class BasicLoadingScreenDataProvider : ScriptableObject, IBasicLoadingScreenDataProvider
    {
        [field: SerializeField] public AssetReferenceGameObject BasicLoadingScreen { get; private set; }
        public AssetReference BasicLoadingScreenAddressableKey => BasicLoadingScreen;
    }
}