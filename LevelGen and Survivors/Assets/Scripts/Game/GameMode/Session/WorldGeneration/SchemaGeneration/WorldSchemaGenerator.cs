using System.Threading;
using Cysharp.Threading.Tasks;
using NoiseDotNet;

namespace Game.GameMode.Session.WorldGeneration.SchemaGeneration
{
    public class WorldSchemaGenerator : IWorldSchemaGenerator
    {
        public UniTask Generate(int layer, WorldGenerationRequest request, NoiseSettings settings,
            WorldSchemaGenerationConfigs worldSchemaGenerationConfigs, CancellationToken cancellationToken)
        {
            LayerGenerationConfig config = worldSchemaGenerationConfigs.Configs[layer];

            for (int i = 0; i < request.ChunkSize.x; i++)
            {
                for (int j = 0; j < request.ChunkSize.y; j++)
                {
                    int index = j * request.ChunkSize.x + i;
                    request.CoordsX[index] = request.WorldOffset.x + i;
                    request.CoordsY[index] = request.WorldOffset.y + j;
                }
            }
            
            Noise.GradientNoise2D(request.CoordsX, request.CoordsY, request.Chunk, settings);

            
            for (int i = 0; i < request.ChunkSize.x; i++)
            {
                for (int j = 0; j < request.ChunkSize.y; j++)
                {
                    int index = j * request.ChunkSize.x + i;
                    request.Chunk[index] = request.Chunk[index] > config.Threshold ? 1 : 0;
                }
            }

            return UniTask.CompletedTask;
        }
        
    }
}