using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.Session.Game.Data;
using Game.GameMode.Session.Game.Data.Entities;
using UnityEngine;

namespace Game.GameMode.Session.Game.Systems.Enemies
{
    public class EnemyMover : ILoopedSystem
    {
        public UniTask Initialize(SessionRegistry sessionRegistry, CancellationToken cancellationToken)
        {
            return UniTask.CompletedTask;
        }

        public UniTask Update(float deltaTime, SessionRegistry sessionRegistry, CancellationToken cancellationToken)
        {
            Vector3 playerPosition = sessionRegistry.PlayerCharacterComponent.Transform.position;

            foreach (EnemyComponent enemy in sessionRegistry.Enemies)
            {
                Vector3 enemyToPlayerRadius = playerPosition - enemy.Transform.position;

                Vector3 fullSpeedDisplacement = enemyToPlayerRadius.normalized * enemy.Speed * deltaTime;


                enemy.Transform.position += 
                    fullSpeedDisplacement.sqrMagnitude < enemyToPlayerRadius.sqrMagnitude
                    ? fullSpeedDisplacement
                    : enemyToPlayerRadius;
                

                float squareRadiusSum = enemy.Radius + sessionRegistry.PlayerCharacterComponent.Radius;
                squareRadiusSum *= squareRadiusSum;

                enemy.SquareDistanceToPlayer = enemyToPlayerRadius.sqrMagnitude;
                enemy.InPlayerRadius = enemy.SquareDistanceToPlayer < squareRadiusSum;
            }
            
            return UniTask.CompletedTask;
        }
    }
}