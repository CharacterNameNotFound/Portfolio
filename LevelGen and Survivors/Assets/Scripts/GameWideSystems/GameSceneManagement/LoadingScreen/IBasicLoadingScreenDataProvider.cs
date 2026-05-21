using UnityEngine.AddressableAssets;
using Utils.UtilityTypes.AssetReferencing;

namespace GameWideSystems.GameSceneManager.LoadingScreen
{
    public interface IBasicLoadingScreenDataProvider
    {
        public AssetReference BasicLoadingScreenAddressableKey { get; }

    }
}