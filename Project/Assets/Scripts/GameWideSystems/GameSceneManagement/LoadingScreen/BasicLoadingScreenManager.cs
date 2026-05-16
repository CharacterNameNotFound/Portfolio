using System.Threading;
using Cysharp.Threading.Tasks;
using GameWideSystems.CameraManagement;
using GameWideSystems.GameSceneManagement;
using GameWideSystems.UIManagement;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;
using Utils.UtilityTypes.AssetReferencing;

namespace GameWideSystems.GameSceneManager.LoadingScreen
{
    public class BasicLoadingScreenManager : ILoadingScreenManager
    {
        private IBasicLoadingScreenDataProvider _basicLoadingScreenDataProvider;
        private IScreenHostProvider _screenHostProvider;
        private ICameraManager _cameraManager;
        private Logger.Logger _logger;

        private bool _inUse;

        private GameObject _loadingScreen;
        public ILoadingScreen LoadingScreen { get; private set; }

        public BasicLoadingScreenManager(IBasicLoadingScreenDataProvider basicLoadingScreenDataProvider, IScreenHostProvider screenHostProvider, Logger.Logger logger, ICameraManager cameraManager)
        {
            _basicLoadingScreenDataProvider = basicLoadingScreenDataProvider;
            _screenHostProvider = screenHostProvider;
            _logger = logger;
            _cameraManager = cameraManager;
        }
        
        public async UniTask<ILoadingScreen> Show(CancellationToken cancellationToken)
        {
            if (_inUse)
            {
                await LoadingScreen.Reset(cancellationToken);
                return LoadingScreen;
            }

            _inUse = true;
            
            Transform systemHost = _screenHostProvider.GetHolderFor(ScreenHolderType.System);
            _loadingScreen = await _basicLoadingScreenDataProvider.BasicLoadingScreenAddressableKey.Instantiate(new InstantiationParameters(systemHost, false), cancellationToken);
            
            Canvas canvas = _loadingScreen.GetComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceCamera;
            canvas.worldCamera = _cameraManager.UICamera;
            canvas.sortingOrder = 10000;
            
            LoadingScreen = _loadingScreen.GetComponent<ILoadingScreen>();
            await LoadingScreen.Show(cancellationToken);

            return LoadingScreen;
        }

        public async UniTask Hide(bool hideBeforeRelease, CancellationToken cancellationToken)
        {
            if (!_inUse)
            {
                _logger.Warn("Attempting to release loading screen while none present");
                return;
            }

            if (hideBeforeRelease)
            {
                await LoadingScreen.Hide(cancellationToken);
            }
            
            Addressables.ReleaseInstance(_loadingScreen);
            _loadingScreen = null;
            LoadingScreen = null;
            _inUse = false;
        }
    }
}