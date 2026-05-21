using System.Threading;
using Cysharp.Threading.Tasks;

namespace Game.GameMode.Session.Controller.GameInitialization
{
    public interface IEnemyInitializer
    {
        public UniTask Initialize(CancellationToken cancellationToken);
        public void CleanUp();
    }
}