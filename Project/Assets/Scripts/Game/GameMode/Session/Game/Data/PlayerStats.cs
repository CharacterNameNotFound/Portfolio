using System;

namespace Game.GameMode.Session.Game.Data
{
    [Serializable]
    public class PlayerStats
    {
        public float CameraSize;
        public float MoveSpeed;

        public PlayerStats()
        {
            
        }
        
        public PlayerStats(PlayerStats playerStats)
        {
            MoveSpeed = playerStats.MoveSpeed;
            CameraSize = playerStats.CameraSize;
        }
        
    }
}