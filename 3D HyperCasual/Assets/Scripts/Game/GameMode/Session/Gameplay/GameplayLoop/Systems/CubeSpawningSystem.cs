using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.Session.Gameplay.Entities;
using Game.GameMode.Session.Gameplay.GameplayLoop.Systems.Configs;
using Game.GameMode.Session.Gameplay.Pools.CubePooling;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.GameMode.Session.Gameplay.GameplayLoop.Systems
{
    public class CubeSpawningSystem : ILoopedSystem
    {
        private GameCubePool _gameCubePool;
        private GameCubeSystemsConfigs _configs;

        private int _sectionIndex;
        private float _sectionDurationLeft;
        private float _spawnCooldown;
        private List<int> _spawnPointBuffer;
        
        public CubeSpawningSystem(GameCubePool gameCubePool, GameCubeSystemsConfigs configs)
        {
            _gameCubePool = gameCubePool;
            _configs = configs;
        }

        public UniTask Initialize(SessionRegistry sessionRegistry, CancellationToken cancellationToken)
        {
            _sectionIndex = 0;
            _spawnCooldown = 1;
            _sectionDurationLeft = _configs.Scenario.First().SectionLength;
            _spawnPointBuffer = new List<int>(4);
            return UniTask.CompletedTask;
        }

        public async UniTask Update(float delta, SessionRegistry sessionRegistry, CancellationToken cancellationToken)
        {
            UpdatePositions(delta, sessionRegistry);
            
            _spawnCooldown -= delta;
            _sectionDurationLeft -= delta;
            
            if (_sectionDurationLeft <= 0)
            {
                _sectionIndex++;
                _sectionIndex = Math.Clamp(_sectionIndex, 0, _configs.Scenario.Count - 1);
                _sectionDurationLeft = _configs.Scenario[_sectionIndex].SectionLength;
                _spawnCooldown = _configs.Scenario[_sectionIndex].PreSectionDelay;
            }
            
            if (_spawnCooldown > 0)
            {
                return;
            }
            
            await Spawn(sessionRegistry, cancellationToken);
        }

        public UniTask CleanUp(SessionRegistry sessionRegistry, CancellationToken cancellationToken)
        {
            foreach (GameCubeComponent item in sessionRegistry.ActiveCubes)
            {
                _gameCubePool.ReturnToPool(item);
            }
            
            foreach (GameCubeComponent item in sessionRegistry.SpawningCubes)
            {
                _gameCubePool.ReturnToPool(item);
            }

            return UniTask.CompletedTask;
        }

        private void UpdatePositions(float delta, SessionRegistry sessionRegistry)
        {
            for (int i = 0; i < sessionRegistry.SpawningCubes.Count; i++)
            {
                GameCubeComponent cube = sessionRegistry.SpawningCubes[i];
                Vector3 position = cube.Transform.position + new Vector3(0, -cube.Speed * delta, 0);
                
                if (sessionRegistry.GameFieldComponent.SpawnPoints[cube.Line].transform.position.y > position.y)
                {
                    position.y = sessionRegistry.GameFieldComponent.SpawnPoints[cube.Line].transform.position.y;
                    sessionRegistry.SpawningCubes.RemoveAt(i);
                    i--;
                    
                    sessionRegistry.ActiveCubes.Add(cube);
                }
                
                cube.Transform.position = position;
            }
        }

        private async UniTask Spawn(SessionRegistry sessionRegistry, CancellationToken cancellationToken)
        {
            _spawnPointBuffer.Clear();

            ScenarioSection scenario = _configs.Scenario[_sectionIndex];
            int spawnCount = Random.Range(scenario.SpawnCount.x, scenario.SpawnCount.y);

            for (int i = 0; i < sessionRegistry.GameFieldComponent.SpawnPoints.Count; i++)
            {
                _spawnPointBuffer.Add(i);
            }
            
            for (int i = 0; i < spawnCount; i++)
            {
                GameCubeComponent cube = await _gameCubePool.GetObject(cancellationToken);

                int index = Random.Range(0, _spawnPointBuffer.Count);
                cube.Line = _spawnPointBuffer[index];
                _spawnPointBuffer.RemoveAt(index);

                Vector3 position = sessionRegistry.GameFieldComponent.SpawnPoints[cube.Line].transform.position;

                position.y = _configs.SpawnHeight;
                
                cube.Transform.position = position;
                cube.Speed = scenario.CubeSpeed;
                cube.SetColor(_configs.Colors[Random.Range(0, _configs.Colors.Count)]);
                cube.Transform.SetParent(null);
                cube.gameObject.SetActive(true);
                
                sessionRegistry.SpawningCubes.Add(cube);
            }

            _spawnCooldown = scenario.SpawnFrequency;
        }
        
    }
}