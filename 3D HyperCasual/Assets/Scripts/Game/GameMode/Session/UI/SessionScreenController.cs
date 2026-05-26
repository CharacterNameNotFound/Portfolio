using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using GameWideSystems.UIManagement;
using GameWideSystems.UIManagement.Screen;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.GameMode.Session.UI
{
    public class SessionScreenController : UIScreen<IScreenParams, SessionScreenDependencies>
    {
        private static readonly int Hp = Shader.PropertyToID("_Hp");
        
        public List<ColorButton> ColorButtons;
        public TMP_Text ScoreText;
        public Image HpBar;
        public GameObject LoseScreen;
        
        public override ScreenType ScreenType => ScreenType.Screen;
        public override ScreenHolderType ScreenHolderType => ScreenHolderType.Game;

        // We're updating UI each fixed update anyway 
        private void FixedUpdate()
        {
            ScoreText.text = Dependencies.SessionRegistry.Score.ToString();
            HpBar.material.SetFloat(Hp, Dependencies.SessionRegistry.Lives / (float)Dependencies.SessionRegistry.LivesMax);
        }

        public void FinishGame()
        {
            Dependencies.GameStateManager.CloseCurrentGameState(true, cancellationToken: Application.exitCancellationToken).Forget();
            Time.timeScale = 1;
        }
        
    }
}