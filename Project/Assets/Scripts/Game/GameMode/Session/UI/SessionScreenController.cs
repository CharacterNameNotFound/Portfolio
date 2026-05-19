using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using GameWideSystems.UIManagement;
using GameWideSystems.UIManagement.Screen;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Game.GameMode.Session.UI
{
    // Can use prebuilt state machine, but not worth it
    public class SessionScreenController : UIScreen<IScreenParams, SessionScreenDependencies>
    {
        public override ScreenType ScreenType => ScreenType.Screen;
        public override ScreenHolderType ScreenHolderType => ScreenHolderType.Game;


        [SerializeField] private Button _button;

        public override UniTask<UniTask> OnBeforeOpen(IScreenParams screenParams, CancellationToken cancellationToken)
        {
            UniTask<UniTask> result = base.OnBeforeOpen(screenParams, cancellationToken);
            
            _button.onClick.AddListener(OnPress);
            
            return result;
        }

        private void OnPress()
        {
            GameObject first = SceneManager.GetActiveScene().GetRootGameObjects().First();
            
            Destroy(first);

            Dependencies.GameStateManager.CloseCurrentGameState(true, cancellationToken: Application.exitCancellationToken).Forget();
            //Dependencies._sessionInitializer.InitializeSession(Application.exitCancellationToken).Forget();
        }
    }
}