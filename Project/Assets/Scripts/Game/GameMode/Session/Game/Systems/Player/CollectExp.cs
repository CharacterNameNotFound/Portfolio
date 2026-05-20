using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.Session.Game.Data;
using Game.GameMode.Session.Game.Utilities;
using Game.GameMode.Session.Pools.ExperiencePool;

namespace Game.GameMode.Session.Game.Systems.Player
{
    public class CollectExp : ILoopedSystem
    {
        private ExpGemPool _expGemPool;

        public CollectExp(ExpGemPool expGemPool)
        {
            _expGemPool = expGemPool;
        }

        public UniTask Initialize(SessionRegistry sessionRegistry, CancellationToken cancellationToken)
        {
            return UniTask.CompletedTask;
        }

        public UniTask Update(float deltaTime, SessionRegistry sessionRegistry, CancellationToken cancellationToken)
        {
            for (int i = 0; i < sessionRegistry.ExpGems.Count; i++)
            {
                if (!CollisionChecks.IsCirclesCollided(
                        sessionRegistry.ExpGems[i].Transform.position, 
                        sessionRegistry.PlayerCharacterComponent.Transform.position, 
                        0, 
                        sessionRegistry.PlayerStats.CollectionRadius))
                {
                    continue;
                }

                sessionRegistry.PlayerStats.CurrentExp += sessionRegistry.ExpGems[i].Value;
                _expGemPool.ReturnToPool(sessionRegistry.ExpGems[i]);
                sessionRegistry.ExpGems.RemoveAt(i);
                i--;
            }

            return UniTask.CompletedTask;
        }
    }
}