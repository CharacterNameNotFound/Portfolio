using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine.Tilemaps;

namespace Game.GameMode.Session.WorldGeneration.SchemaApplication
{
    public interface IWorldSchemaApplier
    {
        public UniTask ApplySchemaArray(
            int layer,
            WorldGenerationRequest request,
            Tilemap tilemap,
            WorldSchemaApplierConfigs worldSchemaApplierConfigs,
            CancellationToken cancellationToken);
    }
}