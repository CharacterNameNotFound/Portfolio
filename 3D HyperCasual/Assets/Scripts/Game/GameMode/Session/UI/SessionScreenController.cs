using GameWideSystems.UIManagement;
using GameWideSystems.UIManagement.Screen;

namespace Game.GameMode.Session.UI
{
    public class SessionScreenController : UIScreen<IScreenParams, SessionScreenDependencies>
    {
        public override ScreenType ScreenType => ScreenType.Screen;
        public override ScreenHolderType ScreenHolderType => ScreenHolderType.Game;
        
        
        
    }
}