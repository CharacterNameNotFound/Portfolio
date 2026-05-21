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
        private JoystickBuffer _joystickBuffer;
        private CounterLock _counterLock = new(true);
        private Vector2 _sourcePosition;

        public ScreenJoystickInputLayer(
            ScreenJoystickLayerConfigs screenJoystickLayerConfigs, 
            IScreenHostProvider screenHostProvider, 
            JoystickBuffer joystickBuffer)
        {
            _screenJoystickLayerConfigs = screenJoystickLayerConfigs;
            _screenHostProvider = screenHostProvider;
            _joystickBuffer = joystickBuffer;
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
                _joystickBuffer.Joystick = Vector2.zero;
                _joystickView.Hide();
                return true;
            }
            
            if (gesture is Pressed pressed)
            {
                _joystickView.UpdatePosition(pressed.CurrentPosition);

                Vector2 inputRadiusVector = pressed.CurrentPosition - _sourcePosition;
                
                float joystickViewRadiusLength = inputRadiusVector.magnitude / _joystickView.Radius;
                joystickViewRadiusLength = Mathf.Clamp01(joystickViewRadiusLength);

                _joystickBuffer.Joystick = inputRadiusVector.normalized * joystickViewRadiusLength;
                
                return true;
            }
            
            if (gesture is Press press)
            {
                _joystickView.Show(press.SourcePosition);
                _sourcePosition = press.SourcePosition;

                _joystickBuffer.Joystick = Vector3.zero;
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