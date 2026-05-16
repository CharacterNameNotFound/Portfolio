using Structure.GameInstalling;
using Zenject;
using GameModeInstaller = Structure.GameInstalling.GameModeInstaller;

namespace Structure
{
    public class CoreInstaller : ScriptableObjectInstaller
    {
        public override void InstallBindings()
        {
            Container.Install<GameModeInstaller>();
            Container.Install<GameLevelSystemInstaller>();
            Container.Install<ScreenBuilderInstaller>();
            Container.Install<TooltipInstaller>();
            Container.Install<UtilityInstaller>();
        }
    }
}