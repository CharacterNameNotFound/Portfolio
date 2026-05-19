using Game.GameMode.Session.Controller;
using Game.GameMode.Session.Controller.GameInitialization;
using Game.GameMode.Session.Game.Data;
using Game.GameMode.Session.Game.Systems;
using Game.GameMode.Session.Game.Systems.Player;
using Game.GameMode.Session.WorldGeneration;
using Game.GameMode.Session.WorldGeneration.DecorationGeneration;
using Game.GameMode.Session.WorldGeneration.RoadGeneration;
using Game.GameMode.Session.WorldGeneration.SchemaApplication;
using Game.GameMode.Session.WorldGeneration.SchemaGeneration;
using Zenject;

namespace Structure
{
    public class SessionInstaller : Installer
    {
        public override void InstallBindings()
        {
            InstallLevelGeneration();
            InstallSessionSystems();
        }

        private void InstallLevelGeneration()
        {
            Container.Bind<IWorldSchemaGenerator>().To<WorldSchemaGenerator>().AsSingle();
            Container.Bind<ISessionInitializer>().To<SessionInitializer>().AsSingle();
            Container.Bind<IWorldGenerationController>().To<WorldGenerationController>().AsSingle();
            Container.Bind<IWorldSchemaApplier>().To<WorldSchemaApplier>().AsSingle();
            Container.Bind<IRoadGenerator>().To<RoadGenerator>().AsSingle();
            Container.Bind<IDecorationGenerator>().To<DecorationGenerator>().AsSingle();
            Container.Bind<SessionRegistry>().To<SessionRegistry>().AsSingle();
            Container.Bind<IGameEndDecider>().To<GameEndDecider>().AsSingle();
            Container.Bind<IGameLooper>().To<GameLooper>().AsSingle();
            Container.Bind<IPlayerSpawner>().To<PlayerSpawner>().AsSingle();
        }

        private void InstallSessionSystems()
        {
            Container.Bind<ILoopedSystem>().To<PlayerMovement>().AsCached();
            Container.Bind<ILoopedSystem>().To<PlayerCameraMovement>().AsCached();
        }
        
    }
}