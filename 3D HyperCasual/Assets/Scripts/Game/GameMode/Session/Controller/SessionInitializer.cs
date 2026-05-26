using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.Session.Gameplay.Entities;
using Game.GameMode.Session.Gameplay.GameplayLoop;
using Game.GameMode.Session.Gameplay.Inputs;
using Game.GameMode.Session.Gameplay.Pools.CubePooling;
using Game.GameMode.Session.Gameplay.Pools.PoppedCubeParticlePooling;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;
using Utils.UtilityTypes.AssetReferencing;

namespace Game.GameMode.Session.Controller
{
    public class SessionInitializer : ISessionInitializer
    {
        private SessionInitializationConfigs _sessionInitializationConfigs;
        private GameCubePool _gameCubePool;
        private SessionRegistry _sessionRegistry;
        private PoppedCubeParticlePool _poppedCubeParticlePool;
        private InputBuffer _inputBuffer;

        public SessionInitializer(
            SessionInitializationConfigs sessionInitializationConfigs, 
            GameCubePool gameCubePool, 
            SessionRegistry sessionRegistry, 
            PoppedCubeParticlePool poppedCubeParticlePool, 
            InputBuffer inputBuffer)
        {
            _sessionInitializationConfigs = sessionInitializationConfigs;
            _gameCubePool = gameCubePool;
            _sessionRegistry = sessionRegistry;
            _poppedCubeParticlePool = poppedCubeParticlePool;
            _inputBuffer = inputBuffer;
        }

        public async UniTask Initialize(CancellationToken cancellationToken)
        {
            await _gameCubePool.ExtendBy(_sessionInitializationConfigs.CubesPooledCount, cancellationToken);
            await _poppedCubeParticlePool.ExtendBy(_sessionInitializationConfigs.CubesPooledCount, cancellationToken);
            
            GameObject gameFieldSO = await _sessionInitializationConfigs.GameFieldAssetReference.Instantiate(new InstantiationParameters(), cancellationToken);
            _sessionRegistry.GameFieldComponent = gameFieldSO.GetComponent<GameFieldComponent>();

            _sessionRegistry.LivesMax = _sessionInitializationConfigs.Lives;
            _sessionRegistry.Lives = _sessionInitializationConfigs.Lives;
            _sessionRegistry.Score = 0;
            _sessionRegistry.ActiveCubes = new List<GameCubeComponent>(_sessionInitializationConfigs.CubesPooledCount / 2);
            _sessionRegistry.SpawningCubes = new List<GameCubeComponent>(_sessionInitializationConfigs.CubesPooledCount / 2);

            _inputBuffer.Clear();
        }

        public void CleanUp()
        {
            _gameCubePool.ReleaseAll();
            _poppedCubeParticlePool.ReleaseAll();

            Addressables.ReleaseInstance(_sessionRegistry.GameFieldComponent.gameObject);
            _sessionRegistry.ActiveCubes = null;
            _sessionRegistry.SpawningCubes = null;
        }
        
    }
}