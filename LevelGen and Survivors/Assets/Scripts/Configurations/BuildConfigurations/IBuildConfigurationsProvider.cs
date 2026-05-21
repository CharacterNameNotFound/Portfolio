using Configurations.PlatformDependentFields;

namespace Configurations.BuildConfigurations
{
    public interface IBuildConfigurationsProvider
    {
        public PlatformType PlatformType { get; }
    }
}