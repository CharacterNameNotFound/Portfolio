using UnityEngine;

namespace Game.GameMode.Session.Inputs
{
    public class JoystickView : MonoBehaviour
    {
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private RectTransform _innerTransform;

        private Vector2 _pivot;
        public float Radius;
        
        public void Show(Vector2 screenPoint)
        {
            _pivot = screenPoint;
            Radius = _innerTransform.rect.width;
            _rectTransform.position = screenPoint;
            gameObject.SetActive(true);
        }

        public void UpdatePosition(Vector2 screenPoint)
        {
            if (Vector2.Distance(screenPoint, _pivot) > Radius)
            {
                screenPoint = _pivot + (screenPoint - _pivot).normalized * Radius;
            }
            
            _innerTransform.position = screenPoint;
        }
        
        public void Hide()
        {
            gameObject.SetActive(false);
        }
        

    }
}