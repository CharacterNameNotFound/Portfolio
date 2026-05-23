using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.CustomUIElements.ButtonList
{
    public abstract class ButtonListElement<T> : MonoBehaviour where T : ButtonListElementData
    {
        [SerializeField] private Button Button;
        public event Action<T> OnPress;
        protected T ElementData;

        protected void Awake()
        {
            if (!Button)
            {
                Button = GetComponent<Button>();
            }
            
            Button.onClick.AddListener(() => OnPress?.Invoke(ElementData));
        }

        public void ResetPressed()
        {
            Button.interactable = true;
        }

        public void SetPressed()
        {
            Button.interactable = false;
        }
        
        protected void OnDestroy()
        {
            Button.onClick.RemoveAllListeners();
        }

        public virtual UniTask Load(T elementData, CancellationToken cancellationToken)
        {
            ElementData = elementData;
            
            gameObject.SetActive(true);
            
            return UniTask.CompletedTask;
        }
        
        
        
    }
}