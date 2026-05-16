using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using GameWideSystems.InputManager;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;
using Utils.UtilityTypes.AssetReferencing;

namespace GameWideSystems.GameSceneManager
{
    public class GameSceneManager : IGameSceneManager
    {
        private InputControlFacade _inputControlFacade;

        public GameSceneManager(InputControlFacade inputControlFacade)
        {
            _inputControlFacade = inputControlFacade;
        }

        public async UniTask<SceneInstance> OpenScene(AssetReference sceneAddressableKy,
            LoadSceneMode loadSceneMode,
            LoadingScreenParams loadingScreenParams = null,
            bool lockScreen = false,
            CancellationToken cancellationToken = default)
        {
            if (lockScreen)
            {
                _inputControlFacade.SetInputsAvailable(false);
            }

            SceneInstance scene;
            
            if (loadingScreenParams is not null)
            {
                scene = await LoadingWithProgressReporting(sceneAddressableKy, loadSceneMode, loadingScreenParams, cancellationToken);
            }
            else
            {
                scene = await LoadSceneInternal(sceneAddressableKy, loadSceneMode, cancellationToken);
            }
            
            if (lockScreen)
            {
                _inputControlFacade.SetInputsAvailable(true);
            }

            return scene;
        }

        public async UniTask UnloadScene(AssetReference sceneReference, CancellationToken cancellationToken = default)
        {
            await Addressables.UnloadSceneAsync(sceneReference.OperationHandle).ToUniTask(cancellationToken: cancellationToken);
        }

        private async UniTask<SceneInstance> LoadingWithProgressReporting(AssetReference sceneAssetReference, LoadSceneMode loadSceneMode, LoadingScreenParams loadingScreenParams, CancellationToken cancellationToken = default)
        {
            await loadingScreenParams.LoadingScreenManager.Show(cancellationToken);

            SceneInstance scene = await LoadSceneInternal(sceneAssetReference, loadSceneMode, cancellationToken);

            if (loadingScreenParams.IsLoadingScreenClosedAutomatically)
            {
                await loadingScreenParams.LoadingScreenManager.Hide(true, cancellationToken);
            }

            return scene;
        }

        private UniTask<SceneInstance> LoadSceneInternal(AssetReference sceneAssetReference, LoadSceneMode loadSceneMode, CancellationToken cancellationToken = default)
        {
            return sceneAssetReference.LoadScene(loadSceneMode, cancellationToken: cancellationToken);
        }
        
    }
}