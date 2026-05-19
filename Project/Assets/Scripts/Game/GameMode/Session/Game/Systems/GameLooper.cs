using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.Session.Game.Data;
using UnityEngine;

namespace Game.GameMode.Session.Game.Systems
{
    public class GameLooper : IGameLooper
    {
        private List<ILoopedSystem> _loopedSystems;
        private IGameEndDecider _gameEndDecider;
        private SessionRegistry _sessionRegistry;
        
        public GameLooper(List<ILoopedSystem> loopedSystems, IGameEndDecider gameEndDecider, SessionRegistry sessionRegistry)
        {
            _loopedSystems = loopedSystems;
            _gameEndDecider = gameEndDecider;
            _sessionRegistry = sessionRegistry;
        }

        public UniTask StartLoop(CancellationToken cancellationToken)
        {
            Loop(cancellationToken).Forget();

            return UniTask.CompletedTask;
        }

        private async UniTask Loop(CancellationToken cancellationToken)
        {
            do
            {
                float lastFrameTime = Time.deltaTime;

                foreach (ILoopedSystem system in _loopedSystems)
                {
                    system.Update(lastFrameTime, _sessionRegistry);
                }
                
                await UniTask.NextFrame();
                
            } while (!cancellationToken.IsCancellationRequested &&
                     !Application.exitCancellationToken.IsCancellationRequested &&
                     !_gameEndDecider.IsSessionFinished());
            
        }
        

    }
}