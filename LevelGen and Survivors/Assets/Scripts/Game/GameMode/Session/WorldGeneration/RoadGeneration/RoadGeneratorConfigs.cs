using UnityEngine;
using UnityEngine.Tilemaps;

namespace Game.GameMode.Session.WorldGeneration.RoadGeneration
{
    public class RoadGeneratorConfigs : ScriptableObject
    {
        [field: SerializeField] public int SplineSectionsCount { get; set; }
        [field: SerializeField] public int MaxRoadRadius { get; set; }
        [field: SerializeField] public int MinRoadRadius { get; set; }
        [field: SerializeField] public TileBase RoadTile { get; set; }
        [field: SerializeField] public float RoadResolution { get; set; }
        [field: SerializeField] public float CatmullRomAlpha { get; set; } = 0.5f;
    }
}