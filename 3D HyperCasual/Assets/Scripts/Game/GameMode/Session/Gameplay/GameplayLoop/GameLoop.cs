using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game.GameMode.Session.Gameplay.GameplayLoop
{
    public class GameLoop : IGameLoop
    {
        private ILoopedSystem[] _loopedSystems;
        private SessionRegistry _sessionRegistry;
        private IGameFinishConditionChecker _gameFinishConditionChecker;

        public GameLoop(ILoopedSystem[] loopedSystems, SessionRegistry sessionRegistry, IGameFinishConditionChecker gameFinishConditionChecker)
        {
            _loopedSystems = loopedSystems;
            _sessionRegistry = sessionRegistry;
            _gameFinishConditionChecker = gameFinishConditionChecker;
        }
        
        public async UniTask StartLoop(CancellationToken cancellationToken)
        {
            foreach (ILoopedSystem system in _loopedSystems)
            {
                await system.Initialize(_sessionRegistry, cancellationToken);
            }
            
            Loop(cancellationToken).Forget();
        }

        private async UniTask Loop(CancellationToken cancellationToken)
        {
            float deltaTime = Time.fixedDeltaTime;

            do
            {
                foreach (ILoopedSystem system in _loopedSystems)
                {
                    await system.Update(deltaTime, _sessionRegistry, cancellationToken);
                }
                
                await UniTask.NextFrame(PlayerLoopTiming.FixedUpdate, cancellationToken: cancellationToken);

            } while (!cancellationToken.IsCancellationRequested &&
                     !Application.exitCancellationToken.IsCancellationRequested && 
                     !_gameFinishConditionChecker.IsGameFinished(_sessionRegistry));
            
            foreach (ILoopedSystem system in _loopedSystems)
            {
                await system.CleanUp(_sessionRegistry, cancellationToken);
            }

            Time.timeScale = 0;
            _sessionRegistry.SessionScreen.LoseScreen.SetActive(true);
            
        }
        
        
    }
}