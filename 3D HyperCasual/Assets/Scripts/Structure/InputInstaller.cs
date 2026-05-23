using GameWideSystems.InputManager;
using GameWideSystems.InputManager.DefaultHandlingLayers;
using GameWideSystems.InputManager.ReadingCores;
using GameWideSystems.InputManager.ReadingCores.Keyboard;
using GameWideSystems.InputManager.ReadingCores.Pointer;
using GameWideSystems.TooltipsManagement;
using UnityEngine;
using Zenject;

namespace Structure.GameInstalling
{
    public class InputInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private PointerInputConfigurationsSOProviders _pointerInputConfigurationsSoProviders;
        
        public override void InstallBindings()
        {
            InstallInputLayers();
            InstallInputs();
        }

        private void InstallInputs()
        {
            Container.Bind<IPointerInputConfigurationsProvider>().FromInstance(_pointerInputConfigurationsSoProviders).AsSingle();

            Container.Bind<MainInputs>().To<MainInputs>().FromNew().AsSingle().NonLazy();
            Container.Bind<IInputHost>().To<InputHost>().FromNew().AsSingle().NonLazy();
            Container.Bind<PointerCore>().To<PointerCore>().FromNew().AsSingle().NonLazy();
            Container.Bind<IInputReadingCore>().To<PointerCore>().FromResolve().AsCached().NonLazy();
            Container.Bind<IInputReadingCore>().To<KeyboardReader>().FromNew().AsCached().NonLazy();
            Container.Bind<InputReader>().To<InputReader>().FromNew().AsSingle().NonLazy();
            
            Container.Bind<InputControlFacade>().To<InputControlFacade>().FromNew().AsSingle();

            Container.BindInterfacesTo<IInputReadingCore>().FromResolveAll().AsCached();
        }
        
        private void InstallInputLayers()
        {
            Container.Bind<UIInputPointerHandlingLayer>().To<UIInputPointerHandlingLayer>().AsSingle();
            Container.Bind<TooltipInputLayer>().To<TooltipInputLayer>().AsSingle();
            
            Container.Bind<IInputHandlerLayer>().To<UIInputPointerHandlingLayer>().FromResolve().AsCached();
            Container.Bind<IInputHandlerLayer>().To<TooltipInputLayer>().FromResolve().AsCached();
        }
        
    }
}