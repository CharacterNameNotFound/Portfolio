using System.Threading;
using Cysharp.Threading.Tasks;
using Game.UI.Tooltips;
using Utils.UtilityTypes.ObjectPooling;

namespace GameWideSystems.TooltipsManagement
{
    public interface ITooltipManager
    {
        public UniTask RegisterTooltip(TooltipType tooltipType, TooltipPool tooltipObjectProvider, CancellationToken cancellationToken);
        public UniTask<T> ShowTooltip<T>(TooltipType tooltipType, TooltipParams tooltipParams, CancellationToken cancellationToken) where T : TooltipBase;
        public UniTask HideTooltip<T>(T tooltip, CancellationToken cancellationToken) where T : TooltipBase;
    }
}