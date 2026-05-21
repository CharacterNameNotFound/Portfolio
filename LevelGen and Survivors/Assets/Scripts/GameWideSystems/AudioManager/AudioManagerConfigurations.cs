using UnityEngine;
using UnityEngine.AddressableAssets;
using Utils.UtilityTypes.AssetReferencing;

namespace GameWideSystems.AudioManager
{
    public class AudioManagerConfigurations : ScriptableObject, IAudioManagerConfigurationsProvider
    {
        [SerializeField] private AssetReference _sfxAudioPrefabReference;
        [SerializeField] private AssetReference _musicAudioPrefabReference;
        [field: SerializeField] public int SFXPoolSize { get; private set; }

        public AssetReference SFXAudioPrefab => _sfxAudioPrefabReference;
        public AssetReference MusicAudioPrefab  => _sfxAudioPrefabReference;
        
    }
}