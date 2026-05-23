
using GameWideSystems.GameSceneManagement;
using GameWideSystems.GameStateManagement;
using GameWideSystems.InputManager;
using GameWideSystems.UIManagement.Screen;

namespace Game.GameMode.MainHub.UI.Screen
{
    public class MainHubScreenDependencies : IUIScreenDependencies
    {
        public InputControlFacade InputControlFacade { get; }
        public GameStateManager GameStateManager { get;}
        public ILoadingScreenManager LoadingScreenManager { get; }


        public MainHubScreenDependencies(
            InputControlFacade inputControlFacade, 
            GameStateManager gameStateManager, 
            ILoadingScreenManager loadingScreenManager)
        {
            InputControlFacade = inputControlFacade;
            GameStateManager = gameStateManager;
            LoadingScreenManager = loadingScreenManager;
        }
    }
}