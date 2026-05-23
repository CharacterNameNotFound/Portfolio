using Game.Routines.ProfileOperations.ProfileCreation;
using Game.Routines.ProfileOperations.ProfileLoading;
using Game.Session;
using Zenject;

namespace Structure.GameInstalling
{
    /// <summary>
    /// Specific classes for game management
    /// </summary>
    public class GameLevelSystemInstaller : Installer
    {
        public override void InstallBindings()
        {
            InstallSessionManagement();
            InstallRoutines();
            InstallPathProviders();
        }

        private void InstallSessionManagement()
        {
            Container.Bind<OfflineProfileCreationValidation>().To<OfflineProfileCreationValidation>().AsSingle();
        }
        
        private void InstallRoutines()
        {
            Container.Bind<IProfileCreationRoutine>().To<OfflineProfileCreationRoutine>().AsSingle();
            Container.Bind<IProfileLoadingRoutine>().To<OfflineProfileLoadingRoutine>().AsSingle();
        }

        private void InstallPathProviders()
        {
            Container.Bind<GenericPathProvider>().To<GenericPathProvider>().AsSingle();
        }
        
        
    }
}