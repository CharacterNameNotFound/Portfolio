using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.Session.WorldGeneration;

namespace Game.GameMode.Session.Controller.GameInitialization
{
    public class SessionInitializer : ISessionInitializer
    {
        private IWorldGenerationController _worldGenerationController;
        private IPlayerSpawner _playerSpawner;
        private IEnemyInitializer _enemyInitializer;
        private IScenarioDataInitializer _dataInitializer;
        private IItemInitializer _sessionInitializer;

        public SessionInitializer(
            IWorldGenerationController worldGenerationController, 
            IPlayerSpawner playerSpawner, 
            IEnemyInitializer enemyInitializer, 
            IScenarioDataInitializer dataInitializer, 
            IItemInitializer sessionInitializer)
        {
            _worldGenerationController = worldGenerationController;
            _playerSpawner = playerSpawner;
            _enemyInitializer = enemyInitializer;
            _dataInitializer = dataInitializer;
            _sessionInitializer = sessionInitializer;
        }


        public async UniTask InitializeSession(WorldGenerationConfigs worldGenerationConfigs, CancellationToken cancellationToken)
        {
            await _worldGenerationController.GenerateWorld(worldGenerationConfigs, cancellationToken);
            await _playerSpawner.SpawnPlayer(cancellationToken);
            await _playerSpawner.SpawnCamera(cancellationToken);
            await _enemyInitializer.Initialize(cancellationToken);
            await _dataInitializer.InitializeScenarioData(cancellationToken);
            await _sessionInitializer.Initialize(cancellationToken);
        }

        public void CleanUp()
        {
            _enemyInitializer.CleanUp();
            _playerSpawner.CleanUp();
            _sessionInitializer.CleanUp();
        }
        
        
    }
}