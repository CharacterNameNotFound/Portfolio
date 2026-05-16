using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.ResourceManagement.ResourceProviders;
using Utils.UtilityTypes.AssetReferencing;

namespace GameWideSystems.AudioManager
{
    public class AudioManager
    {
        private SFXPoolPlayer _sfxPlayer;
        private AudioSource _musicPlayer;
        private AudioManagerConfigurations _audioManagerConfigurations;

        public AudioManager(AudioManagerConfigurations audioManagerConfigurations)
        {
            _audioManagerConfigurations = audioManagerConfigurations;
        }

        public async UniTask Initialize(Transform managerRoot, CancellationToken cancellationToken)
        { 
            Transform audioManagerTransform = new GameObject("AudioManager").transform;
            audioManagerTransform.SetParent(managerRoot);

            await InitializeSfxPool(audioManagerTransform, cancellationToken);
            await InitializeMusicPlayer(audioManagerTransform, cancellationToken);
        }
        
        public UniTask PlaySFX(AudioClip audioClip, CancellationToken cancellationToken)
        {
            return _sfxPlayer.PlaySFX(audioClip, cancellationToken);
        }

        public void PlayMusic(AudioClip audioClip)
        {
            _musicPlayer.clip = audioClip;
        }
        
        private UniTask InitializeSfxPool(Transform parent, CancellationToken cancellationToken)
        {
            GameObject sfxPool = new("SFX audio pool");
            
            sfxPool.transform.SetParent(parent);
            
            _sfxPlayer = new SFXPoolPlayer(_audioManagerConfigurations.SFXAudioPrefab, sfxPool.transform);

            return _sfxPlayer.Initialize(_audioManagerConfigurations.SFXPoolSize, cancellationToken);
        }

        private async UniTask InitializeMusicPlayer(Transform parent, CancellationToken cancellationToken)
        {
            GameObject musicPool = new("Music source holder");
            musicPool.transform.SetParent(parent);

            GameObject audioPlayer = await _audioManagerConfigurations.MusicAudioPrefab.Instantiate(
                new InstantiationParameters(musicPool.transform, false), cancellationToken);

            _musicPlayer = audioPlayer.GetComponent<AudioSource>();
        }
        
        
    }
}