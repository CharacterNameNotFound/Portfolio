using System.Threading;
using Cysharp.Threading.Tasks;

namespace GameWideSystems.UIManagement.UIManagerRequests.UIActionImplementations
{
    public class CloseAllUIAction : IUIAction
    {
        public UIActionType ActionType => UIActionType.Close;
        private ScreenType _screenType;
        private bool _isSilent;

        public CloseAllUIAction(ScreenType screenType, bool isSilent)
        {
            _screenType = screenType;
            _isSilent = isSilent;
        }
        
        async UniTask<UniTask> IUIAction.Handle(UIManager uiManager, CancellationToken cancellationToken)
        {
            await uiManager.CloseAll(_isSilent, _screenType, cancellationToken);
            return UniTask.CompletedTask;
        }

        
    }
}