using System.Threading;
using Configurations.BuildConfigurations;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;
using Utils.UtilityTypes.AssetReferencing;

namespace GameWideSystems.UIManagement.Screen
{
    public class GenericUIScreenBuilder<TAddressableProvider, TUIScreenDependencies, TScreenControllerContract> : IUIScreenBuilder 
        where TAddressableProvider : IScreenAddressableReferenceProvider<TScreenControllerContract> 
        where TUIScreenDependencies : IUIScreenDependencies 
        where TScreenControllerContract : UIScreenBase  
    {
        private readonly IBuildConfigurationsProvider _buildConfigurationsProvider;
        private TAddressableProvider _gameModeAddressableProvider;
        private TUIScreenDependencies _dialogDependencies;
        
        public virtual ScreenType ScreenType => ScreenType.Screen;

        public GenericUIScreenBuilder(IBuildConfigurationsProvider buildConfigurationsProvider, TAddressableProvider gameModeAddressableProvider, TUIScreenDependencies dialogDependencies)
        {
            _buildConfigurationsProvider = buildConfigurationsProvider;
            _gameModeAddressableProvider = gameModeAddressableProvider;
            _dialogDependencies = dialogDependencies;
        }
        
        public async UniTask<UIScreenBase> Build(Transform creationHolder, Camera uiCamera, CancellationToken cancellationToken)
        {
            AssetReference uiReference = _gameModeAddressableProvider.GetScreenRuntimeKey(_buildConfigurationsProvider.PlatformType);
            GameObject screen = await uiReference.Instantiate(new InstantiationParameters(creationHolder, false), cancellationToken);

            screen.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceCamera;
            screen.GetComponent<Canvas>().worldCamera = uiCamera;
            
            screen.SetActive(false);
            await UniTask.NextFrame();
            cancellationToken.ThrowIfCancellationRequested();
            
            UIScreenBase screenController = screen.GetComponent<UIScreenBase>();

            screenController.SetScreenBuilder(this);
            await screenController.Initialize(_dialogDependencies, cancellationToken);
            
            return screenController;
        }
    }
}