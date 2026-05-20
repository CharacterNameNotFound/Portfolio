using System.Collections.Generic;
using Game.GameMode.Session.Game.Data.Entities;
using Utils.UtilityTypes.ObjectPooling;

namespace Game.GameMode.Session.Pools.ExperiencePool
{
    public class ExpGemPool : GameObjectPool<ExpGemComponent>
    {
        public ExpGemPool(List<ExpGemComponent> pool, IPoolEntityBuilder<ExpGemComponent> entityBuilder, IPooledObjectHostProvider pooledObjectHostProvider) : base(pool, entityBuilder, pooledObjectHostProvider)
        {
        }
    }
}