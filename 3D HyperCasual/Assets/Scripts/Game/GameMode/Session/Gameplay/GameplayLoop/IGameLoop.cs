using System.Threading;
using Cysharp.Threading.Tasks;

namespace Game.GameMode.Session.Gameplay.GameplayLoop
{
    public interface IGameLoop
    {
        public UniTask StartLoop(CancellationToken cancellationToken);
    }
}