using System.Collections.Generic;
using Game.GameMode.Session.Gameplay.Entities;
using Utils.UtilityTypes.ObjectPooling;

namespace Game.GameMode.Session.Gameplay.Pools.PoppedCubeParticlePooling
{
    public class PoppedCubeParticlePool : GameObjectPool<PoppedParticlesComponent>
    {
        public PoppedCubeParticlePool(List<PoppedParticlesComponent> pool, IPoolEntityBuilder<PoppedParticlesComponent> entityBuilder, IPooledObjectHostProvider pooledObjectHostProvider) : base(pool, entityBuilder, pooledObjectHostProvider)
        {
        }
    }
}