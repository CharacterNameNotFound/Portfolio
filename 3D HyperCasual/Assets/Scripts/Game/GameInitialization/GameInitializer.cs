using System.Collections;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.Initializer;
using Game.Utilities.MusicControlling;
using GameWideSystems.AudioManager;
using GameWideSystems.GameSceneManagement;
using GameWideSystems.GameStateManagement;
using GameWideSystems.LocalizationWrapper;
using UnityEngine;
using Zenject;

namespace Game.GameInitialization
{
    public class GameInitializer : MonoBehaviour
    {
        private AudioManager _audioManager;
        private InitializationGameMode _initializationGameMode;
        private GameStateManager _gameStateManager;
        private ILocalizationManager _localizationManager;
        private ILoadingScreenManager _loadingScreenManager;
        private AudioArchive _audioArchive;


        [Inject]
        private void Construct(
            AudioManager audioManager,
            InitializationGameMode initializationGameMode,
            GameStateManager gameStateManager,
            ILocalizationManager localizationManager,
            ILoadingScreenManager loadingScreenManager,
            AudioArchive audioArchive)
        {
            _audioManager = audioManager;
            _initializationGameMode = initializationGameMode;
            _gameStateManager = gameStateManager;
            _localizationManager = localizationManager;
            _loadingScreenManager = loadingScreenManager;
            _audioArchive = audioArchive;
        }
        
        private IEnumerator Start()
        {
            yield return Initialize(Application.exitCancellationToken);
        }
        
        private async UniTask Initialize(CancellationToken cancellationToken)
        {
            Transform proceduralHolderTransform = FindAnyObjectByType<ProjectContext>().transform;

            await _loadingScreenManager.Show(cancellationToken);
            await _audioManager.Initialize(proceduralHolderTransform, cancellationToken);
            await _audioArchive.Initialize(cancellationToken);

            await _audioArchive.PlayMusic(MusicGroup.Menu, cancellationToken);
            //await _localizationManager.Initialize();

            await _gameStateManager.AppendGameState(_initializationGameMode, cancellationToken: cancellationToken);
        }
        
    }
}