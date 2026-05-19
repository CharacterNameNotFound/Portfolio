using System;
using UnityEngine;

namespace Game.GameMode.Session.Game.Data.Enemies
{
    [Serializable]
    public class EnemyWave
    {
        public Sprite Sprite;

        public int SpawnCount;
        
        public float Radius;
        public float Hp;
        public float Speed;
        public float Dps;
        public float ExpReward;
    }
}