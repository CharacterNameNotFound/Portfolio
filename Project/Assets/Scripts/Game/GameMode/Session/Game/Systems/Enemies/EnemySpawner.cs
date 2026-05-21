using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.Session.Game.Data;
using Game.GameMode.Session.Game.Data.Enemies;
using Game.GameMode.Session.Game.Data.Entities;
using Game.GameMode.Session.Pools.EnemyBuilding;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.GameMode.Session.Game.Systems.Enemies
{
    public class EnemySpawner : ILoopedSystem
    {
        private EnemyPool _enemyPool;
        
        private int _sectionIndex;
        private float _sectionTime;
        private float _spawnCooldown;

        public EnemySpawner(EnemyPool enemyPool)
        {
            _enemyPool = enemyPool;
        }

        public UniTask Initialize(SessionRegistry sessionRegistry, CancellationToken cancellationToken)
        {
            _sectionIndex = 0;
            _sectionTime = 0;
            _spawnCooldown = 0;
            
            return UniTask.CompletedTask;
        }

        public async UniTask Update(float deltaTime, SessionRegistry sessionRegistry, CancellationToken cancellationToken)
        {
            SessionScenario scenario = sessionRegistry.SessionScenario;
            _sectionTime += deltaTime;
            if (_sectionTime >= scenario.WaveList[_sectionIndex].SegmentLength)
            {
                _sectionIndex++;
                _sectionIndex = Mathf.Clamp(_sectionIndex, 0, scenario.WaveList.Length);
                _spawnCooldown = 0;
                _sectionTime = 0;
            }
            
            _spawnCooldown -= deltaTime;
            if (_spawnCooldown > 0)
            {
                return;
            }

            ScenarioWave scenarioWave = scenario.WaveList[_sectionIndex];
            _spawnCooldown = scenarioWave.SpawnFrequency;

            for (int i = 0; i < scenarioWave.EnemyWave.SpawnCount; i++)
            {
                EnemyComponent enemyComponent = await _enemyPool.GetObject(cancellationToken);
                Vector3 spawnPoint = GetSpawnPoint(scenarioWave.SpawnType, sessionRegistry, scenarioWave.EnemyWave.SpawnCount, i);

                spawnPoint += sessionRegistry.PlayerCharacterComponent.Transform.position;
                
                enemyComponent.SpriteRenderer.sprite = scenarioWave.EnemyWave.Sprite;
                enemyComponent.Transform.position = spawnPoint;
                enemyComponent.Transform.SetParent(null);
                enemyComponent.gameObject.SetActive(true);

                SetEnemyStats(enemyComponent, scenarioWave.EnemyWave);
                sessionRegistry.Enemies.Add(enemyComponent);
            }
            
        }

        private Vector3 GetSpawnPoint(SpawnType spawnType, SessionRegistry sessionRegistry, int spawnCount, int instanceCount)
        {
            return spawnType switch {
                SpawnType.Random => GetRandomPoint(sessionRegistry, spawnCount, instanceCount),
                SpawnType.Square => GetSquarePoint(sessionRegistry, spawnCount, instanceCount),
                SpawnType.Ring => GetRingPoint(sessionRegistry, spawnCount, instanceCount),
                _ => throw new ArgumentOutOfRangeException(nameof(spawnType), spawnType, null)
            };
        }

        private Vector3 GetRandomPoint(SessionRegistry sessionRegistry, int spawnCount, int instanceCount)
        {
            Vector2 position = sessionRegistry.PlayerCameraComponent.CameraSize * Random.onUnitCircle * 1.2f;

            position.x *= sessionRegistry.PlayerCameraComponent.Camera.Lens.Aspect;

            return position;
        }
        
        // not perfect square, but when enemies will close up, I will get the result I wanted
        private Vector3 GetSquarePoint(SessionRegistry sessionRegistry, int spawnCount, int instanceCount)
        {
            float radius = sessionRegistry.PlayerCameraComponent.CameraSize * 1.5f;

            float radialDisplacement = Mathf.PI * 2 / spawnCount;

            float pointOnHorizontal = Mathf.Sin(radialDisplacement * instanceCount);
            float pointOnVertical = Mathf.Cos(radialDisplacement * instanceCount);

            if (Mathf.Abs(pointOnHorizontal) < Mathf.Abs(pointOnVertical))
            {
                return new Vector3(
                    radius * pointOnHorizontal * sessionRegistry.PlayerCameraComponent.Camera.Lens.Aspect,
                    radius * Mathf.Sign(pointOnVertical),
                    0);
            }

            return new Vector3(
                radius * sessionRegistry.PlayerCameraComponent.Camera.Lens.Aspect * Mathf.Sign(pointOnHorizontal),
                radius * pointOnVertical,
                0);
        }
        
        private Vector3 GetRingPoint(SessionRegistry sessionRegistry, int spawnCount, int instanceCount)
        {
            float radius = sessionRegistry.PlayerCameraComponent.CameraSize * 1.2f * sessionRegistry.PlayerCameraComponent.Camera.Lens.Aspect;

            float radialDisplacement = Mathf.PI * 2 / spawnCount;
            
            return new Vector3(
                Mathf.Sin(radialDisplacement * instanceCount) * radius,
                Mathf.Cos(radialDisplacement * instanceCount) * radius,
                0);
        }

        public void SetEnemyStats(EnemyComponent enemyComponent, EnemyWave wave)
        {
            enemyComponent.Radius = wave.Radius;
            enemyComponent.Hp = wave.Hp;
            enemyComponent.Speed = wave.Speed;
            enemyComponent.Dps = wave.Dps;
            enemyComponent.Exp = wave.ExpReward;

            enemyComponent.InPlayerRadius = false;
        }
        
        
    }
}