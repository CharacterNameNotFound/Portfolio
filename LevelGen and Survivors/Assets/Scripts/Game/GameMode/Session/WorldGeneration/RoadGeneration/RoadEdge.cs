namespace Game.GameMode.Session.WorldGeneration.RoadGeneration
{
    public struct RoadEdge
    {
        public int FirstVertexIndex;
        public int SecondVertexIndex;
        public float Length;
        public bool Draw;

        public RoadEdge(int firstVertexIndex, int secondVertexIndex, float length, bool draw)
        {
            FirstVertexIndex = firstVertexIndex;
            SecondVertexIndex = secondVertexIndex;
            Length = length;
            Draw = draw;
        }

        public void SetDraw()
        {
            Draw = true;
        }

        public bool IsConnectedEdge(RoadEdge edge, int searchedConnection, out int point)
        {
            // excluding same edge
            if ((FirstVertexIndex == edge.FirstVertexIndex && SecondVertexIndex == edge.SecondVertexIndex) || 
                (FirstVertexIndex == edge.SecondVertexIndex && SecondVertexIndex == edge.FirstVertexIndex))
            {
                point = -1;
                return false;
            }

            // connected by first vert
            if (searchedConnection == FirstVertexIndex)
            {
                point = SecondVertexIndex;
                return true;
            }
            
            // connected by second vert
            if (searchedConnection == SecondVertexIndex)
            {
                point = FirstVertexIndex;
                return true;
            }

            point = -1;
            return false;
        }

        public bool GetOppositeVertIndex(int index, out int oppositeIndex)
        {
            if (index == FirstVertexIndex)
            {
                oppositeIndex = SecondVertexIndex;
                return true;
            }

            if (index == SecondVertexIndex)
            {
                oppositeIndex = FirstVertexIndex;
                return true;
            }
            
            oppositeIndex = -1;
            return false;
        }
        
    }
}