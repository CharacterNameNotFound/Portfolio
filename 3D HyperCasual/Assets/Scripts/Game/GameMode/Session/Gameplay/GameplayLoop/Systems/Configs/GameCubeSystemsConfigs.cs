using System.Collections.Generic;
using UnityEngine;

namespace Game.GameMode.Session.Gameplay.GameplayLoop.Systems.Configs
{
    public class GameCubeSystemsConfigs : ScriptableObject
    {
        [SerializeField] private List<Color> _colors;
        [SerializeField] private List<ScenarioSection> _scenarioSections;
        [SerializeField] private float _spawnHeight;
        [SerializeField] private float _despawnZ;

        public IReadOnlyList<Color> Colors => _colors;
        public IReadOnlyList<ScenarioSection> Scenario => _scenarioSections;
        public float SpawnHeight => _spawnHeight;
        public float DespawnZ => _despawnZ;
    }
}