using System.Collections.Generic;
using Game.GameMode.Session.Game.Data;
using Game.GameMode.Session.Game.Data.Entities;
using UnityEngine;

namespace Game.GameMode.Session.WorldGeneration.DecorationGeneration
{
    public class DecorationGeneratorConfigs : ScriptableObject
    {
        [field: SerializeField] public List<DecorationComponent> Decorations { get; private set; }
        [field: SerializeField] public Vector2Int DecoratingChunkSize { get; set; }
        [field: SerializeField] public int DecoratingChunkDecorationCount { get; set; }
        [field: SerializeField] public float MinimumDistanceBetweenDecos { get; set; }
        [field: SerializeField] public int SanityCount { get; set; }
        
        
        
    }
}