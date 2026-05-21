using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Game.GameMode.Session.WorldGeneration.SchemaApplication
{
    public class WorldSchemaApplier : IWorldSchemaApplier
    {
        public UniTask ApplySchemaArray(
            int layer, 
            WorldGenerationRequest request,
            Tilemap tilemap,
            WorldSchemaApplierConfigs worldSchemaApplierConfigs,
            CancellationToken cancellationToken)
        {
            
            RuleTile ruleTile = worldSchemaApplierConfigs.Tile[layer];
            
            for (int i = 0; i < request.ChunkSize.x; i++)
            {
                for (int j = 0; j < request.ChunkSize.y; j++)
                {
                    int index = j * request.ChunkSize.x + i;
                    
                    request.Positions[index] = new Vector3Int(i + request.WorldOffset.x, j + request.WorldOffset.y, 0);
                    request.Tiles[index] = request.Chunk[index] >= 0.5f ? ruleTile : null;
                }
            }
            
            tilemap.SetTiles(request.Positions, request.Tiles);

            return UniTask.CompletedTask;
        }
        
        
        
    }
}