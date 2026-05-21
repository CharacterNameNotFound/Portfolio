using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.Session.Game.Data;
using GameWideSystems.AudioManager;
using UnityEngine;

namespace Game.GameMode.Session.Controller.GameInitialization
{
    public class ScenarioDataInitializer : IScenarioDataInitializer
    {
        private SessionScenario _sessionScenario;
        private SessionRegistry _sessionRegistry;
        private AudioManager _audioManager;

        public ScenarioDataInitializer(SessionScenario sessionScenario, SessionRegistry sessionRegistry, AudioManager audioManager)
        {
            _sessionScenario = sessionScenario;
            _sessionRegistry = sessionRegistry;
            _audioManager = audioManager;
        }

        public UniTask InitializeScenarioData(CancellationToken cancellationToken)
        {
            _sessionRegistry.SessionScenario = _sessionScenario;
            _sessionRegistry.AudioManager = _audioManager;

            return UniTask.CompletedTask;
        }
        
    }
}