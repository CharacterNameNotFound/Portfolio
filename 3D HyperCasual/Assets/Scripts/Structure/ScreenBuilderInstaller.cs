using Game.GameMode.MainHub.UI.Screen;
using Zenject;

namespace Structure.GameInstalling
{
    public class ScreenBuilderInstaller : Installer
    {
        public override void InstallBindings()
        {
            InstallMainScreen();
            
        }

        private void InstallMainScreen()
        {
            Container.Bind<MainHubScreenBuilder>().To<MainHubScreenBuilder>().AsSingle();
            Container.Bind<MainHubScreenDependencies>().To<MainHubScreenDependencies>().AsSingle();
        }

        
    }
}