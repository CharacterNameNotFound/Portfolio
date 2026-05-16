using System.Threading;
using Cysharp.Threading.Tasks;
using GameWideSystems.UIManagement;
using GameWideSystems.UIManagement.Screen;
using UnityEngine;
using UnityEngine.UI;
using Utils.UtilityTypes.Result;

namespace Game.GameMode.MainHub.UI.Screen
{
    public class MainHubScreenController : UIScreen<IScreenParams, MainHubScreenDependencies>
    {
        [field: SerializeField] private Button _playSelectionButton;
        [field: SerializeField] private Button _continueSelectionButton;

        
        
        public override ScreenType ScreenType => ScreenType.Screen;
        public override ScreenHolderType ScreenHolderType => ScreenHolderType.Game;
        
        
        public override async UniTask<UniTask> OnBeforeOpen(IScreenParams screenParams, CancellationToken cancellationToken)
        {
            UniTask<UniTask> result = base.OnBeforeOpen(screenParams, cancellationToken);

            return result;
        }


        
        public override void OnAfterClose()
        {
            base.OnAfterClose();
        }
        

       
        
    }
}