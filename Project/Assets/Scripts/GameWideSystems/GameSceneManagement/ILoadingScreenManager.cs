using System.Threading;
using Cysharp.Threading.Tasks;
using GameWideSystems.GameSceneManager;

namespace GameWideSystems.GameSceneManagement
{
    public interface ILoadingScreenManager
    {
        public ILoadingScreen LoadingScreen { get; }
        public UniTask<ILoadingScreen> Show(CancellationToken cancellationToken);
        public UniTask Hide(bool hideBeforeRelease, CancellationToken cancellationToken);
    }
}