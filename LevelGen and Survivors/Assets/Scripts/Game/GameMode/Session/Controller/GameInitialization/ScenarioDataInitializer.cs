using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.Session.Game.Data;
using GameWideSystems.AudioManager;
using GameWideSystems.ScriptedVisualEffectManagement;
using UnityEngine;

namespace Game.GameMode.Session.Controller.GameInitialization
{
    public class ScenarioDataInitializer : IScenarioDataInitializer
    {
        private SessionScenario _sessionScenario;
        private SessionRegistry _sessionRegistry;
        private AudioManager _audioManager;
        private IScriptedVisualEffectManager _scriptedVisualEffectManager;

        public ScenarioDataInitializer(SessionScenario sessionScenario, SessionRegistry sessionRegistry, AudioManager audioManager, IScriptedVisualEffectManager scriptedVisualEffectManager)
        {
            _sessionScenario = sessionScenario;
            _sessionRegistry = sessionRegistry;
            _audioManager = audioManager;
            _scriptedVisualEffectManager = scriptedVisualEffectManager;
        }

        public UniTask InitializeScenarioData(CancellationToken cancellationToken)
        {
            _sessionRegistry.SessionScenario = _sessionScenario;
            _sessionRegistry.AudioManager = _audioManager;
            _sessionRegistry.ScriptedVisualEffectManager = _scriptedVisualEffectManager;

            return UniTask.CompletedTask;
        }
        
    }
}