using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game.UI.CustomUIElements.ButtonList
{
    public class ButtonListBase<TElement, TElementData> : MonoBehaviour where TElement : ButtonListElement<TElementData> where TElementData : ButtonListElementData
    {
        public event Action<TElementData> OnItemSelected; 
        
        [SerializeField] private Transform _buttonHost;
        [SerializeField] private TElement _prefab;

        private int _pressed = -1;
        private List<TElement> _existingElements = new();
        
        // ToDo (low): Update with endless list, last request cancellation
        public async UniTask UpdateList(List<TElementData> elements, CancellationToken cancellationToken)
        {
            ResetPressed();
            int spawnedRequired = elements.Count - _existingElements.Count;

            for (int i = 0; i < spawnedRequired; i++)
            {
                TElement buttonListElement = Instantiate(_prefab, _buttonHost);
                _existingElements.Add(buttonListElement);
                buttonListElement.OnPress += FunnelEvent;
            }

            List<UniTask> buttonLoading = new();
            
            for (int i = 0; i < elements.Count; i++)
            {
                elements[i].ButtonIndex = i;
                UniTask button = _existingElements[i].Load(elements[i], cancellationToken);
                buttonLoading.Add(button);
            }

            for (int i = elements.Count; i < _existingElements.Count; i++)
            {
                _existingElements[i].gameObject.SetActive(false);
            }

            await buttonLoading;
        }

        public void ResetPressed()
        {
            if (_pressed >= 0)
            {
                _existingElements[_pressed].ResetPressed();
            }

            _pressed = -1;
        }
        
        public void SwapPressed(int index)
        {
            if (_pressed >= 0)
            {
                _existingElements[_pressed].ResetPressed();
            }

            _existingElements[index].SetPressed();
            _pressed = index;
        }

        private void OnDestroy()
        {
            foreach (TElement item in _existingElements)
            {
                item.OnPress -= FunnelEvent;
            }
        }

        private void FunnelEvent(TElementData element)
        {
            OnItemSelected?.Invoke(element);
        }
    }
}