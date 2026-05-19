using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.Session.WorldGeneration;

namespace Game.GameMode.Session.Controller.GameInitialization
{
    public class SessionInitializer : ISessionInitializer
    {
        private IWorldGenerationController _worldGenerationController;
        private IPlayerSpawner _playerSpawner;

        public SessionInitializer(IWorldGenerationController worldGenerationController, IPlayerSpawner playerSpawner)
        {
            _worldGenerationController = worldGenerationController;
            _playerSpawner = playerSpawner;
        }


        public async UniTask InitializeSession(WorldGenerationConfigs worldGenerationConfigs, CancellationToken cancellationToken)
        {
            await _worldGenerationController.GenerateWorld(worldGenerationConfigs, cancellationToken);
            await _playerSpawner.SpawnPlayer(cancellationToken);
            await _playerSpawner.SpawnCamera(cancellationToken);
        }
        
    }
}