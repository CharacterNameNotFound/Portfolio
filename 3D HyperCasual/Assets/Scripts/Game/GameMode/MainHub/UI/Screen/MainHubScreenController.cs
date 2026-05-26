using System.Threading;
using Cysharp.Threading.Tasks;
using GameWideSystems.UIManagement;
using GameWideSystems.UIManagement.Screen;
using UnityEngine;
using UnityEngine.UI;
namespace Game.GameMode.MainHub.UI.Screen
{
    public class MainHubScreenController : UIScreen<IScreenParams, MainHubScreenDependencies>
    {
        [field: SerializeField] private Button _playSelectionButton;
        
        public override ScreenType ScreenType => ScreenType.Screen;
        public override ScreenHolderType ScreenHolderType => ScreenHolderType.Game;
        
        
        public override UniTask<UniTask> OnBeforeOpen(IScreenParams screenParams, CancellationToken cancellationToken)
        {
            UniTask<UniTask> result = base.OnBeforeOpen(screenParams, cancellationToken);

            _playSelectionButton.onClick.AddListener(() => Play(Application.exitCancellationToken).Forget());
            
            return result;
        }

        private async UniTask Play(CancellationToken cancellationToken)
        {
            Dependencies.AudioArchive.PlayButton();
            
            await Dependencies.GameStateManager.AppendGameState(Dependencies.SessionGameModeFactory.Create(), cancellationToken: cancellationToken);
        }
        

       
        
    }
}