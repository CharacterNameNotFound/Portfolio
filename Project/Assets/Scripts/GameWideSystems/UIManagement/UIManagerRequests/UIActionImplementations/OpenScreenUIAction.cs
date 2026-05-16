using System.Threading;
using Cysharp.Threading.Tasks;
using GameWideSystems.UIManagement.Screen;

namespace GameWideSystems.UIManagement.UIManagerRequests.UIActionImplementations
{
    public class OpenScreenUIAction : IUIAction
    {
        private IUIScreenBuilder _uiScreenBuilder;
        private IScreenParams _screenParams;
        private bool _isSilent;
        private ScreenHolder _openedScreen;

        public UIActionType ActionType => UIActionType.Open;

        public OpenScreenUIAction(IUIScreenBuilder uiScreenBuilder, IScreenParams screenParams, bool isSilent,
            out ScreenHolder openedScreen)
        {
            _uiScreenBuilder = uiScreenBuilder;
            _screenParams = screenParams;
            _isSilent = isSilent;
            openedScreen = _openedScreen = new ScreenHolder();
        }

        UniTask<UniTask> IUIAction.Handle(UIManager uiManager, CancellationToken cancellationToken)
        {
            return uiManager.OpenScreen(_uiScreenBuilder, _screenParams, _isSilent, _openedScreen, cancellationToken);
        }

        

    }
}