using Configurations.PlatformDependentFields;
using UnityEngine;

namespace Configurations.BuildConfigurations
{
    public class BuildConfigurationsData : ScriptableObject, IBuildConfigurationsProvider
    {
        [field: SerializeField] public PlatformType PlatformType { get; private set; }
    }
}