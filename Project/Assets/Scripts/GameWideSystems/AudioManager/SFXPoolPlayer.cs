using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Utils.UtilityTypes.AssetReferencing;
using Utils.UtilityTypes.ObjectPooling;

namespace GameWideSystems.AudioManager
{
    public class SFXPoolPlayer
    {
        private GameObjectPool<AudioPlayer> _audioSourcePool;
        
        public SFXPoolPlayer(AssetReference sfxAudioPrefab, Transform sfxPoolTransform)
        {
            _audioSourcePool = new GameObjectPool<AudioPlayer>(
                new List<AudioPlayer>(),
                new AddressablePoolEntityProvider<AudioPlayer>(sfxAudioPrefab),
                new AssignablePooledObjectHostProvider(sfxPoolTransform)
                );
        }

        public UniTask Initialize(int sfxPoolSize, CancellationToken cancellationToken)
        {
            return _audioSourcePool.ExtendBy(sfxPoolSize, cancellationToken);
        }

        public async UniTask PlaySFX(AudioClip clip, CancellationToken cancellationToken)
        {
            AudioPlayer audioPlayer = await _audioSourcePool.GetObject(cancellationToken);

            audioPlayer.AudioSource.gameObject.SetActive(true);
            audioPlayer.AudioSource.PlayOneShot(clip);
            
            await UniTask.WaitForSeconds(clip.length, cancellationToken: cancellationToken);
            
            audioPlayer.AudioSource.gameObject.SetActive(false);
            _audioSourcePool.ReturnToPool(audioPlayer);
            
        }
        
    }
}