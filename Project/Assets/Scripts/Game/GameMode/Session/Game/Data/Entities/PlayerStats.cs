using System;
using UnityEngine;

namespace Game.GameMode.Session.Game.Data.Entities
{
    [Serializable]
    public class PlayerStats
    {
        public float CameraSize;
        public float MoveSpeed;
        public float MaxHp;
        public float CollectionRadius;
        public float RequiredExpPerLevel;


        [Header("Weapon stats")] 
        public int ProjectileCount;
        public float DamageModifier = 1;
        public float CooldownModifier = 1;
        public float RadiusModifier = 1;
        
        
        [HideInInspector] public float CurrentHp;
        [HideInInspector] public int Level;
        [HideInInspector] public float CurrentExp;

        public PlayerStats()
        {
            
        }
        
        public PlayerStats(PlayerStats playerStats)
        {
            MoveSpeed = playerStats.MoveSpeed;
            CameraSize = playerStats.CameraSize;
            MaxHp = playerStats.MaxHp;
            CurrentHp = playerStats.CurrentHp;
            CollectionRadius = playerStats.CollectionRadius;
            RequiredExpPerLevel = playerStats.RequiredExpPerLevel;
        }

    }
}