using UnityEngine.AddressableAssets;
using Utils.UtilityTypes.AssetReferencing;

namespace GameWideSystems.AudioManager
{
    public interface IAudioManagerConfigurationsProvider
    {
        public AssetReference SFXAudioPrefab { get; }
        public int SFXPoolSize { get; }
        public AssetReference MusicAudioPrefab { get; }
    }
}