using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.Session.Game.Data;

namespace Game.GameMode.Session.Game.Items
{
    public interface IItem
    {
        public UniTask Initialize(CancellationToken cancellationToken);
        public UniTask OnObtained(SessionRegistry sessionRegistry, CancellationToken cancellationToken);
        public UniTask OnStatsUpdated(SessionRegistry sessionRegistry, CancellationToken cancellationToken);
        public UniTask UpdateInternal(float deltaTime, int itemIndex, SessionRegistry sessionRegistry, CancellationToken cancellationToken);
        public UniTask CleanUp(CancellationToken cancellationToken);
    }
}