using System.Threading;
using Cysharp.Threading.Tasks;
using GameWideSystems.InputManager;
using GameWideSystems.InputManager.GestureReaders.Pointer;
using GameWideSystems.TooltipsManagement;
using UnityEngine;
using Utils.UtilityTypes.ObjectPooling;

namespace Game.UI.Tooltips
{
    public abstract class TooltipBase : PoolableGameObject, ITooltip
    {
        public abstract TooltipType TooltipType { get; }
        public abstract UniTask Hide(CancellationToken cancellationToken);
        public abstract override void Dispose();

        [SerializeField] protected RectTransform Body;
        
        protected TooltipParams _tooltipParams;

        
        public override void OnPooled()
        {
            gameObject.SetActive(false);
        }

        public virtual UniTask Show(TooltipParams tooltipParams, CancellationToken cancellationToken)
        {
            _tooltipParams = tooltipParams;
            
            float pivotX = tooltipParams.Pivot.x / Screen.width;
            float pivotY = tooltipParams.Pivot.y / Screen.height;

            GetComponent<RectTransform>().pivot = new Vector2(pivotX, pivotY);
            transform.position = tooltipParams.Pivot;

            return UniTask.CompletedTask;
        }
        
        public bool TryHandle(IGesture gesture)
        {
            _tooltipParams.TooltipManager.HideTooltip(this, Application.exitCancellationToken);
            
            if (gesture is IPointerGesture pointerGesture)
            {
                return RectTransformUtility.RectangleContainsScreenPoint(Body, pointerGesture.SourcePosition);
            }
            
            return true;
        }
    }
}