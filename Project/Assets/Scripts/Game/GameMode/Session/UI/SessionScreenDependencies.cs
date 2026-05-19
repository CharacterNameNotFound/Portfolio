using Game.GameMode.Session.Controller;
using Game.GameMode.Session.Controller.GameInitialization;
using GameWideSystems.GameStateManagement;
using GameWideSystems.UIManagement.Screen;

namespace Game.GameMode.Session.UI
{
    public class SessionScreenDependencies : IUIScreenDependencies
    {
        public ISessionInitializer SessionInitializer;
        public GameStateManager GameStateManager;

        public SessionScreenDependencies(ISessionInitializer sessionInitializer, GameStateManager gameStateManager)
        {
            SessionInitializer = sessionInitializer;
            GameStateManager = gameStateManager;
        }
    }
}