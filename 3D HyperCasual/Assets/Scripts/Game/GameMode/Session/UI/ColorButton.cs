using UnityEngine;

namespace Game.GameMode.Session.UI
{
    public class ColorButton : MonoBehaviour
    {
        public RectTransform Transform;
        
        [HideInInspector] public bool Pressed;
        [HideInInspector] public Vector2 OriginalScale;
        
        public void SetPressed()
        {
            Pressed = true;
        }
    }
}