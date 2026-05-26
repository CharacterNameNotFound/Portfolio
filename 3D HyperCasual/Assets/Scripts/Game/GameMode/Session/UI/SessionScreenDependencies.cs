using Game.GameMode.Session.Gameplay.GameplayLoop;
using GameWideSystems.GameStateManagement;
using GameWideSystems.UIManagement.Screen;

namespace Game.GameMode.Session.UI
{
    public class SessionScreenDependencies : IUIScreenDependencies
    {
        public SessionRegistry SessionRegistry;
        public GameStateManager GameStateManager;

        public SessionScreenDependencies(SessionRegistry sessionRegistry, GameStateManager gameStateManager)
        {
            SessionRegistry = sessionRegistry;
            GameStateManager = gameStateManager;
        }
    }
}