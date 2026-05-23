using System.Threading;
using Cysharp.Threading.Tasks;

namespace GameWideSystems.UIManagement.UIManagerRequests.UIActionImplementations
{
    public class CloseTopUIAction : IUIAction
    {
        public UIActionType ActionType => UIActionType.Close;
        private bool _isSilent;

        public CloseTopUIAction(bool isSilent)
        {
            _isSilent = isSilent;
        }
        
        async UniTask<UniTask> IUIAction.Handle(UIManager uiManager, CancellationToken cancellationToken)
        {
            await uiManager.CloseTop(_isSilent, cancellationToken);
            return UniTask.CompletedTask;
        }

        
    }
}