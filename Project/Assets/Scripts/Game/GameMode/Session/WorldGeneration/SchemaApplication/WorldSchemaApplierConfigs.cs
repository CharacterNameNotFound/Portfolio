using UnityEngine;

namespace Game.GameMode.Session.WorldGeneration.SchemaApplication
{
    public class WorldSchemaApplierConfigs : ScriptableObject
    {
        [field: SerializeField] public RuleTile[] Tile { get; private set; }
    }
}