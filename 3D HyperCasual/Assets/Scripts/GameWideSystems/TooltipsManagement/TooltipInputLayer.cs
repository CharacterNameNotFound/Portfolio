using System.Collections.Generic;
using GameWideSystems.InputManager;
using GameWideSystems.InputManager.ReadingCores.Pointer;
using UnityEngine;

namespace GameWideSystems.TooltipsManagement
{
    public class TooltipInputLayer : IInputHandlerLayer
    {

        private PointerCore _pointerCore;
        
        // for now there will be only 1 tooltip at a time, but making groundwork for future with cascade tooltips
        // most likely it will require transition from list to stack but whatever for now 
        private List<ITooltip> _tooltips = new();
        private ITooltip[] _tooltipsBuffer = new ITooltip[16];
        
        
        public int Index => -2;
        public InputType InputType => InputType.Pointer;
        
        
        public TooltipInputLayer(PointerCore pointerCore)
        {
            _pointerCore = pointerCore;
        }
        
        
        public bool TryHandle(IGesture gesture)
        {
            if (_tooltips.Count == 0)
            {
                return false;
            }
            
            // ToDo: it is a little dirty, it make sense to make proper tooltips
            // we will result in closing tooltip through TryHandle action, so we going around original list, to prevent it modification
            for (int i = 0; i < _tooltips.Count; i++)
            {
                _tooltipsBuffer[i] = _tooltips[i];
            }

            bool result = false;
            
            int count = _tooltips.Count;
            for (int i = 0; i < count; i++)
            {
                if (_tooltipsBuffer[i].TryHandle(gesture))
                {
                    result = true;
                }
            }

            if (result)
            {
                // ToDo: i do not like this, buuuuut it seems okay enough
                _pointerCore.FinalizeGestureReading();
            }
            
            return result;
        }

        public void Register(ITooltip tooltip)
        {
            _tooltips.Add(tooltip);
        }

        public void Unregister(ITooltip tooltip)
        {
            _tooltips.Remove(tooltip);
        }

        public void Clear()
        {
            _tooltips.Clear();
        }
        
    }
}