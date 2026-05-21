using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.Session.Game.Data;
using Game.GameMode.Session.Game.Data.Entities;
using UnityEngine;
using UnityEngine.AddressableAssets;
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
            
            await _playerSpawnerConfigs.TestWeapon.Initialize(cancellationToken);
            await _playerSpawnerConfigs.TestWeapon.OnObtained(_sessionRegistry, cancellationToken);
        }

        public async UniTask SpawnCamera(CancellationToken cancellationToken)
        {
            GameObject playerCamera = await _playerSpawnerConfigs.PlayerCamera.Instantiate(new InstantiationParameters(), cancellationToken);
            _sessionRegistry.PlayerCameraComponent = playerCamera.GetComponent<PlayerCameraComponent>();
            
            _sessionRegistry.PlayerCameraComponent.SetSize(_playerSpawnerConfigs.PlayerStats.CameraSize);
        }

        public void CleanUp()
        {
            Addressables.ReleaseInstance(_sessionRegistry.PlayerCameraComponent.gameObject);
            Addressables.ReleaseInstance(_sessionRegistry.PlayerCharacterComponent.gameObject);
            
            _sessionRegistry.ObtainedItems.Clear();
        }
        
    }
}