using GameWideSystems.AudioManager;
using GameWideSystems.CameraManagement;
using GameWideSystems.GameSceneManagement;
using GameWideSystems.GameSceneManager;
using GameWideSystems.GameSceneManager.LoadingScreen;
using GameWideSystems.GameStateManagement;
using GameWideSystems.LocalizationWrapper;
using GameWideSystems.RNGManagement;
using GameWideSystems.ScriptedVisualEffectManagement;
using GameWideSystems.ScriptedVisualEffectManagement.FlyingParticleScriptedVisualEffect;
using GameWideSystems.ScriptedVisualEffectManagement.FlyingTextScriptedVisualEffects;
using GameWideSystems.SessionManagement.Sessions;
using GameWideSystems.UIManagement;
using UnityEngine;
using Utils.UtilityTypes.ObjectPooling;
using Zenject;
using Logger = GameWideSystems.Logger.Logger;

namespace Structure.GameInstalling
{
    // Somewhat universal classes
    public class GameWideSystemsInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private AudioManagerConfigurations _audioManagerConfigurations;
        [SerializeField] private GameObject _uiManagementPrefab;
        [SerializeField] private GameObject _cameraManager;
        [SerializeField] private GenericPooledObjectHostProvider _genericPooledObjectHostProvider;
        
        public override void InstallBindings()
        {
            InstallLogger();
            InstallAudioManager();
            InstallGameSceneManager();
            InstallGameStateManager();
            InstallLocalization();
            InstallUIManager();
            InstallSession();
            InstallGenericHosts();
            InstallCameraManager();
            InstallRandom();
            InstallScriptedVisualEffects();
        }


        private void InstallGenericHosts()
        {
            Container.Bind<IPooledObjectHostProvider>().FromComponentInNewPrefab(_genericPooledObjectHostProvider).AsSingle().NonLazy();
        }

        private void InstallSession()
        {
            Container.Bind<SessionHolder>().To<SessionHolder>().AsSingle();

        }

        private void InstallUIManager()
        {
            Container.Bind<IScreenHostProvider>().FromComponentInNewPrefab(_uiManagementPrefab).AsSingle().NonLazy();
            Container.Bind<UIManager>().To<UIManager>().AsSingle();
        }

        private void InstallLocalization()
        {
            Container.Bind<ILocalizationManager>().To<LocalizationManager>().AsSingle();
        }

        private void InstallLogger()
        {
            Container.Bind<Logger>().ToSelf().AsSingle();
        }

        private void InstallAudioManager()
        {
            Container.Bind<AudioManagerConfigurations>().FromInstance(_audioManagerConfigurations).AsSingle();
            Container.Bind<AudioManager>().To<AudioManager>().AsSingle().NonLazy();
        }
        
        private void InstallGameSceneManager()
        {
            Container.Bind<IGameSceneManager>().To<GameSceneManager>().AsSingle().NonLazy();
            Container.Bind<ILoadingScreenManager>().To<BasicLoadingScreenManager>().AsSingle();
        }

        private void InstallGameStateManager()
        {
            Container.Bind<GameStateManager>().To<GameStateManager>().AsSingle();
        }

        private void InstallCameraManager()
        {
            Container.Bind<ICameraManager>().FromComponentInNewPrefab(_cameraManager).AsSingle();
        }
        
        private void InstallRandom()
        {
            Container.Bind<IRNGManager>().To<RNGManager>().AsSingle();
        }
        
        private void InstallScriptedVisualEffects()
        {
            Container.Bind<IScriptedVisualEffectManager>().To<ScriptedVisualEffectManager>().AsSingle();
            Container.Bind<FlyingTextScriptedVisualEffectRegisterer>().To<FlyingTextScriptedVisualEffectRegisterer>().AsSingle();
            Container.Bind<FlyingParticleScriptedVisualEffectRegisterer>().To<FlyingParticleScriptedVisualEffectRegisterer>().AsSingle();
        }
        
    }
}
