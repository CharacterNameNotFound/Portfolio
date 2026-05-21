using Game.GameMode.Initializer;
using Game.GameMode.MainHub.Controller;
using Game.GameMode.Session.Controller;
using Zenject;

namespace Structure.GameInstalling
{
    public class GameModeInstaller : Installer
    {
        public override void InstallBindings()
        {
            Container.Bind<InitializationGameMode>().To<InitializationGameMode>().AsSingle();
            Container.BindFactory<MainHubGameMode, MainHubGameMode.Factory>().FromFactory<MainHubGameModeFactory>();
            Container.BindFactory<SessionGameMode, SessionGameMode.Factory>().FromFactory<SessionGameModeFactory>();
            
        }
        


        
    }
}