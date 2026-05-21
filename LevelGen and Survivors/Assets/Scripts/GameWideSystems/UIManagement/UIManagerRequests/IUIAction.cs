using System.Threading;
using Cysharp.Threading.Tasks;

namespace GameWideSystems.UIManagement.UIManagerRequests
{
    public interface IUIAction
    {
        public UIActionType ActionType { get; }
        internal UniTask<UniTask> Handle(UIManager uiManager, CancellationToken cancellationToken);
    }
}