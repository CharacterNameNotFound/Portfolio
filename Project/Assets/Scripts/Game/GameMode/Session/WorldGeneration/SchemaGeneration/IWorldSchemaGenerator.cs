using System.Threading;
using Cysharp.Threading.Tasks;
using NoiseDotNet;

namespace Game.GameMode.Session.WorldGeneration.SchemaGeneration
{
    public interface IWorldSchemaGenerator
    {
        public UniTask Generate(int layer,
            WorldGenerationRequest request,
            NoiseSettings settings,
            WorldSchemaGenerationConfigs worldSchemaGenerationConfigs,
            CancellationToken cancellationToken);
    }
}