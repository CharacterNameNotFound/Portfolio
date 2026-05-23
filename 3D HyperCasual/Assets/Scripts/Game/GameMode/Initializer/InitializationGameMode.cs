using System;
using System.IO;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.MainHub.Controller;
using Game.Session;
using Game.UI.Tooltips;
using GameWideSystems.CameraManagement;
using GameWideSystems.GameStateManagement;
using UnityEngine;

namespace Game.GameMode.Initializer
{
    public class InitializationGameMode : IGameStateController
    {
        private GenericPathProvider _genericPathProvider;
        private GameStateManager _gameStateManager;
        private MainHubGameMode.Factory _logInGameModeFactory;
        private TextTooltipRegisterer _textTooltipRegisterer;
        private ICameraManager _cameraManager;

        public InitializationGameMode(
            GenericPathProvider genericPathProvider, 
            GameStateManager gameStateManager, 
            MainHubGameMode.Factory logInGameModeFactory, 
            TextTooltipRegisterer textTooltipRegisterer, 
            ICameraManager cameraManager)
        {
            _genericPathProvider = genericPathProvider;
            _gameStateManager = gameStateManager;
            _logInGameModeFactory = logInGameModeFactory;
            _textTooltipRegisterer = textTooltipRegisterer;
            _cameraManager = cameraManager;
        }

        public async UniTask<bool> Initialize(GameStateInitializationParameters parameters, CancellationToken cancellationToken)
        {
            _cameraManager.Initialize();
            
            return true;
        }

        public UniTask Start(GameStateStartParameters parameters, CancellationToken cancellationToken = default)
        {
            _textTooltipRegisterer.Register();
            
            return _gameStateManager.AppendGameState(_logInGameModeFactory.Create(), cancellationToken: cancellationToken);
        }

        public UniTask Unload(CancellationToken cancellationToken = default)
        {
            return UniTask.CompletedTask;
        }
        
        public UniTask Load(IGameStateSerializationData gameStateSerializationData, CancellationToken cancellationToken = default)
        {
            return _gameStateManager.AppendGameState(_logInGameModeFactory.Create(), cancellationToken: cancellationToken);
        }

        public UniTask Close(CancellationToken cancellationToken = default)
        {
            throw new Exception("Attempting to close game initializer");
        }
                      
        public UniTask<bool> TryGetSaveState(out IGameStateSerializationData gameStateSerializationData,
            CancellationToken cancellationToken = default)
        {
            gameStateSerializationData = null;
            return UniTask.FromResult<bool>(false);
        }
        
    }
}