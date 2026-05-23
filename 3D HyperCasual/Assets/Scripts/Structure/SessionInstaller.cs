using Game.Utilities.MusicControlling;
using Zenject;

namespace Structure
{
    public class SessionInstaller : Installer
    {
        public override void InstallBindings()
        {
            Container.Bind<AudioArchive>().To<AudioArchive>().AsSingle();
        }
        
        
        
    }
}