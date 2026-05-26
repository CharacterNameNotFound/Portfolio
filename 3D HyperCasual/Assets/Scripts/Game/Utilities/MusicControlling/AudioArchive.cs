using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using GameWideSystems.AudioManager;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Utils.UtilityTypes.AssetReferencing;
using Random = UnityEngine.Random;

namespace Game.Utilities.MusicControlling
{
    public class AudioArchive
    {
        private AudioManager _audioManager;
        private MusicArchiveAudioProvider _soundProvider;

        private int _buttonCounter = 0;
        private List<AudioClip> _buttonClips;

        private AudioClip _currentClip;
        private CancellationTokenSource _transitionCancellation;

        private AudioClip _bitSFX;
        
        public AudioArchive(AudioManager audioManager, MusicArchiveAudioProvider soundProvider)
        {
            _audioManager = audioManager;
            _soundProvider = soundProvider;
        }

        public async UniTask Initialize(CancellationToken cancellationToken)
        {
            _buttonClips = new();
            foreach (AssetReference reference in _soundProvider.ButtonList)
            {
                _buttonClips.Add(await reference.Load<AudioClip>(cancellationToken));
            }

            _bitSFX = await _soundProvider.BitSFX.Load<AudioClip>(cancellationToken);
        }

        public async UniTask PlayMusic(MusicGroup musicGroup, CancellationToken cancellationToken)
        {
            if (_transitionCancellation != null)
            {
                _transitionCancellation.Cancel();
                _transitionCancellation.Dispose();
            }

            _transitionCancellation = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);

            IReadOnlyList<AssetReference> musicRefs = _soundProvider.GetAssetList(musicGroup);

            int rand = Random.Range(0, musicRefs.Count);

            AssetReference assetReference = musicRefs[rand];

            AudioClip music;
            
            try
            {
                music = await assetReference.Load<AudioClip>(_transitionCancellation.Token);
            }
            catch (Exception)
            {
                return;
            }
            
            await _audioManager.PlayMusic(music, _transitionCancellation.Token);
            
            if (_currentClip)
            {
                Addressables.Release(_currentClip);
            }
            
            _currentClip = music;
        }

        public void PlayButton()
        {
            _audioManager.PlaySFX(_buttonClips[_buttonCounter % 2], Application.exitCancellationToken).Forget();
            _buttonCounter++;
        }

        public void PlayBit()
        {
            _audioManager.PlaySFX(_bitSFX, Application.exitCancellationToken).Forget();
        }
        
    }
}