using System.Threading;
using Cysharp.Threading.Tasks;

namespace Game.GameMode.Session.Controller.GameInitialization
{
    public interface IPlayerSpawner
    {
        public UniTask SpawnPlayer(CancellationToken cancellationToken);
        public UniTask SpawnCamera(CancellationToken cancellationToken);
        public void CleanUp();
    }
}