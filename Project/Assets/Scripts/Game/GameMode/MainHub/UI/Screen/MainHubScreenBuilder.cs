using Configurations.BuildConfigurations;
using GameWideSystems.UIManagement.Screen;

namespace Game.GameMode.MainHub.UI.Screen
{
    public class MainHubScreenBuilder : GenericUIScreenBuilder<IScreenAddressableReferenceProvider<MainHubScreenController>, MainHubScreenDependencies, MainHubScreenController>
    {
        public MainHubScreenBuilder(IBuildConfigurationsProvider buildConfigurationsProvider, IScreenAddressableReferenceProvider<MainHubScreenController> gameModeAddressableProvider, MainHubScreenDependencies screenDependencies) : base(buildConfigurationsProvider, gameModeAddressableProvider, screenDependencies)
        {
        }
    }
}