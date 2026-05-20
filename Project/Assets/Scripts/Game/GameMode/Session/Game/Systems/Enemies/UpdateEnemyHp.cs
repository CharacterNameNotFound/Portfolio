using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.Session.Game.Data;
using Game.GameMode.Session.Game.Data.Entities;
using Game.GameMode.Session.Pools.EnemyBuilding;
using Game.GameMode.Session.Pools.ExperiencePool;
using UnityEngine.Analytics;

namespace Game.GameMode.Session.Game.Systems.Enemies
{
    public class UpdateEnemyHp : ILoopedSystem
    {
        private EnemyPool _enemyPool;
        private ExpGemPool _expGemPool;
        
        public UpdateEnemyHp(EnemyPool enemyPool, ExpGemPool expGemPool)
        {
            _enemyPool = enemyPool;
            _expGemPool = expGemPool;
        }

        public UniTask Initialize(SessionRegistry sessionRegistry, CancellationToken cancellationToken)
        {
            return UniTask.CompletedTask;
        }

        public async UniTask Update(float deltaTime, SessionRegistry sessionRegistry, CancellationToken cancellationToken)
        {
            for (int i = 0; i < sessionRegistry.Enemies.Count; i++)
            {
                if (sessionRegistry.Enemies[i].Hp > 0)
                {
                    continue;
                }

                ExpGemComponent expGem = await _expGemPool.GetObject(cancellationToken);
                expGem.Transform.position = sessionRegistry.Enemies[i].Transform.position;
                expGem.Value = sessionRegistry.Enemies[i].Exp;
                expGem.gameObject.SetActive(true);
                sessionRegistry.ExpGems.Add(expGem);
                
                _enemyPool.ReturnToPool(sessionRegistry.Enemies[i]);
                sessionRegistry.Enemies.RemoveAt(i);
                i--;
            }
            
            
        }
    }
}