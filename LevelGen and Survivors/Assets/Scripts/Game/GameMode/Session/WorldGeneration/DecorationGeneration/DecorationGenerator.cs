using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.Session.Game.Data;
using Game.GameMode.Session.Game.Data.Entities;
using UnityEngine;

namespace Game.GameMode.Session.WorldGeneration.DecorationGeneration
{
    public class DecorationGenerator : IDecorationGenerator
    {
        private SessionRegistry _sessionRegistry;

        public DecorationGenerator(SessionRegistry sessionRegistry)
        {
            _sessionRegistry = sessionRegistry;
        }


        public UniTask Decorate(Transform holder, Vector2Int worldSize, DecorationGeneratorConfigs decorationGeneratorConfigs, CancellationToken cancellationToken)
        {
            int horizontalSectionCount = worldSize.x / decorationGeneratorConfigs.DecoratingChunkSize.x;
            int verticalSectionCount = worldSize.y / decorationGeneratorConfigs.DecoratingChunkSize.y;
            
            horizontalSectionCount += worldSize.x % decorationGeneratorConfigs.DecoratingChunkSize.x > 0 ? 1 : 0;
            verticalSectionCount += worldSize.y % decorationGeneratorConfigs.DecoratingChunkSize.y > 0 ? 1 : 0;
            

            List<Vector2> takenPositions = new List<Vector2>(decorationGeneratorConfigs.DecoratingChunkDecorationCount);

            for (int i = 0; i < horizontalSectionCount; i++)
            {
                for (int j = 0; j < verticalSectionCount; j++)
                {
                    Vector2 offset = new Vector2(
                        decorationGeneratorConfigs.DecoratingChunkSize.x * i,
                        decorationGeneratorConfigs.DecoratingChunkSize.y * j); 
                    
                    PopulateChunk(offset, takenPositions, holder, decorationGeneratorConfigs);
                }
            }
            
            

            return UniTask.CompletedTask;
        }

        private void PopulateChunk(Vector2 offset, List<Vector2> takenPositions, Transform holder, DecorationGeneratorConfigs decorationGeneratorConfigs)
        {
            int sanityCount = decorationGeneratorConfigs.SanityCount;

            Vector2 halfSize = decorationGeneratorConfigs.DecoratingChunkSize / 2;
            Vector2 chunkCenter = offset + halfSize;

            float exclusionRadiusSquare = decorationGeneratorConfigs.MinimumDistanceBetweenDecos *
                                          decorationGeneratorConfigs.MinimumDistanceBetweenDecos;
            
            for (int i = 0; i < decorationGeneratorConfigs.DecoratingChunkDecorationCount;)
            {
                Vector2 insideUnitCircle = Random.insideUnitCircle;

                Vector2 spawnPoint = chunkCenter + insideUnitCircle * halfSize;

                if (IsSpawnPointLocked(spawnPoint, takenPositions, exclusionRadiusSquare))
                {
                    sanityCount--;
                    if (sanityCount < 0)
                    {
                        return;
                    }
                    
                    continue;
                }

                int randomIndex = Random.Range(0, decorationGeneratorConfigs.Decorations.Count);
                DecorationComponent decoration = Object.Instantiate(decorationGeneratorConfigs.Decorations[randomIndex], spawnPoint, Quaternion.identity, holder);

                _sessionRegistry.Decorations.Add(decoration);
                
                takenPositions.Add(spawnPoint);
                i++;
            }
            
        }

        private bool IsSpawnPointLocked(Vector2 spawnPoint, List<Vector2> takenPositions, float exclusionRadiusSquare)
        {
            return takenPositions.Any(t => (Vector2.SqrMagnitude(t - spawnPoint) < exclusionRadiusSquare));
        }
        
        
    }
}