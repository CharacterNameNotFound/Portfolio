using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.Session.Game.Data;
using Game.GameMode.Session.Game.Data.Enteties;

namespace Game.GameMode.Session.Game.Systems.Enemies
{
    public class EnemyDamageDealer : ILoopedSystem
    {
        public UniTask Initialize(CancellationToken cancellationToken)
        {
            return UniTask.CompletedTask;
        }

        public UniTask Update(float deltaTime, SessionRegistry sessionRegistry, CancellationToken cancellationToken)
        {
            foreach (EnemyComponent enemy in sessionRegistry.Enemies)
            {
                if (!enemy.InPlayerRadius)
                {
                    continue;
                }

                sessionRegistry.PlayerStats.CurrentHp -= enemy.Dps * deltaTime;
            }
            
            return UniTask.CompletedTask;
        }
    }
}