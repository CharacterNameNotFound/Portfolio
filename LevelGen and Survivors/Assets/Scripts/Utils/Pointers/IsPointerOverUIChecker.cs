using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Utils.Pointers
{
    public class IsPointerOverUIChecker
    {
        public static bool IsPointerOverUI(Vector2 position)
        {
            return UIObjectsUnderPointer(position).Count > 0;
        }

        private static List<RaycastResult> UIObjectsUnderPointer(Vector2 position)
        {
            
            PointerEventData pointerData = new(EventSystem.current)
            {
                position = position
            };

            List<RaycastResult> hits = new();
            
            EventSystem.current.RaycastAll(pointerData, hits);

            return hits;
        }
    }
}