using UnityEngine.AddressableAssets;

namespace GameWideSystems.TooltipsManagement
{
    public interface ITooltipConfigurationProvider
    {
        // if we're going to have only game objects here we have no need to bother ourselves with a key
        public AssetReference TextTooltipPrefabReference { get; }
    }
}