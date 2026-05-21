using GameWideSystems.UIManagement.Screen;

namespace GameWideSystems.UIManagement.UIManagerRequests
{
    public static class UIRequestBuilder
    {

        public static UIRequest OpenScreenRequest(
            this UIManager uiManager, 
            IUIScreenBuilder screenBuilder, 
            IScreenParams screenParams,
            out ScreenHolder openedScreen)
        {
            UIRequest uiRequest = UIRequest.New;
            uiRequest.UIManager = uiManager;
            
            
            if (screenBuilder.ScreenType == ScreenType.Screen)
            {
                uiRequest.CloseAll();
            }
            else if (uiManager.PreloadedScreens.Count > 0)
            {
                uiRequest.UnloadTopScreen();
            }
            

            uiRequest.OpenScreen(screenBuilder, screenParams, out openedScreen);
            return uiRequest;
        }

        public static UIRequest SwapTopScreenRequest(
            this UIManager uiManager,
            IUIScreenBuilder screenBuilder, 
            IScreenParams screenParams,
            out ScreenHolder openedScreen)
        {
            UIRequest uiRequest = UIRequest.New;
            uiRequest.UIManager = uiManager;
            
            if (screenBuilder.ScreenType == ScreenType.Dialog)
            {
                uiRequest.CloseAll();
            }
            else
            {
                uiRequest.CloseTop();
            }

            uiRequest.OpenScreen(screenBuilder, screenParams, out openedScreen);
            return uiRequest;
        }

        public static UIRequest CloseTopRequest(this UIManager uiManager)
        {
            UIRequest uiRequest = UIRequest.New;
            uiRequest.UIManager = uiManager;
            
            return uiRequest.CloseTop().LoadTopScreen();
        }

        public static UIRequest CloseAllRequest(this UIManager uiManager, ScreenType screenType = ScreenType.Screen)
        {
            UIRequest uiRequest = UIRequest.New;
            uiRequest.UIManager = uiManager;
            
            return uiRequest.CloseAll(screenType);
        }
        
    }
}