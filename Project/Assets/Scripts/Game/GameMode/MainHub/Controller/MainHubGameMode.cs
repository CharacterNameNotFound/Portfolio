using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.MainHub.UI.Screen;
using Game.Utilities.SceneDataProviding;
using GameWideSystems.GameSceneManagement;
using GameWideSystems.GameSceneManager;
using GameWideSystems.GameStateManagement;
using GameWideSystems.UIManagement;
using GameWideSystems.UIManagement.UIManagerRequests;
using UnityEngine.SceneManagement;
using Zenject;

namespace Game.GameMode.MainHub.Controller
{
    public class MainHubGameMode : IGameStateController
    {
        public class Factory : PlaceholderFactory<MainHubGameMode> { }
        
        
        private MainHubScreenBuilder _mainHubScreenBuilder;
        private UIManager _uiManager;
        private ISceneAddressableDataProvider _sceneAddressableDataProvider;
        private IGameSceneManager _gameSceneManager;
        private ILoadingScreenManager _loadingScreenManager;
        
        public MainHubGameMode(
            MainHubScreenBuilder mainHubScreenBuilder, 
            UIManager uiManager, 
            ISceneAddressableDataProvider sceneAddressableDataProvider, 
            IGameSceneManager gameSceneManager, 
            ILoadingScreenManager loadingScreenManager)
        {
            _mainHubScreenBuilder = mainHubScreenBuilder;
            _uiManager = uiManager;
            _sceneAddressableDataProvider = sceneAddressableDataProvider;
            _gameSceneManager = gameSceneManager;
            _loadingScreenManager = loadingScreenManager;
        }
        
        public UniTask<bool> Initialize(GameStateInitializationParameters parameters, CancellationToken cancellationToken)
        {
            return UniTask.FromResult(true);
        }

        public async UniTask Start(GameStateStartParameters parameters, CancellationToken cancellationToken = default)
        {
            await _gameSceneManager.OpenScene(_sceneAddressableDataProvider.MainScene, LoadSceneMode.Single,
                new LoadingScreenParams(false, _loadingScreenManager), cancellationToken: cancellationToken);
            await _uiManager.OpenScreenRequest(_mainHubScreenBuilder, null, out _).Play(cancellationToken);
            await _loadingScreenManager.Hide(true, cancellationToken);
        }

        public async UniTask Load(IGameStateSerializationData gameStateSerializationData, CancellationToken cancellationToken = default)
        {
            await _gameSceneManager.OpenScene(_sceneAddressableDataProvider.MainScene, LoadSceneMode.Single,
                new LoadingScreenParams(false, _loadingScreenManager), cancellationToken: cancellationToken);
            await _uiManager.OpenScreenRequest(_mainHubScreenBuilder, null, out _).Play(cancellationToken);
            await _loadingScreenManager.Hide(true, cancellationToken);
        }
        
        public UniTask Unload(CancellationToken cancellationToken = default)
        {
            return Close(cancellationToken);
        }

        public UniTask Close(CancellationToken cancellationToken = default)
        {
            return _uiManager.CloseTopRequest().Play(cancellationToken);
        }

        public UniTask<bool> TryGetSaveState(out IGameStateSerializationData gameStateSerializationData,
            CancellationToken cancellationToken = default)
        {
            gameStateSerializationData = null;
            return UniTask.FromResult(true);
        }
    }
}