using UnityEngine.AddressableAssets;
using Utils.UtilityTypes.AssetReferencing;

namespace Game.Utilities.SceneDataProviding
{
    public interface ISceneAddressableDataProvider
    {
        public AssetReference MainScene { get; }
    }
}