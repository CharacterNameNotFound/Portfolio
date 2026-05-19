using System.Collections.Generic;
using Game.GameMode.Session.Game.Data.Enteties;
using Utils.UtilityTypes.ObjectPooling;

namespace Game.GameMode.Session.Pools.EnemyBuilding
{
    public class EnemyPool : GameObjectPool<EnemyComponent>
    {
        public EnemyPool(List<EnemyComponent> pool, IPoolEntityBuilder<EnemyComponent> entityBuilder, IPooledObjectHostProvider pooledObjectHostProvider) : base(pool, entityBuilder, pooledObjectHostProvider)
        {
        }
    }
}