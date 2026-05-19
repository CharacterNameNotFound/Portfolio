using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.Session.Game.Data;
using UnityEngine;
using UnityEngine.ResourceManagement.ResourceProviders;
using Utils.UtilityTypes.AssetReferencing;

namespace Game.GameMode.Session.Controller.GameInitialization
{
    public class PlayerSpawner : IPlayerSpawner
    {
        private PlayerSpawnerConfigs _playerSpawnerConfigs;
        private SessionRegistry _sessionRegistry;
        
        public PlayerSpawner(PlayerSpawnerConfigs playerSpawnerConfigs, SessionRegistry sessionRegistry)
        {
            _playerSpawnerConfigs = playerSpawnerConfigs;
            _sessionRegistry = sessionRegistry;
        }

        public async UniTask SpawnPlayer(CancellationToken cancellationToken)
        {
            GameObject playerObject = await _playerSpawnerConfigs.PlayerCharacterComponent.Instantiate(new InstantiationParameters(), cancellationToken);

            _sessionRegistry.PlayerCharacterComponent = playerObject.GetComponent<PlayerCharacterComponent>();

            playerObject.transform.position = _sessionRegistry.GameField.Bounds.center;

            _sessionRegistry.PlayerStats = new PlayerStats(_playerSpawnerConfigs.PlayerStats);

        }

        public async UniTask SpawnCamera(CancellationToken cancellationToken)
        {
            GameObject playerCamera = await _playerSpawnerConfigs.PlayerCamera.Instantiate(new InstantiationParameters(), cancellationToken);
            _sessionRegistry.PlayerCameraComponent = playerCamera.GetComponent<PlayerCameraComponent>();
            
            _sessionRegistry.PlayerCameraComponent.SetSize(_playerSpawnerConfigs.PlayerStats.CameraSize);
        }
        
    }
}