using System.Collections.Generic;
using Game.GameMode.Session.Gameplay.Entities;
using Utils.UtilityTypes.ObjectPooling;

namespace Game.GameMode.Session.Gameplay.Pools.CubePooling
{
    public class GameCubePool : GameObjectPool<GameCubeComponent>
    {
        public GameCubePool(List<GameCubeComponent> pool, IPoolEntityBuilder<GameCubeComponent> entityBuilder, IPooledObjectHostProvider pooledObjectHostProvider) : base(pool, entityBuilder, pooledObjectHostProvider)
        {
        }
    }
}