using Game.GameMode.Session.Controller.GameInitialization;
using Game.GameMode.Session.Game.Systems;
using Game.GameMode.Session.Game.Utilities;
using Game.GameMode.Session.Inputs;
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
        private ScreenJoystickInputLayer _screenJoystickInputLayer;
        private ISessionInitializer _sessionInitializer;
        private IGameLooper _gameLooper;
        private AudioArchive _audioArchive;
        private SessionScreenHolder _sessionScreenHolder;


        public SessionGameModeFactory(
            SessionScreenBuilder sessionScreenBuilder, 
            UIManager uiManager, 
            ISceneAddressableDataProvider sceneAddressableDataProvider, 
            IGameSceneManager gameSceneManager, 
            ILoadingScreenManager loadingScreenManager, 
            ScreenJoystickInputLayer screenJoystickInputLayer, 
            ISessionInitializer sessionInitializer, 
            IGameLooper gameLooper, 
            AudioArchive audioArchive, 
            SessionScreenHolder sessionScreenHolder)
        {
            _sessionScreenBuilder = sessionScreenBuilder;
            _uiManager = uiManager;
            _sceneAddressableDataProvider = sceneAddressableDataProvider;
            _gameSceneManager = gameSceneManager;
            _loadingScreenManager = loadingScreenManager;
            _screenJoystickInputLayer = screenJoystickInputLayer;
            _sessionInitializer = sessionInitializer;
            _gameLooper = gameLooper;
            _audioArchive = audioArchive;
            _sessionScreenHolder = sessionScreenHolder;
        }

        public SessionGameMode Create()
        {
            return new SessionGameMode(
                _uiManager, 
                _sceneAddressableDataProvider, 
                _gameSceneManager, 
                _loadingScreenManager, 
                _sessionScreenBuilder, 
                _screenJoystickInputLayer,
                _sessionInitializer,
                _gameLooper,
                _audioArchive,
                _sessionScreenHolder);
            
        }
    }
}