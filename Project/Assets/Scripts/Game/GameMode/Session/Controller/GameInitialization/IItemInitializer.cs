using System.Threading;
using Cysharp.Threading.Tasks;

namespace Game.GameMode.Session.Controller.GameInitialization
{
    public interface IItemInitializer
    {
        public UniTask Initialize(CancellationToken cancellationToken);
        public void CleanUp();
    }
}