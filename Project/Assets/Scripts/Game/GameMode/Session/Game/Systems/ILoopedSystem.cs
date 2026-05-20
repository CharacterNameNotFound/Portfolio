using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.Session.Game.Data;

namespace Game.GameMode.Session.Game.Systems
{
    public interface ILoopedSystem
    {
        public UniTask Initialize(SessionRegistry sessionRegistry, CancellationToken cancellationToken);
        public UniTask Update(float deltaTime, SessionRegistry sessionRegistry, CancellationToken cancellationToken);
    }
}