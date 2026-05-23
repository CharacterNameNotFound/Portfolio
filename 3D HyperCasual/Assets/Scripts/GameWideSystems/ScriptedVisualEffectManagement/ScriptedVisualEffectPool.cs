using System.Collections.Generic;
using Utils.UtilityTypes.ObjectPooling;

namespace GameWideSystems.ScriptedVisualEffectManagement
{
    public class ScriptedVisualEffectPool : GameObjectPool<ScriptedVisualEffectBase>
    {
        public ScriptedVisualEffectPool(List<ScriptedVisualEffectBase> pool, IPoolEntityBuilder<ScriptedVisualEffectBase> entityBuilder, IPooledObjectHostProvider pooledObjectHostProvider) : base(pool, entityBuilder, pooledObjectHostProvider)
        {
        }
    }
}