using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.Session.WorldGeneration;

namespace Game.GameMode.Session.Controller.GameInitialization
{
    public interface ISessionInitializer
    {
        public UniTask InitializeSession(WorldGenerationConfigs worldGenerationConfigs, CancellationToken cancellationToken);


    }
}