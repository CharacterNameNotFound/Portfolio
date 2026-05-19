using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Game.GameMode.Session.WorldGeneration.RoadGeneration
{
    public interface IRoadGenerator
    {
        public UniTask Generate(Vector2Int worldSize, Tilemap tilemap, RoadGeneratorConfigs roadGeneratorConfigs, CancellationToken cancellationToken);
    }
}