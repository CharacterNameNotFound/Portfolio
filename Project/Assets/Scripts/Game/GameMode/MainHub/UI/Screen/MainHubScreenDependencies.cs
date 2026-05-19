using Game.GameMode.Session.Controller;
using Game.Utilities.MusicControlling;
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
        public AudioArchive AudioArchive { get; }

        public SessionGameMode.Factory SessionFactory { get;}


        public MainHubScreenDependencies(
            InputControlFacade inputControlFacade, 
            GameStateManager gameStateManager, 
            ILoadingScreenManager loadingScreenManager, 
            AudioArchive audioArchive, 
            SessionGameMode.Factory sessionFactory)
        {
            InputControlFacade = inputControlFacade;
            GameStateManager = gameStateManager;
            LoadingScreenManager = loadingScreenManager;
            AudioArchive = audioArchive;
            SessionFactory = sessionFactory;
        }
    }
}