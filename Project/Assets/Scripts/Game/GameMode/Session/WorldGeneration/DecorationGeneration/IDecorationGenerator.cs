using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game.GameMode.Session.WorldGeneration.DecorationGeneration
{
    public interface IDecorationGenerator
    {
        public UniTask Decorate(Transform holder, Vector2Int worldSize,
            DecorationGeneratorConfigs decorationGeneratorConfigs,
            CancellationToken cancellationToken);
    }
}