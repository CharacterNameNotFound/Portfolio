using UnityEngine;

namespace GameWideSystems.TooltipsManagement
{
    public class TooltipParams
    {
        public Vector2 Pivot;
        
        // Set by tooltip manager
        public ITooltipManager TooltipManager;

        public TooltipParams(Vector2 pivot)
        {
            Pivot = pivot;
        }
    }
}