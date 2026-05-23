using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.Session.UI;
using Game.Utilities.MusicControlling;
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
        private AudioArchive _audioArchive;
        
        public SessionGameMode(
            SessionScreenBuilder sessionScreenBuilder, 
            UIManager uiManager, 
            ISceneAddressableDataProvider sceneAddressableDataProvider, 
            IGameSceneManager gameSceneManager, 
            ILoadingScreenManager loadingScreenManager)
        {
            _sessionScreenBuilder = sessionScreenBuilder;
            _uiManager = uiManager;
            _sceneAddressableDataProvider = sceneAddressableDataProvider;
            _gameSceneManager = gameSceneManager;
            _loadingScreenManager = loadingScreenManager;
        }
        
        
        public UniTask<bool> Initialize(GameStateInitializationParameters parameters, CancellationToken cancellationToken = default)
        {
            return UniTask.FromResult(true);
        }

        public async UniTask Start(GameStateStartParameters parameters, CancellationToken cancellationToken = default)
        {
            _audioArchive.PlayMusic(MusicGroup.Session, cancellationToken).Forget();
            await _gameSceneManager.OpenScene(_sceneAddressableDataProvider.MainScene, LoadSceneMode.Single,
                new LoadingScreenParams(false, _loadingScreenManager), cancellationToken: cancellationToken);
            await _uiManager.OpenScreenRequest(_sessionScreenBuilder, null, out _).Play(cancellationToken);
            await _loadingScreenManager.Hide(true, cancellationToken);
        }

        public UniTask Load(IGameStateSerializationData gameStateSerializationData, CancellationToken cancellationToken = default)
        {
            return UniTask.FromResult(true);
        }

        public UniTask Unload(CancellationToken cancellationToken = default)
        {
            return UniTask.FromResult(true);
        }

        public UniTask Close(CancellationToken cancellationToken = default)
        {
            return UniTask.FromResult(true);
        }

        public UniTask<bool> TryGetSaveState(out IGameStateSerializationData gameStateSerializationData,
            CancellationToken cancellationToken = default)
        {
            gameStateSerializationData = null;
            return UniTask.FromResult(true);
        }
    }
}