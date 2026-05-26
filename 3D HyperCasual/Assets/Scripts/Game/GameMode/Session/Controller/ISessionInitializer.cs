using System.Threading;
using Cysharp.Threading.Tasks;

namespace Game.GameMode.Session.Controller
{
    public interface ISessionInitializer
    {
        public UniTask Initialize(CancellationToken cancellationToken);
        public void CleanUp();
    }
}