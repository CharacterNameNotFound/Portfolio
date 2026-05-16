using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using GameWideSystems.InputManager;

namespace GameWideSystems.GameStateManagement
{
    public class GameStateManager
    {
        private class GameStateToken
        {
            public IGameStateController GameStateController;
            public IGameStateSerializationData GameStateSerializationData;

            public GameStateToken(IGameStateController gameStateController)
            {
                GameStateController = gameStateController;
            }
        }
        
        private InputControlFacade _inputControlFacade;
        private readonly Stack<GameStateToken> _gameStates = new();

        public GameStateManager(InputControlFacade inputControlFacade)
        {
            _inputControlFacade = inputControlFacade;
        }

        public async UniTask SwapTopState(
            IGameStateController gameStateController, 
            bool lockInput = true, 
            GameStateInitializationParameters initializationParameters = null, 
            GameStateStartParameters startParameters = null, 
            CancellationToken cancellationToken = default)
        {
            if (lockInput)
            {
                _inputControlFacade.SetInputsAvailable(false);
            }
            
            await CloseInternal(lockInput, cancellationToken);
            await OpenInternal(gameStateController, lockInput, initializationParameters, startParameters, cancellationToken);
            
            if (lockInput)
            {
                _inputControlFacade.SetInputsAvailable(true);
            }
        }
        
        public async UniTask AppendGameState(
            IGameStateController gameStateController, 
            bool lockInput = true, 
            GameStateInitializationParameters initializationParameters = null, 
            GameStateStartParameters startParameters = null, 
            CancellationToken cancellationToken = default)
        {
            if (lockInput)
            {
                _inputControlFacade.SetInputsAvailable(false);
            }
            
            await OpenInternal(gameStateController, lockInput, initializationParameters, startParameters, cancellationToken);
            
            if (lockInput)
            {
                _inputControlFacade.SetInputsAvailable(true);
            }
        }

        public async UniTask CloseCurrentGameState(bool reloadPrevious, bool lockInput = true, CancellationToken cancellationToken = default)
        {
            if (lockInput)
            {
                _inputControlFacade.SetInputsAvailable(false);
            }
            
            await CloseInternal(lockInput, cancellationToken);

            if (!reloadPrevious)
            {
                if (lockInput)
                {
                    _inputControlFacade.SetInputsAvailable(true);
                }
                return;
            }
            
            await _gameStates.Peek()
                .GameStateController
                .Load(_gameStates.Peek().GameStateSerializationData, cancellationToken);
            
            if (lockInput)
            {
                _inputControlFacade.SetInputsAvailable(true);
            }
            
        }

        private async UniTask OpenInternal(
            IGameStateController gameStateController, 
            bool lockInput = true, 
            GameStateInitializationParameters initializationParameters = null, 
            GameStateStartParameters startParameters = null, 
            CancellationToken cancellationToken = default)
        {
            if (lockInput)
            {
                _inputControlFacade.SetInputsAvailable(false);
            }
            
            if (_gameStates.TryPeek(out GameStateToken gameStateToken))
            {
                bool isSaved = await gameStateToken.GameStateController.TryGetSaveState(out IGameStateSerializationData serializationData, cancellationToken);
                if (isSaved)
                {
                    gameStateToken.GameStateSerializationData = serializationData;
                }
                
                await gameStateToken.GameStateController.Unload(cancellationToken);
            }

            _gameStates.Push(new GameStateToken(gameStateController));
            bool isInitializedSuccessful = await gameStateController.Initialize(initializationParameters, cancellationToken);

            if (!isInitializedSuccessful)
            {
                if (lockInput)
                {
                    _inputControlFacade.SetInputsAvailable(true);
                }
                
                return;
            }
            
            await gameStateController.Start(startParameters, cancellationToken);
            
            if (lockInput)
            {
                _inputControlFacade.SetInputsAvailable(true);
            }
        }

        private async UniTask CloseInternal(bool lockInput = true, CancellationToken cancellationToken = default)
        {
            if (lockInput)
            {
                _inputControlFacade.SetInputsAvailable(false);
            }
            
            // The Last state will "restart" game, so it should always be present
            if (_gameStates.Count == 1)
            {
                return;
            }
            
            await _gameStates.Pop()
                .GameStateController
                .Close(cancellationToken);
            
            if (lockInput)
            {
                _inputControlFacade.SetInputsAvailable(true);
            }
        }
        
    }
}