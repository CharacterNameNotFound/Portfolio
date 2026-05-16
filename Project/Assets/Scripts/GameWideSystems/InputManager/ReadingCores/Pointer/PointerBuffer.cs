using System;
using GameWideSystems.InputManager.GestureReaders.Pointer;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GameWideSystems.InputManager.ReadingCores.Pointer
{
    public class PointerBuffer
    {
        private readonly PointerReaderEventHost _pointerReaderEventHost;
        private readonly IPointerInputConfigurationsProvider _pointerInputConfigurations;
        private readonly MainInputs _mainInputs;

        private readonly float _convertedSwipeLengthThreshold;
        private readonly float _pointerStartTime;

        private Vector2 _currentPosition;
        private Vector2 _pointerStartPosition;
        private bool _isLongTapStarted = false;
        private bool _isSwipe = false;
        
        public PointerBuffer(PointerReaderEventHost pointerReaderEventHost, 
            IPointerInputConfigurationsProvider pointerInputConfigurations,
            MainInputs mainInputs)
        {
            _pointerReaderEventHost = pointerReaderEventHost;
            _pointerInputConfigurations = pointerInputConfigurations;
            _mainInputs = mainInputs;

            _pointerStartTime = Time.time;
            _convertedSwipeLengthThreshold = _pointerInputConfigurations.SwipeLengthThreshold * (Screen.dpi / 2.54f); // dpi to dpc
        }

        public void OnPress(InputAction.CallbackContext inputEvent)
        {
            _pointerStartPosition = ReadCurrentPosition();
            IGesture gesture = new Press(_pointerStartPosition);
            
            _pointerReaderEventHost.BroadcastGesture(gesture);
        }

        public void OnUpdate()
        {
            _currentPosition = ReadCurrentPosition();

            IGesture pressed = new Pressed(_pointerStartPosition, _currentPosition);
            _pointerReaderEventHost.BroadcastGesture(pressed);
            
            if (!_isSwipe && (_isLongTapStarted || IsLongTapThreshold()))
            {
                if (_isLongTapStarted)
                {
                    IGesture longTapUpdated = new LongPressUpdate(_pointerStartPosition, _currentPosition);
                    _pointerReaderEventHost.BroadcastGesture(longTapUpdated);
                    
                    return;
                }
                
                _isLongTapStarted = true;

                IGesture longTapStart = new LongPressStart(_currentPosition);
                _pointerReaderEventHost.BroadcastGesture(longTapStart);
                
                return;
            }

            if (_isSwipe || IsSwipeThreshold())
            {
                if (_isSwipe)
                {
                    IGesture swipeUpdate = new SwipeUpdate(_pointerStartPosition, _currentPosition);
                    _pointerReaderEventHost.BroadcastGesture(swipeUpdate);
                    
                    return;
                }
                
                Vector2 direction = _currentPosition - _pointerStartPosition;
                
                int x = Mathf.Abs(direction.x) >= Mathf.Abs(direction.y) ? Math.Sign(direction.x) : 0;
                int y = Mathf.Abs(direction.x) < Mathf.Abs(direction.y) ? Math.Sign(direction.y) : 0;
                
                Vector2Int intDirection = new Vector2Int(x, y);

                _isSwipe = true;
                
                IGesture swipe = new Swipe(_pointerStartPosition, direction, intDirection);
                
                _pointerReaderEventHost.BroadcastGesture(swipe);
                
                return;
            }

            
        }

        public void OnRelease(InputAction.CallbackContext inputEvent)
        {
            IGesture release = new Release(_pointerStartPosition, _currentPosition);
            _pointerReaderEventHost.BroadcastGesture(release);
            
            if (_isLongTapStarted)
            {
                IGesture longTapEnd = new LongPressEnd(_pointerStartPosition, ReadCurrentPosition());
                _pointerReaderEventHost.BroadcastGesture(longTapEnd);
            }

            if (_isSwipe)
            {
                IGesture swipe = new SwipeEnd(_pointerStartPosition, _currentPosition);
                _pointerReaderEventHost.BroadcastGesture(swipe);
            }

            if (_isSwipe || _isLongTapStarted)
            {
                _pointerReaderEventHost.BroadcastFinalization();
                return;
            }
            
            IGesture gesture = new Tap(ReadCurrentPosition());
            
            BroadcastAndFinalize(gesture);
        }

        private void BroadcastAndFinalize(IGesture gesture)
        {
            _pointerReaderEventHost.BroadcastGesture(gesture);
            _pointerReaderEventHost.BroadcastFinalization();
        }

        private Vector2 ReadCurrentPosition()
        {
            return _mainInputs.Pointer.PointerPosition.ReadValue<Vector2>();
        }

        private bool IsLongTapThreshold()
        {
            return Time.time - _pointerStartTime > _pointerInputConfigurations.TapToLongPressThreshold;
        }
        
        private bool IsSwipeThreshold()
        {
            return Time.time - _pointerStartTime < _pointerInputConfigurations.TapToLongPressThreshold && Vector2.Distance(_pointerStartPosition, _currentPosition) > _convertedSwipeLengthThreshold;
        }
        
    }
}