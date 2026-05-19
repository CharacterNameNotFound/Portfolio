using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.Session.Game.Data;
using Game.GameMode.Session.Game.Data.Enteties;
using Game.GameMode.Session.Pools.EnemyBuilding;

namespace Game.GameMode.Session.Controller.GameInitialization
{
    public class EnemyInitializer : IEnemyInitializer
    {
        private EnemyInitializerConfigs _enemyInitializerConfigs;
        private EnemyPool _enemyPool;
        private SessionRegistry _sessionRegistry;

        public EnemyInitializer(EnemyInitializerConfigs enemyInitializerConfigs, EnemyPool enemyPool, SessionRegistry sessionRegistry)
        {
            _enemyInitializerConfigs = enemyInitializerConfigs;
            _enemyPool = enemyPool;
            _sessionRegistry = sessionRegistry;
        }

        public UniTask Initialize(CancellationToken cancellationToken)
        {
            return _enemyPool.ExtendBy(_enemyInitializerConfigs.PoolSize, cancellationToken);
        }

        public void CleanUp()
        {
            foreach (EnemyComponent enemy in _sessionRegistry.Enemies)
            {
                _enemyPool.ReturnToPool(enemy);
            }
            
            _sessionRegistry.Enemies.Clear();
            _enemyPool.ReleaseAll();
        }
    }
}