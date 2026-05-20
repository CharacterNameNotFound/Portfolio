using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.Session.Game.Data;
using Game.GameMode.Session.Game.Data.Entities;
using Game.GameMode.Session.Game.Utilities;

namespace Game.GameMode.Session.Game.Systems.Player
{
    public class PlayerLevelUp : ILoopedSystem
    {
        private SessionScreenHolder _sessionScreenHolder;

        public PlayerLevelUp(SessionScreenHolder sessionScreenHolder)
        {
            _sessionScreenHolder = sessionScreenHolder;
        }

        public UniTask Initialize(SessionRegistry sessionRegistry, CancellationToken cancellationToken)
        {
            _sessionScreenHolder.SessionScreenController.SetExp(0);

            PlayerStats playerStats = sessionRegistry.PlayerStats;
            playerStats.CurrentExp = 0;
            playerStats.Level = 1;

            return UniTask.CompletedTask;
        }

        public UniTask Update(float deltaTime, SessionRegistry sessionRegistry, CancellationToken cancellationToken)
        {
            PlayerStats playerStats = sessionRegistry.PlayerStats;
            float barValue = playerStats.CurrentExp / (playerStats.RequiredExpPerLevel * playerStats.Level); 
            
            _sessionScreenHolder.SessionScreenController.SetExp(barValue);

            return UniTask.CompletedTask;
        }
    }
}