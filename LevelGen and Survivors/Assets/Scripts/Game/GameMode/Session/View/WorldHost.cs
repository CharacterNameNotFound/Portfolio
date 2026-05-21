using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Game.GameMode.Session.View
{
    public class WorldHost : MonoBehaviour
    {
        [field: SerializeField] public Tilemap Ground { get; private set; }
        [field: SerializeField] public Tilemap GroundDecorations { get; private set; }
        [field: SerializeField] public Tilemap Roads { get; private set; }
        [field: SerializeField] public Transform Decorations { get; private set; }

        public List<Tilemap> GetLevelTileMaps()
        {
            return new List<Tilemap>() { Ground, GroundDecorations };
        }
        
    }
}