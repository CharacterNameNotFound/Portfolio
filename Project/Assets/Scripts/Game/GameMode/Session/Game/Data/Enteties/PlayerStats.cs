using System;

namespace Game.GameMode.Session.Game.Data.Enteties
{
    [Serializable]
    public class PlayerStats
    {
        public float CameraSize;
        public float MoveSpeed;
        public float MaxHp;
        
        
        
        public float CurrentHp;

        public PlayerStats()
        {
            
        }
        
        public PlayerStats(PlayerStats playerStats)
        {
            MoveSpeed = playerStats.MoveSpeed;
            CameraSize = playerStats.CameraSize;
            MaxHp = playerStats.MaxHp;
            CurrentHp = playerStats.CurrentHp;
        }
        
    }
}