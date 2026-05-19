using UnityEngine;

namespace Game.GameMode.Session.Game.Data
{
    public class GameField
    {
        public Bounds Bounds;
        
        public GameField(Bounds bounds)
        {
            Bounds = bounds;
        }
    }
}