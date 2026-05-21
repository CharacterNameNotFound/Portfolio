using Game.GameMode.MainHub.UI.Screen;
using Game.GameMode.Session.UI;
using Zenject;

namespace Structure.GameInstalling
{
    public class ScreenBuilderInstaller : Installer
    {
        public override void InstallBindings()
        {
            InstallMainScreen();
            InstallSessionScreen();
            
        }

        private void InstallMainScreen()
        {
            Container.Bind<MainHubScreenBuilder>().To<MainHubScreenBuilder>().AsSingle();
            Container.Bind<MainHubScreenDependencies>().To<MainHubScreenDependencies>().AsSingle();
        }

        private void InstallSessionScreen()
        {
            Container.Bind<SessionScreenBuilder>().To<SessionScreenBuilder>().AsSingle();
            Container.Bind<SessionScreenDependencies>().To<SessionScreenDependencies>().AsSingle();
        }
        
    }
}