using UnityEngine;

namespace Game.GameMode.Session.WorldGeneration.SchemaGeneration
{
    public class WorldSchemaGenerationConfigs : ScriptableObject
    {
        [field: SerializeField] public LayerGenerationConfig[] Configs { get; private set; }
        
    }
}