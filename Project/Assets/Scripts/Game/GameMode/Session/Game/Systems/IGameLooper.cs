using System.Threading;
using Cysharp.Threading.Tasks;

namespace Game.GameMode.Session.Game.Systems
{
    public interface IGameLooper
    {
        public UniTask StartLoop(CancellationToken cancellationToken);
    }
}