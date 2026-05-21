using System.Threading;
using Cysharp.Threading.Tasks;

namespace Game.GameMode.Session.WorldGeneration
{
    public interface IWorldGenerationController
    {
        public UniTask GenerateWorld(WorldGenerationConfigs worldGenerationConfigs, CancellationToken cancellationToken);
    }
}