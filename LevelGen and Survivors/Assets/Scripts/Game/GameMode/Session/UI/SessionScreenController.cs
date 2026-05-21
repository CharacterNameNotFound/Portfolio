using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.Session.Game.Items;
using GameWideSystems.UIManagement;
using GameWideSystems.UIManagement.Screen;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Game.GameMode.Session.UI
{
    // Can use prebuilt state machine, but not worth  (it was worth it, taking into account level-ups)
    public class SessionScreenController : UIScreen<IScreenParams, SessionScreenDependencies>
    {
        private static readonly int ValueID = Shader.PropertyToID("_Hp");
        
        
        public override ScreenType ScreenType => ScreenType.Screen;
        public override ScreenHolderType ScreenHolderType => ScreenHolderType.Game;


        [SerializeField] private Button _toMainMenuButton;
        [SerializeField] private Button _loseScreenButton;
        [SerializeField] private Button _pauseButton;
        [SerializeField] private Image _expBar;
        [SerializeField] private GameObject _loseScreen;
        
        [SerializeField] private GameObject _levelUpDialog;
        [SerializeField] private LevelUpOptionUIElement[] _levelUpElements;


        private int _selectedOptionIndex;
        private bool _isLevelUpPressed;
        

        public override UniTask<UniTask> OnBeforeOpen(IScreenParams screenParams, CancellationToken cancellationToken)
        {
            _isLevelUpPressed = false;
            
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

        public void SetExp(float expPercentile)
        {
            _expBar.material.SetFloat(ValueID, expPercentile);
        }

        public async UniTask<int> ShowLevelUpScreen(IItem[] upgradeOptions, CancellationToken cancellationToken)
        {
            _selectedOptionIndex = 0;
            _levelUpDialog.gameObject.SetActive(true);

            for (int i = 0; i < _levelUpElements.Length; i++)
            {
                _levelUpElements[i].Show(upgradeOptions[i]);
            }
            
            // as we can only turn off the game, no need for cancellation token source
            await UniTask.WaitUntil(() => _isLevelUpPressed, cancellationToken: cancellationToken);

            _isLevelUpPressed = false;
            _levelUpDialog.gameObject.SetActive(false);

            return _selectedOptionIndex;
        }

        public void SetSelectedItem(int itemId)
        {
            _selectedOptionIndex = itemId;
        }
        
        public void SetLevelUpPressed()
        {
            _isLevelUpPressed = true;
        }
        
    }
}