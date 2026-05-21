using System.Collections.Generic;
using Game.UI.Tooltips;
using Utils.UtilityTypes.ObjectPooling;

namespace GameWideSystems.TooltipsManagement
{
    public class TooltipPool : GameObjectPool<TooltipBase>
    {
        public TooltipPool(List<TooltipBase> pool, IPoolEntityBuilder<TooltipBase> entityBuilder, IPooledObjectHostProvider pooledObjectHostProvider) : base(pool, entityBuilder, pooledObjectHostProvider)
        {
        }
    }
}