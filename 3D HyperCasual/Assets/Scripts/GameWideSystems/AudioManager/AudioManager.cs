using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.ResourceManagement.ResourceProviders;
using Utils.UtilityTypes.AssetReferencing;

namespace GameWideSystems.AudioManager
{
    public class AudioManager
    {
        private const string MasterVolumeKey = "Audio/MasterVolume"; 
        private const string IsMuteKey = "Audio/IsMute"; 
        
        private SFXPoolPlayer _sfxPlayer;
        private AudioSource _musicPlayer;
        private AudioManagerConfigurations _audioManagerConfigurations;

        private bool _isMute;
        private float _masterVolume;

        public AudioManager(AudioManagerConfigurations audioManagerConfigurations)
        {
            _audioManagerConfigurations = audioManagerConfigurations;
        }

        public float MasterVolume => _masterVolume;
        public bool IsMute => _isMute;

        public async UniTask Initialize(Transform managerRoot, CancellationToken cancellationToken)
        {
            _masterVolume = PlayerPrefs.GetFloat(MasterVolumeKey, 1f);
            _isMute = PlayerPrefs.GetInt(IsMuteKey, 0) == 1;
            
            Transform audioManagerTransform = new GameObject("AudioManager").transform;
            audioManagerTransform.SetParent(managerRoot);

            await InitializeSfxPool(audioManagerTransform, cancellationToken);
            await InitializeMusicPlayer(audioManagerTransform, cancellationToken);
        }
        
        public UniTask PlaySFX(AudioClip audioClip, CancellationToken cancellationToken)
        {
            return _sfxPlayer.PlaySFX(audioClip, GetMasterVolume(), cancellationToken);
        }

        public async UniTask PlayMusic(AudioClip audioClip, CancellationToken cancellationToken)
        {
            await DOVirtual.Float(1, 0, 1, (f) => _musicPlayer.volume = GetMasterVolume() * f)
                .Play()
                .ToUniTask(cancellationToken: cancellationToken);
            
            _musicPlayer.Stop();
            _musicPlayer.clip = audioClip;
            _musicPlayer.Play();
            
            await DOVirtual.Float(0, 1, 1, (f) => _musicPlayer.volume = GetMasterVolume() * f)
                .Play()
                .ToUniTask(cancellationToken: cancellationToken);
        }

        public void SetMasterVolume(float volume)
        {
            _masterVolume = Mathf.Clamp01(volume);
            
            PlayerPrefs.SetFloat(MasterVolumeKey, volume);

            _musicPlayer.volume = GetMasterVolume();
        }
        
        public void ToggleMute(bool value)
        {
            PlayerPrefs.SetInt(IsMuteKey, value ? 1 : 0);
            
            _isMute = value;
            _musicPlayer.volume = GetMasterVolume();
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

        private float GetMasterVolume()
        {
            return _isMute ? 0 : _masterVolume;
        }
        
    }
}