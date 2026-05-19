using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.Session.Game.Data;
using UnityEngine;

namespace Game.GameMode.Session.Controller.GameInitialization
{
    public class ScenarioDataInitializer : IScenarioDataInitializer
    {
        private SessionScenario _sessionScenario;
        private SessionRegistry _sessionRegistry;

        public ScenarioDataInitializer(SessionScenario sessionScenario, SessionRegistry sessionRegistry)
        {
            _sessionScenario = sessionScenario;
            _sessionRegistry = sessionRegistry;
        }

        public UniTask InitializeScenarioData(CancellationToken cancellationToken)
        {
            _sessionRegistry.SessionScenario = _sessionScenario;

            return UniTask.CompletedTask;
        }
        
    }
}