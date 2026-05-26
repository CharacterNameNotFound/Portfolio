using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.Session.Gameplay.Pools.PoppedCubeParticlePooling;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Utils.UtilityTypes.ObjectPooling;

namespace Game.GameMode.Session.Gameplay.Entities
{
    public class PoppedParticlesComponent : MonoBehaviour, IPoolableEntity
    {
        public ParticleSystem ParticleSystem;
        
        public void OnPooled()
        {
            gameObject.SetActive(false);
        }

        public void Dispose()
        {
            Addressables.ReleaseInstance(gameObject);
        }

        public async UniTask Play(Color cubeColor, PoppedCubeParticlePool pool, CancellationToken cancellationToken)
        {
            ParticleSystem.MainModule mainSystem = ParticleSystem.main;
            mainSystem.startColor = cubeColor;
            gameObject.SetActive(true);
            
            try
            {
                ParticleSystem.Play();
                await UniTask.WaitForSeconds(mainSystem.duration, cancellationToken: cancellationToken);
            }
            catch (Exception)
            {
                Addressables.ReleaseInstance(gameObject);
            }
            
            pool.ReturnToPool(this);
        }
    }
}