using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using GameWideSystems.InputManager;
using GameWideSystems.InputManager.GestureReaders.Pointer;
using GameWideSystems.UIManagement;
using UnityEngine;
using UnityEngine.ResourceManagement.ResourceProviders;
using Utils.UtilityTypes.AssetReferencing;
using Utils.UtilityTypes.Counters;

namespace Game.GameMode.Session.Inputs
{
    public class ScreenJoystickInputLayer : IInputHandlerLayer
    {
        public int Index => 10;
        public InputType InputType => InputType.Pointer;

        private ScreenJoystickLayerConfigs _screenJoystickLayerConfigs;
        private IScreenHostProvider _screenHostProvider;

        private JoystickView _joystickView;
        private CounterLock _counterLock = new(true);
        private Vector2 _sourcePosition;

        public ScreenJoystickInputLayer(ScreenJoystickLayerConfigs screenJoystickLayerConfigs, IScreenHostProvider screenHostProvider)
        {
            _screenJoystickLayerConfigs = screenJoystickLayerConfigs;
            _screenHostProvider = screenHostProvider;
        }

        public async UniTask Initialize(CancellationToken cancellationToken)
        {
            _joystickView = await _screenJoystickLayerConfigs.JoystickView
                .Instantiate<JoystickView>(
                    new InstantiationParameters(_screenHostProvider.SystemHost, false), 
                    cancellationToken);
            
            _joystickView.gameObject.SetActive(false);
        }
        
        public bool TryHandle(IGesture gesture)
        {
            if (_counterLock.IsLocked())
            {
                return false;
            }

            if (gesture is Release release)
            {
                _joystickView.Hide();
                return true;
            }
            
            if (gesture is Pressed pressed)
            {
                _joystickView.UpdatePosition(pressed.CurrentPosition);
                return true;
            }
            
            if (gesture is Press press)
            {
                
                _joystickView.Show(press.SourcePosition);
                _sourcePosition = press.SourcePosition;
                return true;
            }
            
            return true;
        }

        public void SetActive(bool newState)
        {
            _counterLock.Toggle(newState);
        }

    }
}