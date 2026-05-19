using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.Session.Controller.GameInitialization;
using Game.GameMode.Session.Game.Systems;
using Game.GameMode.Session.Inputs;
using Game.GameMode.Session.UI;
using Game.Utilities.SceneDataProviding;
using GameWideSystems.GameSceneManagement;
using GameWideSystems.GameSceneManager;
using GameWideSystems.GameStateManagement;
using GameWideSystems.UIManagement;
using GameWideSystems.UIManagement.UIManagerRequests;
using UnityEngine.SceneManagement;
using Zenject;

namespace Game.GameMode.Session.Controller
{
    public class SessionGameMode : IGameStateController
    {
        public class Factory : PlaceholderFactory<SessionGameMode> { }
        
        private SessionScreenBuilder _sessionScreenBuilder;
        private UIManager _uiManager;
        private ISceneAddressableDataProvider _sceneAddressableDataProvider;
        private IGameSceneManager _gameSceneManager;
        private ILoadingScreenManager _loadingScreenManager;
        private ScreenJoystickInputLayer _screenJoystickInputLayer;
        private ISessionInitializer _sessionInitializer;
        private IGameLooper _gameLooper;

        private CancellationTokenSource _cancellationTokenSource;
        
        public SessionGameMode(
            UIManager uiManager, 
            ISceneAddressableDataProvider sceneAddressableDataProvider, 
            IGameSceneManager gameSceneManager, 
            ILoadingScreenManager loadingScreenManager, 
            SessionScreenBuilder sessionScreenBuilder, 
            ScreenJoystickInputLayer screenJoystickInputLayer, 
            ISessionInitializer sessionInitializer, 
            IGameLooper gameLooper)
        {
            _uiManager = uiManager;
            _sceneAddressableDataProvider = sceneAddressableDataProvider;
            _gameSceneManager = gameSceneManager;
            _loadingScreenManager = loadingScreenManager;
            _sessionScreenBuilder = sessionScreenBuilder;
            _screenJoystickInputLayer = screenJoystickInputLayer;
            _sessionInitializer = sessionInitializer;
            _gameLooper = gameLooper;
        }
        
        
        public async UniTask<bool> Initialize(GameStateInitializationParameters parameters, CancellationToken cancellationToken = default)
        {
            _cancellationTokenSource = new CancellationTokenSource();
            
            await _gameSceneManager.OpenScene(_sceneAddressableDataProvider.SessionScene, LoadSceneMode.Single,
                new LoadingScreenParams(false, _loadingScreenManager), cancellationToken: cancellationToken);
            await _sessionInitializer.InitializeSession(((SessionInitializationParameters) parameters).WorldGenerationConfigs, cancellationToken);
            return true;
        }

        public async UniTask Start(GameStateStartParameters parameters, CancellationToken cancellationToken = default)
        {
            await _uiManager.OpenScreenRequest(_sessionScreenBuilder, null, out _).Play(cancellationToken);
            await _loadingScreenManager.Hide(true, cancellationToken);

            await _gameLooper.StartLoop(_cancellationTokenSource.Token);
            
            _screenJoystickInputLayer.SetActive(true);
        }

        public UniTask Load(IGameStateSerializationData gameStateSerializationData, CancellationToken cancellationToken = default)
        {
            return UniTask.CompletedTask;
        }

        public UniTask Unload(CancellationToken cancellationToken = default)
        {
            return Close(cancellationToken);
        }

        public UniTask Close(CancellationToken cancellationToken = default)
        {
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource.Dispose();
            
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