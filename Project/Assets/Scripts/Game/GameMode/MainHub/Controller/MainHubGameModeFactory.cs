using Game.GameMode.MainHub.UI.Screen;
using Game.GameMode.Session.WorldGeneration;
using Game.Utilities.SceneDataProviding;
using GameWideSystems.GameSceneManagement;
using GameWideSystems.GameSceneManager;
using GameWideSystems.UIManagement;
using Zenject;

namespace Game.GameMode.MainHub.Controller
{
    public class MainHubGameModeFactory : IFactory<MainHubGameMode>
    {
        private MainHubScreenBuilder _mainHubScreenBuilder;
        private UIManager _uiManager;
        private ISceneAddressableDataProvider _sceneAddressableDataProvider;
        private IGameSceneManager _gameSceneManager;
        private ILoadingScreenManager _loadingScreenManager;
        private WorldGenerationConfigs _worldGenerationConfigs;


        public MainHubGameModeFactory(MainHubScreenBuilder mainHubScreenBuilder, 
            UIManager uiManager, 
            ISceneAddressableDataProvider sceneAddressableDataProvider, 
            IGameSceneManager gameSceneManager, 
            ILoadingScreenManager loadingScreenManager, 
            WorldGenerationConfigs worldGenerationConfigs)
        {
            _mainHubScreenBuilder = mainHubScreenBuilder;
            _uiManager = uiManager;
            _sceneAddressableDataProvider = sceneAddressableDataProvider;
            _gameSceneManager = gameSceneManager;
            _loadingScreenManager = loadingScreenManager;
            _worldGenerationConfigs = worldGenerationConfigs;
        }

        public MainHubGameMode Create()
        {
            return new MainHubGameMode(_mainHubScreenBuilder, _uiManager, _sceneAddressableDataProvider, _gameSceneManager, _loadingScreenManager, _worldGenerationConfigs);
        }
    }
}