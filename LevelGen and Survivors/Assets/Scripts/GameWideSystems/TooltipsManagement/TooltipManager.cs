using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using GameWideSystems.Logger;
using GameWideSystems.TooltipsManagement;
using GameWideSystems.UIManagement;
using Utils.UtilityTypes.ObjectPooling;

namespace Game.UI.Tooltips
{
    public class TooltipManager : ITooltipManager
    {
        private const int DefaultTooltipEntitiesCount = 2;
        
        private Logger _logger;
        private IScreenHostProvider _screenHostProvider;
        private TooltipInputLayer _tooltipInputLayer;
        
        private Dictionary<TooltipType, TooltipPool> _tooltipPools;

        public TooltipManager(Logger logger, IScreenHostProvider screenHostProvider, TooltipInputLayer tooltipInputLayer)
        {
            _logger = logger;
            _screenHostProvider = screenHostProvider;
            _tooltipInputLayer = tooltipInputLayer;
            
            _tooltipPools = new Dictionary<TooltipType, TooltipPool>();
        }

        public UniTask RegisterTooltip(TooltipType tooltipType, TooltipPool tooltipObjectProvider, CancellationToken cancellationToken)
        {
            _tooltipPools[tooltipType] = tooltipObjectProvider;

            int pregenRequired = DefaultTooltipEntitiesCount - tooltipObjectProvider.GetPooledCount();

            if (pregenRequired <= 0)
            {
                return UniTask.CompletedTask;
            }

            return tooltipObjectProvider.ExtendBy(pregenRequired, cancellationToken);
        }
        
        public async UniTask<T> ShowTooltip<T>(TooltipType tooltipType, TooltipParams tooltipParams, CancellationToken cancellationToken) where T : TooltipBase
        {
            if (!_tooltipPools.TryGetValue(tooltipType, out TooltipPool pool))
            {
                _logger.Warn($"No tooltip of type {tooltipType.ToString()} registered");
                return null;
            }

            PoolableGameObject poolableEntity = await pool.GetObject(cancellationToken);
            ITooltip tooltip = (ITooltip)poolableEntity;

            _tooltipInputLayer.Register(tooltip);
            
            tooltipParams.TooltipManager = this;
            
            var transform = _screenHostProvider.Tooltips;
            poolableEntity.transform.SetParent(transform);
            await tooltip.Show(tooltipParams, cancellationToken);
            
            return (T) poolableEntity;
        }

        public async UniTask HideTooltip<T>(T tooltip, CancellationToken cancellationToken) where T : TooltipBase
        {
            _tooltipInputLayer.Unregister(tooltip);
            await tooltip.Hide(cancellationToken);
            _tooltipPools[tooltip.TooltipType].ReturnToPool(tooltip);
        }
    }
}