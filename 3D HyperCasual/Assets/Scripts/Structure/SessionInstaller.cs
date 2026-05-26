using System.Collections.Generic;
using Game.GameMode.Session.Controller;
using Game.GameMode.Session.Gameplay.Entities;
using Game.GameMode.Session.Gameplay.GameplayLoop;
using Game.GameMode.Session.Gameplay.GameplayLoop.Systems;
using Game.GameMode.Session.Gameplay.Inputs;
using Game.GameMode.Session.Gameplay.Pools.CubePooling;
using Game.GameMode.Session.Gameplay.Pools.PoppedCubeParticlePooling;
using Game.Utilities.MusicControlling;
using GameWideSystems.InputManager;
using Utils.UtilityTypes.ObjectPooling;
using Zenject;

namespace Structure
{
    public class SessionInstaller : Installer
    {
        public override void InstallBindings()
        {
            InstallPools();
            InstallSystems();
            InstallInputs();
            InstallLoopedSystems();
        }


        private void InstallSystems()
        {
            Container.Bind<AudioArchive>().To<AudioArchive>().AsSingle();
            Container.Bind<SessionRegistry>().To<SessionRegistry>().AsSingle();
            Container.Bind<ISessionInitializer>().To<SessionInitializer>().AsSingle();
            Container.Bind<IGameLoop>().To<GameLoop>().AsSingle();
            Container.Bind<IGameFinishConditionChecker>().To<GameFinishConditionChecker>().AsSingle();
        }

        private void InstallPools()
        {
            Container.Bind<List<GameCubeComponent>>().To<List<GameCubeComponent>>().AsSingle();
            Container.Bind<IPoolEntityBuilder<GameCubeComponent>>().To<GameCubeComponentEntityProvider>().AsSingle();
            Container.Bind<GameCubePool>().To<GameCubePool>().AsSingle();
            
            Container.Bind<List<PoppedParticlesComponent>>().To<List<PoppedParticlesComponent>>().AsSingle();
            Container.Bind<IPoolEntityBuilder<PoppedParticlesComponent>>().To<PoppedCubeParticleEntityProvider>().AsSingle();
            Container.Bind<PoppedCubeParticlePool>().To<PoppedCubeParticlePool>().AsSingle();
            
        }
        
        private void InstallInputs()
        {
            Container.Bind<InputBuffer>().To<InputBuffer>().AsSingle();
            Container.Bind<IInputHandlerLayer>().To<KeyboardInputLayer>().AsCached();
            
        }
        
        private void InstallLoopedSystems()
        {
            // player systems
            Container.Bind<ILoopedSystem>().To<ScreenButtonReaderSystem>().AsSingle();
            Container.Bind<ILoopedSystem>().To<HammerAnimationSystem>().AsSingle();
            Container.Bind<ILoopedSystem>().To<HammersReactionSystem>().AsSingle();
            
            
            // game field
            Container.Bind<ILoopedSystem>().To<CubeMovementSystem>().AsSingle();
            Container.Bind<ILoopedSystem>().To<CubeSpawningSystem>().AsSingle();
            
            
            // reset systems
            Container.Bind<ILoopedSystem>().To<InputResetSystem>().AsSingle();
            
        }
        
    }
}