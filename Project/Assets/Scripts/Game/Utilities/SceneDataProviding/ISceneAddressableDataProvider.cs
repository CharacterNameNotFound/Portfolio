using UnityEngine.AddressableAssets;

namespace Game.Utilities.SceneDataProviding
{
    public interface ISceneAddressableDataProvider
    {
        public AssetReference MainScene { get; }
        public AssetReference SessionScene { get; }
    }
}