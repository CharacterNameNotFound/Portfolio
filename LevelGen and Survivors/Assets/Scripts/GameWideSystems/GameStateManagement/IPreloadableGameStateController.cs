using Cysharp.Threading.Tasks;

namespace GameWideSystems.GameStateManagement
{
    public interface IPreloadableGameStateController : IGameStateController
    {
        public UniTask Preload();
        
        /// <summary>
        /// Used by preloadable game states to unload all preloadables
        /// </summary>
        public UniTask UnloadFully();
    }
}