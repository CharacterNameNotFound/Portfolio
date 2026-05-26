using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.Session.Gameplay.Inputs;
using Game.GameMode.Session.UI;

namespace Game.GameMode.Session.Gameplay.GameplayLoop.Systems
{
    public class ScreenButtonReaderSystem : ILoopedSystem
    {
        private InputBuffer _inputBuffer;

        public ScreenButtonReaderSystem(InputBuffer inputBuffer)
        {
            _inputBuffer = inputBuffer;
        }

        public UniTask Initialize(SessionRegistry sessionRegistry, CancellationToken cancellationToken)
        {
            foreach (ColorButton button in sessionRegistry.SessionScreen.ColorButtons)
            {
                button.OriginalScale = button.Transform.sizeDelta;
            }
            
            return UniTask.CompletedTask;
        }

        public UniTask Update(float delta, SessionRegistry sessionRegistry, CancellationToken cancellationToken)
        {
            List<ColorButton> sessionScreenColorButtons = sessionRegistry.SessionScreen.ColorButtons;
            for (int i = 0; i < sessionScreenColorButtons.Count; i++)
            {
                if (!sessionScreenColorButtons[i].Pressed)
                {
                    continue;
                }

                sessionScreenColorButtons[i].Pressed = false;
                _inputBuffer.ActivatedLines[i] = true;
            }
            
            return UniTask.CompletedTask;
        }

        public UniTask CleanUp(SessionRegistry sessionRegistry, CancellationToken cancellationToken)
        {
            return UniTask.CompletedTask;
        }
    }
}