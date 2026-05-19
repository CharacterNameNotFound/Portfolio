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


        [SerializeField] private Button _toMainMenuButton;
        [SerializeField] private Button _loseScreenButton;
        [SerializeField] private Button _pauseButton;
        [SerializeField] private GameObject _loseScreen;

        public override UniTask<UniTask> OnBeforeOpen(IScreenParams screenParams, CancellationToken cancellationToken)
        {
            UniTask<UniTask> result = base.OnBeforeOpen(screenParams, cancellationToken);
            
            _toMainMenuButton.onClick.AddListener(ToMainMenu);
            _loseScreenButton.onClick.AddListener(ToMainMenu);
            
            _pauseButton.onClick.AddListener(Pause);
            
            return result;
        }

        private void ToMainMenu()
        {
            GameObject first = SceneManager.GetActiveScene().GetRootGameObjects().First();
            
            Destroy(first);

            Dependencies.GameStateManager.CloseCurrentGameState(true, cancellationToken: Application.exitCancellationToken).Forget();
        }

        private void Pause()
        {
            Time.timeScale = Time.timeScale > 0.5 ? 0 : 1;
        }

        public void ShowLoseScreen()
        {
            _loseScreen.SetActive(true);
        }
    }
}