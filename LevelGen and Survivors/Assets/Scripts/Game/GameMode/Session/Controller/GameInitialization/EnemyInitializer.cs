using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.Session.Game.Data;
using Game.GameMode.Session.Game.Data.Entities;
using Game.GameMode.Session.Pools.EnemyBuilding;
using Game.GameMode.Session.Pools.ExperiencePool;

namespace Game.GameMode.Session.Controller.GameInitialization
{
    public class EnemyInitializer : IEnemyInitializer
    {
        private EnemyInitializerConfigs _enemyInitializerConfigs;
        private EnemyPool _enemyPool;
        private SessionRegistry _sessionRegistry;
        private ExpGemPool _expGemPool;

        public EnemyInitializer(EnemyInitializerConfigs enemyInitializerConfigs, EnemyPool enemyPool, SessionRegistry sessionRegistry, ExpGemPool expGemPool)
        {
            _enemyInitializerConfigs = enemyInitializerConfigs;
            _enemyPool = enemyPool;
            _sessionRegistry = sessionRegistry;
            _expGemPool = expGemPool;
        }

        public async UniTask Initialize(CancellationToken cancellationToken)
        {
            await _enemyPool.ExtendBy(_enemyInitializerConfigs.PoolSize, cancellationToken);
            await _expGemPool.ExtendBy(_enemyInitializerConfigs.PoolSize, cancellationToken);
        }

        public void CleanUp()
        {
            foreach (EnemyComponent enemy in _sessionRegistry.Enemies)
            {
                _enemyPool.ReturnToPool(enemy);
            }
            
            _sessionRegistry.Enemies.Clear();
            _enemyPool.ReleaseAll();
            
            foreach (ExpGemComponent expGem in _sessionRegistry.ExpGems)
            {
                _expGemPool.ReturnToPool(expGem);
            }
            
            _sessionRegistry.ExpGems.Clear();
            _expGemPool.ReleaseAll();
            
        }
    }
}