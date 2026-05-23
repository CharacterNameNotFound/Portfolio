using Configurations.BuildConfigurations;
using GameWideSystems.UIManagement.Screen;

namespace Game.GameMode.Session.UI
{
    public class SessionScreenBuilder : GenericUIScreenBuilder<IScreenAddressableReferenceProvider<SessionScreenController>, SessionScreenDependencies, SessionScreenController>
    {
        public SessionScreenBuilder(IBuildConfigurationsProvider buildConfigurationsProvider, IScreenAddressableReferenceProvider<SessionScreenController> gameModeAddressableProvider, SessionScreenDependencies dialogDependencies) : base(buildConfigurationsProvider, gameModeAddressableProvider, dialogDependencies)
        {
        }
    }
}