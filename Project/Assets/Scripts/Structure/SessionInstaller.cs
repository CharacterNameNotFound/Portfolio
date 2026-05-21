using System.Collections.Generic;
using Game.GameMode.Session.Controller.GameInitialization;
using Game.GameMode.Session.Game.Data;
using Game.GameMode.Session.Game.Data.Entities;
using Game.GameMode.Session.Game.Systems;
using Game.GameMode.Session.Game.Systems.Enemies;
using Game.GameMode.Session.Game.Systems.Player;
using Game.GameMode.Session.Game.Systems.Weapons;
using Game.GameMode.Session.Game.Utilities;
using Game.GameMode.Session.Pools.EnemyBuilding;
using Game.GameMode.Session.Pools.ExperiencePool;
using Game.GameMode.Session.WorldGeneration;
using Game.GameMode.Session.WorldGeneration.DecorationGeneration;
using Game.GameMode.Session.WorldGeneration.RoadGeneration;
using Game.GameMode.Session.WorldGeneration.SchemaApplication;
using Game.GameMode.Session.WorldGeneration.SchemaGeneration;
using Utils.UtilityTypes.ObjectPooling;
using Zenject;

namespace Structure
{
    public class SessionInstaller : Installer
    {
        public override void InstallBindings()
        {
            InstallLevelGeneration();
            InstallSessionSystems();
            InstallPools();
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
            Container.Bind<IScenarioDataInitializer>().To<ScenarioDataInitializer>().AsSingle();
            Container.Bind<IEnemyInitializer>().To<EnemyInitializer>().AsSingle();
            Container.Bind<IItemInitializer>().To<ItemInitializer>().AsSingle();
            
            Container.Bind<SessionScreenHolder>().To<SessionScreenHolder>().AsSingle();
        }

        private void InstallSessionSystems()
        {
            Container.Bind<ILoopedSystem>().To<PlayerMovement>().AsCached();
            Container.Bind<ILoopedSystem>().To<PlayerCameraMovement>().AsCached();
            Container.Bind<ILoopedSystem>().To<CollectExp>().AsCached();
            Container.Bind<ILoopedSystem>().To<PlayerLevelUp>().AsCached();
            
            Container.Bind<ILoopedSystem>().To<EnemySpawner>().AsCached();
            Container.Bind<ILoopedSystem>().To<EnemyMover>().AsCached();
            Container.Bind<ILoopedSystem>().To<EnemyDamageDealer>().AsCached();
            
            Container.Bind<ILoopedSystem>().To<WeaponActivator>().AsCached();
            
            Container.Bind<ILoopedSystem>().To<UpdatePlayerHp>().AsCached();
            Container.Bind<ILoopedSystem>().To<UpdateEnemyHp>().AsCached();


        }

        private void InstallPools()
        {
            // Enemy pool
            Container.Bind<List<EnemyComponent>>().To<List<EnemyComponent>>().AsCached();
            Container.Bind<IPoolEntityBuilder<EnemyComponent>>().To<EnemyComponentEntityProvider>().AsCached();
            Container.Bind<EnemyPool>().To<EnemyPool>().AsCached();
            
            // Exp gems
            Container.Bind<List<ExpGemComponent>>().To<List<ExpGemComponent>>().AsCached();
            Container.Bind<IPoolEntityBuilder<ExpGemComponent>>().To<ExpGemComponentEntityProvider>().AsCached();
            Container.Bind<ExpGemPool>().To<ExpGemPool>().AsCached();
        }
        
        
    }
}