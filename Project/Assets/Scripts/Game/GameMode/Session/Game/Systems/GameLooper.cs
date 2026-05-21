using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.Session.Game.Data;
using Game.GameMode.Session.Game.Items;
using Game.GameMode.Session.UI;
using GameWideSystems.UIManagement;
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

        public async UniTask StartLoop(ScreenHolder sessionScreen, CancellationToken cancellationToken)
        {
            foreach (ILoopedSystem system in _loopedSystems)
            {
                await system.Initialize(_sessionRegistry, cancellationToken);
            }

            Loop(sessionScreen, cancellationToken).Forget();
        }

        private async UniTask Loop(ScreenHolder sessionScreen, CancellationToken cancellationToken)
        {
            do
            {
                float lastFrameTime = Time.fixedDeltaTime;

                foreach (ILoopedSystem system in _loopedSystems)
                {
                    await system.Update(lastFrameTime, _sessionRegistry, cancellationToken);
                }
                
                await UniTask.NextFrame(PlayerLoopTiming.FixedUpdate, cancellationToken);
                
            } while (!cancellationToken.IsCancellationRequested &&
                     !Application.exitCancellationToken.IsCancellationRequested &&
                     !_gameEndDecider.IsSessionFinished(_sessionRegistry));

            Application.exitCancellationToken.ThrowIfCancellationRequested();
            
            ((SessionScreenController) sessionScreen.ScreenBase).ShowLoseScreen();
        }
        

    }
}