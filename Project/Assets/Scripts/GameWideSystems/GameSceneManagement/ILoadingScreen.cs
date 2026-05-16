using System.Threading;
using Cysharp.Threading.Tasks;

namespace GameWideSystems.GameSceneManager
{
    public interface ILoadingScreen
    {
        public UniTask Show(CancellationToken cancellationToken);
        public UniTask Hide(CancellationToken cancellationToken);
        public void UpdateProgress(float operationProgress);
        public UniTask Reset(CancellationToken cancellationToken);
    }
}