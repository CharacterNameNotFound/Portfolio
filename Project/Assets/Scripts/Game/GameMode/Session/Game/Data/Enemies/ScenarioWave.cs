using System;

namespace Game.GameMode.Session.Game.Data.Enemies
{
    [Serializable]
    public class ScenarioWave
    {
        public EnemyWave EnemyWave;
        public SpawnType SpawnType;
        public float SpawnFrequency;
        public float SegmentLength;
    }
}