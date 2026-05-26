using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.Session.Gameplay.Entities;
using Game.GameMode.Session.Gameplay.GameplayLoop.Systems.Configs;
using Game.GameMode.Session.Gameplay.Pools.CubePooling;
using UnityEngine;

namespace Game.GameMode.Session.Gameplay.GameplayLoop.Systems
{
    public class CubeMovementSystem : ILoopedSystem
    {
        private GameCubeSystemsConfigs _configs;
        private GameCubePool _cubePool;

        public CubeMovementSystem(GameCubeSystemsConfigs configs, GameCubePool cubePool)
        {
            _configs = configs;
            _cubePool = cubePool;
        }

        public UniTask Initialize(SessionRegistry sessionRegistry, CancellationToken cancellationToken)
        {
            return UniTask.CompletedTask;
        }

        public UniTask Update(float delta, SessionRegistry sessionRegistry, CancellationToken cancellationToken)
        {
            for (int i = 0; i < sessionRegistry.ActiveCubes.Count; i++)
            {
                GameCubeComponent cube = sessionRegistry.ActiveCubes[i];

                cube.Transform.position += new Vector3(0, 0, -delta * cube.Speed);
                
                if (cube.Transform.position.z > _configs.DespawnZ)
                {
                    continue;
                }
                
                _cubePool.ReturnToPool(cube);
                sessionRegistry.Lives--;
                sessionRegistry.ActiveCubes.RemoveAt(i);
                i--;
            }
            
            return UniTask.CompletedTask;
        }

        public UniTask CleanUp(SessionRegistry sessionRegistry, CancellationToken cancellationToken)
        {
            return UniTask.CompletedTask;
        }
        
    }
}