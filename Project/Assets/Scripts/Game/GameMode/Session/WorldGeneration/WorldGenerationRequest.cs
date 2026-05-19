using UnityEngine;
using UnityEngine.Tilemaps;

namespace Game.GameMode.Session.WorldGeneration
{
    public class WorldGenerationRequest
    {
        // noise generation buffers
        public float[] Chunk;
        public float[] CoordsX;
        public float[] CoordsY;
        
        // tile application buffer
        public Vector3Int[] Positions;
        public TileBase[] Tiles;

        // chunking data
        public Vector2Int WorldSize;
        public Vector2Int MaxChunkSize;
        public Vector2Int ChunkSize;
        public Vector2Int WorldOffset;
        
        
        public WorldGenerationRequest(Vector2Int worldSize, Vector2Int maxChunkSize)
        {
            WorldSize = worldSize;
            MaxChunkSize = maxChunkSize;
            ChunkSize = maxChunkSize;
            
            Chunk = new float[maxChunkSize.x * maxChunkSize.y];
            CoordsX = new float[maxChunkSize.x * maxChunkSize.y];
            CoordsY = new float[maxChunkSize.x * maxChunkSize.y];
            
            Positions = new Vector3Int[maxChunkSize.x * maxChunkSize.y];
            Tiles = new TileBase[maxChunkSize.x * maxChunkSize.y];
        }

    }
}