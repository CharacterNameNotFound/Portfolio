
using UnityEngine;

namespace Game.GameMode.Session.WorldGeneration.RoadGeneration
{
    public struct RoadVertex
    {
        public Vector2 Coords;
        public int Group;
        public int DrawnEdges;
        public int Index;

        public RoadVertex(Vector2 coords, int index)
        {
            Coords = coords;
            Group = index;
            Index = index;
            
            DrawnEdges = 0;
        }

        public void IncrementEdges()
        {
            DrawnEdges++;
        }
        
        
    }
}