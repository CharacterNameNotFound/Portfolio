using Game.GameMode.Session.WorldGeneration.DecorationGeneration;
using Game.GameMode.Session.WorldGeneration.RoadGeneration;
using Game.GameMode.Session.WorldGeneration.SchemaApplication;
using Game.GameMode.Session.WorldGeneration.SchemaGeneration;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Game.GameMode.Session.WorldGeneration
{
    public class WorldGenerationConfigs : ScriptableObject
    {
        [field: SerializeField] public AssetReference WorldHostAssetReference { get; set; }
        [field: SerializeField] public WorldSchemaGenerationConfigs WorldSchemaGenerationConfigs { get; set; }
        [field: SerializeField] public WorldSchemaApplierConfigs WorldSchemaApplierConfigs { get; set; }
        [field: SerializeField] public RoadGeneratorConfigs RoadGeneratorConfigs { get; set; }
        [field: SerializeField] public DecorationGeneratorConfigs DecorationGeneratorConfigs { get; set; }

        [field: SerializeField] public Vector2Int WorldSize { get; set; }
        [field: SerializeField] public Vector2Int ChunkSize { get; set; }
        
        [field: SerializeField] public float FrequencyX { get; set; }
        [field: SerializeField] public float FrequencyY { get; set; }

        public void InstantiateScriptables()
        {
            WorldSchemaGenerationConfigs = Instantiate(WorldSchemaGenerationConfigs);
            WorldSchemaApplierConfigs = Instantiate(WorldSchemaApplierConfigs);
            RoadGeneratorConfigs = Instantiate(RoadGeneratorConfigs);
            DecorationGeneratorConfigs = Instantiate(DecorationGeneratorConfigs);
        }
    }
}