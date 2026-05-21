using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;
using Utils.UtilityTypes.AssetReferencing;

namespace GameWideSystems.GameSceneManager
{
    public interface IGameSceneManager
    {
        public UniTask<SceneInstance> OpenScene(AssetReference sceneAddressableKy, LoadSceneMode loadSceneMode,
            LoadingScreenParams loadingScreenParams = null, bool lockScreen = false,
            CancellationToken cancellationToken = default);
        public UniTask UnloadScene(AssetReference sceneReference, CancellationToken cancellationToken = default);
    }
}