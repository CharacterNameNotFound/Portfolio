using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.Session.Game.Data;
using Game.GameMode.Session.Game.Data.Enteties;
using Game.GameMode.Session.View;
using Game.GameMode.Session.WorldGeneration.DecorationGeneration;
using Game.GameMode.Session.WorldGeneration.RoadGeneration;
using Game.GameMode.Session.WorldGeneration.SchemaApplication;
using Game.GameMode.Session.WorldGeneration.SchemaGeneration;
using NoiseDotNet;
using UnityEngine;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.Tilemaps;
using Utils.UtilityTypes.AssetReferencing;

namespace Game.GameMode.Session.WorldGeneration
{
    public class WorldGenerationController : IWorldGenerationController
    {
        private IWorldSchemaGenerator _worldSchemaGenerator;
        private IWorldSchemaApplier _worldSchemaApplier;
        private IRoadGenerator _roadGenerator;
        private IDecorationGenerator _decorationGenerator;
        private SessionRegistry _sessionRegistry;

        public WorldGenerationController(
            IWorldSchemaGenerator worldSchemaGenerator, 
            IWorldSchemaApplier worldSchemaApplier, 
            IRoadGenerator roadGenerator, 
            IDecorationGenerator decorationGenerator, 
            SessionRegistry sessionRegistry)
        {
            _worldSchemaGenerator = worldSchemaGenerator;
            _worldSchemaApplier = worldSchemaApplier;
            _roadGenerator = roadGenerator;
            _decorationGenerator = decorationGenerator;
            _sessionRegistry = sessionRegistry;
        }

        public async UniTask GenerateWorld(WorldGenerationConfigs worldGenerationConfigs, CancellationToken cancellationToken)
        {
            Bounds bounds = new Bounds
            {
                min = new Vector3(10, 10, 0),
                max = new Vector3(worldGenerationConfigs.WorldSize.x - 10, worldGenerationConfigs.WorldSize.y - 10, 0)
            };
            _sessionRegistry.GameField = new GameField(bounds);
            
            NoiseSettings settings = new NoiseSettings(
                worldGenerationConfigs.FrequencyX,
                worldGenerationConfigs.FrequencyX, 
                Mathf.RoundToInt(Random.value * 1000));
            
            // generating world host
            WorldHost worldHost = await worldGenerationConfigs.WorldHostAssetReference.Instantiate<WorldHost>(new InstantiationParameters(), cancellationToken);
            List<Tilemap> levelTileMaps = worldHost.GetLevelTileMaps();

            int layerCount = levelTileMaps.Count;
            
            WorldGenerationRequest request = WorldConfigsToSchemaRequest(worldGenerationConfigs);
            
            for (int i = 0; i < layerCount; i++)
            {
                await GenerateLayer(i, request, levelTileMaps[i], settings, worldGenerationConfigs, cancellationToken);
                await UniTask.NextFrame(cancellationToken);
                request.ChunkSize = request.MaxChunkSize;
            }

            await _roadGenerator.Generate(worldGenerationConfigs.WorldSize, worldHost.Roads, worldGenerationConfigs.RoadGeneratorConfigs, cancellationToken);
            await _decorationGenerator.Decorate(worldHost.Decorations, worldGenerationConfigs.WorldSize, worldGenerationConfigs.DecorationGeneratorConfigs, cancellationToken);
        }

        private WorldGenerationRequest WorldConfigsToSchemaRequest(WorldGenerationConfigs worldGenerationConfigs)
        {
            return new WorldGenerationRequest(worldGenerationConfigs.WorldSize, worldGenerationConfigs.ChunkSize);
        }

        private async UniTask GenerateLayer(
            int layer, 
            WorldGenerationRequest request, 
            Tilemap levelTileMap,
            NoiseSettings settings, 
            WorldGenerationConfigs worldGenerationConfigs, 
            CancellationToken cancellationToken)
        {
            
            // generating full chunks
            int horizontalChunkCount = worldGenerationConfigs.WorldSize.x / worldGenerationConfigs.ChunkSize.x;
            int verticalChunkCount = worldGenerationConfigs.WorldSize.y / worldGenerationConfigs.ChunkSize.y;
            await GenerateChunk(layer, horizontalChunkCount, verticalChunkCount, Vector2Int.zero, request, settings, levelTileMap, worldGenerationConfigs, cancellationToken);
            
            // generating partial chunks
            bool isVerticalRim = worldGenerationConfigs.WorldSize.x % worldGenerationConfigs.ChunkSize.x > 0;
            bool isHorizontalRim = worldGenerationConfigs.WorldSize.y % worldGenerationConfigs.ChunkSize.y > 0;
            
            // generating top
            if (isHorizontalRim)
            {
                int verticalOffset = verticalChunkCount * request.MaxChunkSize.y;

                request.ChunkSize = new Vector2Int(request.MaxChunkSize.x, worldGenerationConfigs.WorldSize.y % worldGenerationConfigs.ChunkSize.y);
                
                await GenerateChunk(layer, horizontalChunkCount, 1, new Vector2Int(0, verticalOffset), request, settings, levelTileMap, worldGenerationConfigs, cancellationToken);
            }
            
            if (isVerticalRim)
            {
                int horizontalOffset = horizontalChunkCount * request.MaxChunkSize.x;

                request.ChunkSize = new Vector2Int(worldGenerationConfigs.WorldSize.x % worldGenerationConfigs.ChunkSize.x, request.MaxChunkSize.y);
                
                await GenerateChunk(layer, 1, verticalChunkCount, new Vector2Int(horizontalOffset, 0), request, settings, levelTileMap, worldGenerationConfigs, cancellationToken);
            }

            if (isHorizontalRim && isVerticalRim)
            {
                int horizontalOffset = horizontalChunkCount * request.MaxChunkSize.x;
                int verticalOffset = verticalChunkCount * request.MaxChunkSize.y;
                
                request.ChunkSize = new Vector2Int(worldGenerationConfigs.WorldSize.x % worldGenerationConfigs.ChunkSize.x, 
                    worldGenerationConfigs.WorldSize.y % worldGenerationConfigs.ChunkSize.y);

                await GenerateChunk(layer, 1, 1, new Vector2Int(horizontalOffset, verticalOffset), request, settings, levelTileMap, worldGenerationConfigs, cancellationToken);
            }
            
        }

        private async UniTask GenerateChunk(
            int layer, 
            int horizontalCount, 
            int verticalCount, 
            Vector2Int baseOffset, 
            WorldGenerationRequest request, 
            NoiseSettings settings,
            Tilemap levelTileMaps, 
            WorldGenerationConfigs worldGenerationConfigs, 
            CancellationToken cancellationToken)
        {
            for (int i = 0; i < horizontalCount; i++)
            {
                for (int j = 0; j < verticalCount; j++)
                {
                    Vector2Int offset = new Vector2Int(worldGenerationConfigs.ChunkSize.x * i, worldGenerationConfigs.ChunkSize.y * j);
                    request.WorldOffset = offset + baseOffset;
                    
                    await _worldSchemaGenerator.Generate(layer, request, settings, worldGenerationConfigs.WorldSchemaGenerationConfigs, cancellationToken);
                    await _worldSchemaApplier.ApplySchemaArray(layer, request, levelTileMaps, worldGenerationConfigs.WorldSchemaApplierConfigs, cancellationToken);
                }
            }
            
        }
        
        
        
        
    }
}