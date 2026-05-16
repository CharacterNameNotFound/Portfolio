using Game.UI.Tooltips;
using GameWideSystems.TooltipsManagement;
using Utils.UtilityTypes.ObjectPooling;
using Zenject;

namespace Structure
{
    public class TooltipInstaller : Installer
    {
        public override void InstallBindings()
        {
            Container.Bind<ITooltipManager>().To<TooltipManager>().AsSingle().NonLazy();
            
            InstallTextTooltip();
        }
        
        private void InstallTextTooltip()
        {
            Container.Bind<TextTooltipRegisterer>().ToSelf().AsSingle();

        }
        
    }
}