using System.Threading;
using Cysharp.Threading.Tasks;

namespace Game.GameMode.Session.Controller.GameInitialization
{
    public interface IScenarioDataInitializer
    {
        public UniTask InitializeScenarioData(CancellationToken cancellationToken);
    }
}