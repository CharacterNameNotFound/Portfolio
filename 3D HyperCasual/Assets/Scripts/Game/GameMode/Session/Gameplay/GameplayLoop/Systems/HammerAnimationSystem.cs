using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.Session.Gameplay.Entities;
using Game.GameMode.Session.Gameplay.GameplayLoop.Systems.Configs;
using Game.GameMode.Session.Gameplay.Inputs;
using Game.GameMode.Session.UI;
using UnityEngine;

namespace Game.GameMode.Session.Gameplay.GameplayLoop.Systems
{
    public class HammerAnimationSystem : ILoopedSystem
    {
        private InputBuffer _inputBuffer;
        private HammerSystemsConfig _hammerConfigs;

        public HammerAnimationSystem(InputBuffer inputBuffer, HammerSystemsConfig hammerConfigs)
        {
            _inputBuffer = inputBuffer;
            _hammerConfigs = hammerConfigs;
        }

        public UniTask Initialize(SessionRegistry sessionRegistry, CancellationToken cancellationToken)
        {
            foreach (HammerCubeComponent hammer in sessionRegistry.GameFieldComponent.HummerPoints)
            {
                hammer.OriginalPosition = hammer.Transform.position;
            }
            return UniTask.CompletedTask;
        }

        public UniTask Update(float delta, SessionRegistry sessionRegistry, CancellationToken cancellationToken)
        {
            for (int i = 0; i < _inputBuffer.ActivatedLines.Length; i++)
            {
                HammerCubeComponent hammer = sessionRegistry.GameFieldComponent.HummerPoints[i];

                //it is intentional, we have "fixed frame rate", so we can just get right number and reduce math amount
                hammer.Transform.position = Vector3.Lerp(hammer.Transform.position, hammer.OriginalPosition, _hammerConfigs.HammerRelaxator);
            }

            for (int i = 0; i < sessionRegistry.SessionScreen.ColorButtons.Count; i++)
            {
                ColorButton button = sessionRegistry.SessionScreen.ColorButtons[i];
                
                button.Transform.sizeDelta = Vector2.Lerp(button.Transform.sizeDelta, button.OriginalScale, _hammerConfigs.UIButtonRelaxator);
            }
            
            return UniTask.CompletedTask;
        }

        public UniTask CleanUp(SessionRegistry sessionRegistry, CancellationToken cancellationToken)
        {
            return UniTask.CompletedTask;
        }
    }
}