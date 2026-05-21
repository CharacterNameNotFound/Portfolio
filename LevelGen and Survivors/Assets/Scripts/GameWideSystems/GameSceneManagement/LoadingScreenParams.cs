using GameWideSystems.GameSceneManagement;

namespace GameWideSystems.GameSceneManager
{
    public class LoadingScreenParams
    {
        public readonly ILoadingScreenManager LoadingScreenManager;
        public readonly bool IsLoadingScreenClosedAutomatically;

        public LoadingScreenParams(bool isLoadingScreenClosedAutomatically, ILoadingScreenManager loadingScreenManager)
        {
            IsLoadingScreenClosedAutomatically = isLoadingScreenClosedAutomatically;
            LoadingScreenManager = loadingScreenManager;
        }
    }
}