using Configurations.BuildConfigurations;
using UnityEngine;
using Zenject;

namespace Structure
{
    public class ConfigurationInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private BuildConfigurationsData _buildConfigurationsData;
        
        public override void InstallBindings()
        {
            Container.Bind<IBuildConfigurationsProvider>().FromInstance(_buildConfigurationsData);
        }
    }
}