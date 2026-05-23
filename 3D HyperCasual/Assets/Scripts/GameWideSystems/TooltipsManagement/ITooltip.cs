using System.Threading;
using Cysharp.Threading.Tasks;
using GameWideSystems.InputManager;

namespace GameWideSystems.TooltipsManagement
{
    public interface ITooltip
    {
        public TooltipType TooltipType { get; }
        public UniTask Show(TooltipParams tooltipParams, CancellationToken cancellationToken);
        public UniTask Hide(CancellationToken cancellationToken);
        public bool TryHandle(IGesture gesture);
    }
}