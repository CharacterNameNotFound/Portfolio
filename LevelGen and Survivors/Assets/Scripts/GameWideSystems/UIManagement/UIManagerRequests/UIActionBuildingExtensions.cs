using System.Threading;
using GameWideSystems.UIManagement.Screen;
using GameWideSystems.UIManagement.UIManagerRequests.UIActionImplementations;

namespace GameWideSystems.UIManagement.UIManagerRequests
{
    public static class UIActionBuildingExtensions
    {
        public static UIRequest UnlockLockInputs(this UIRequest request, bool value = true)
        {
            request.IsLockInputs = value;
            return request;
        }
        
        public static UIRequest OpenScreen(this UIRequest request,
            IUIScreenBuilder screenBuilder,
            IScreenParams screenParams, 
            out ScreenHolder openedScreen)
        {
            request.AppendAction(new OpenScreenUIAction(screenBuilder, screenParams, false, out openedScreen));
            return request;
        }

        public static UIRequest OpenScreenSilent(this UIRequest request, 
            IUIScreenBuilder screenBuilder, 
            IScreenParams screenParams,
            out ScreenHolder openedScreen)
        {
            request.AppendAction(new OpenScreenUIAction(screenBuilder, screenParams, true, out openedScreen));
            return request;
        }

        public static UIRequest CloseTop(this UIRequest request)
        {
            request.AppendAction(new CloseTopUIAction(false));
            return request;
        }
        
        public static UIRequest CloseTopSilent(this UIRequest request)
        {
            request.AppendAction(new CloseTopUIAction(true));
            return request;
        }

        public static UIRequest CloseAllSilent(this UIRequest request, ScreenType screenType = ScreenType.Screen, bool await = true)
        {
            request.AppendAction(new CloseAllUIAction(screenType, true));
            return request;
        }
        
        public static UIRequest CloseAll(this UIRequest request, ScreenType screenType = ScreenType.Screen, bool await = true)
        {
            request.AppendAction(new CloseAllUIAction(screenType, false));
            return request;
        }

        public static UIRequest LoadTopScreen(this UIRequest request)
        {
            request.AppendAction(new LoadTopScreenUIAction());
            return request;
        }
        
        public static UIRequest UnloadTopScreen(this UIRequest request)
        {
            request.AppendAction(new UnloadTopScreenUIAction());
            return request;
        }
        
    }
}