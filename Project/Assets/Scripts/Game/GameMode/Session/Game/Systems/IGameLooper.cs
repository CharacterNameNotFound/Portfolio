using System.Threading;
using Cysharp.Threading.Tasks;
using GameWideSystems.UIManagement;

namespace Game.GameMode.Session.Game.Systems
{
    public interface IGameLooper
    {
        public UniTask StartLoop(ScreenHolder sessionScreen, CancellationToken cancellationToken);
    }
}