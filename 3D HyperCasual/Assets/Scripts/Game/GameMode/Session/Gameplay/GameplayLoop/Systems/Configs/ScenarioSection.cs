using System;
using UnityEngine;

namespace Game.GameMode.Session.Gameplay.GameplayLoop.Systems.Configs
{
    [Serializable]
    public class ScenarioSection
    {
        public float SectionLength;
        public float SpawnFrequency;
        public Vector2Int SpawnCount;
        public float CubeSpeed;
        public float PreSectionDelay;
    }
}