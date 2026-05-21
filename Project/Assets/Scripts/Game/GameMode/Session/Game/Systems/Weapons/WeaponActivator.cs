using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.Session.Game.Data;

namespace Game.GameMode.Session.Game.Systems.Weapons
{
    public class WeaponActivator : ILoopedSystem
    {
        public UniTask Initialize(SessionRegistry sessionRegistry, CancellationToken cancellationToken)
        {
            return UniTask.CompletedTask;
        }

        public async UniTask Update(float deltaTime, SessionRegistry sessionRegistry, CancellationToken cancellationToken)
        {
            for (int i = 0; i < sessionRegistry.ObtainedItems.Count; i++)
            {
                await sessionRegistry.ObtainedItems[i].UpdateInternal(deltaTime, i, sessionRegistry, cancellationToken);
            }
            
        }
    }
}