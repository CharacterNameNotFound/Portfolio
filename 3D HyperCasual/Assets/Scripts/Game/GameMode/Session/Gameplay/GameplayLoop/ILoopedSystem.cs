using System.Threading;
using Cysharp.Threading.Tasks;

namespace Game.GameMode.Session.Gameplay.GameplayLoop
{
    public interface ILoopedSystem
    {
        public UniTask Initialize(SessionRegistry sessionRegistry, CancellationToken cancellationToken);
        public UniTask Update(float delta, SessionRegistry sessionRegistry, CancellationToken cancellationToken);
        public UniTask CleanUp(SessionRegistry sessionRegistry, CancellationToken cancellationToken);
    }
}