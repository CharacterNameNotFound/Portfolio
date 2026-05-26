using Game.GameMode.Session.Gameplay.GameplayLoop;
using Game.GameMode.Session.UI;
using Game.Utilities.MusicControlling;
using Game.Utilities.SceneDataProviding;
using GameWideSystems.GameSceneManagement;
using GameWideSystems.GameSceneManager;
using GameWideSystems.UIManagement;
using Zenject;

namespace Game.GameMode.Session.Controller
{
    public class SessionGameModeFactory : IFactory<SessionGameMode>
    {
        private SessionScreenBuilder _sessionScreenBuilder;
        private UIManager _uiManager;
        private ISceneAddressableDataProvider _sceneAddressableDataProvider;
        private IGameSceneManager _gameSceneManager;
        private ILoadingScreenManager _loadingScreenManager;
        private AudioArchive _audioArchive;
        private SessionRegistry _sessionRegistry;
        private IGameLoop _gameLoop;
        private ISessionInitializer _sessionInitializer;

        public SessionGameModeFactory(
            SessionScreenBuilder sessionScreenBuilder, 
            UIManager uiManager, 
            ISceneAddressableDataProvider sceneAddressableDataProvider, 
            IGameSceneManager gameSceneManager, 
            ILoadingScreenManager loadingScreenManager, 
            AudioArchive audioArchive, 
            SessionRegistry sessionRegistry, 
            IGameLoop gameLoop, 
            ISessionInitializer sessionInitializer)
        {
            _sessionScreenBuilder = sessionScreenBuilder;
            _uiManager = uiManager;
            _sceneAddressableDataProvider = sceneAddressableDataProvider;
            _gameSceneManager = gameSceneManager;
            _loadingScreenManager = loadingScreenManager;
            _audioArchive = audioArchive;
            _sessionRegistry = sessionRegistry;
            _gameLoop = gameLoop;
            _sessionInitializer = sessionInitializer;
        }


        public SessionGameMode Create()
        {
            return new SessionGameMode(
                _sessionScreenBuilder, 
                _uiManager, 
                _sceneAddressableDataProvider, 
                _gameSceneManager, 
                _loadingScreenManager, 
                _audioArchive, 
                _sessionRegistry, 
                _gameLoop,
                _sessionInitializer);
            
            
        }
    }
}