using System.Threading;
using Cysharp.Threading.Tasks;

namespace GameWideSystems.UIManagement.UIManagerRequests.UIActionImplementations
{
    public class UnloadTopScreenUIAction : IUIAction
    {
        public UIActionType ActionType => UIActionType.Close;

        private bool _isSilent;
        
        UniTask<UniTask> IUIAction.Handle(UIManager uiManager, CancellationToken cancellationToken)
        {
            return uiManager.UnloadTopScreen(cancellationToken);
        }

    }
}