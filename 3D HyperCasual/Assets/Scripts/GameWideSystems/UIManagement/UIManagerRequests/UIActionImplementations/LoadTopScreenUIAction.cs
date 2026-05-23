using System.Threading;
using Cysharp.Threading.Tasks;

namespace GameWideSystems.UIManagement.UIManagerRequests.UIActionImplementations
{
    public class LoadTopScreenUIAction : IUIAction
    {
        public UIActionType ActionType => UIActionType.Open;
        
        UniTask<UniTask> IUIAction.Handle(UIManager uiManager, CancellationToken cancellationToken)
        {
            return uiManager.LoadTopScreen(cancellationToken);
        }

        
    }
}