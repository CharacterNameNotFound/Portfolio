using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Utils.UtilityTypes.ObjectPooling;

namespace GameWideSystems.ScriptedVisualEffectManagement.FlyingParticleScriptedVisualEffect
{
    public class FlyingParticleScriptedVisualEffectRegisterer
    {
        private IPooledObjectHostProvider _pooledObjectHostProvider;
        private IScriptedVisualEffectManager _scriptedVisualEffectManager;
        private FlyingParticleScriptedVisualEffectRegistererConfigs _configs;
        
        public FlyingParticleScriptedVisualEffectRegisterer(
            IPooledObjectHostProvider pooledObjectHostProvider, 
            IScriptedVisualEffectManager scriptedVisualEffectManager, 
            FlyingParticleScriptedVisualEffectRegistererConfigs configs)
        {
            _pooledObjectHostProvider = pooledObjectHostProvider;
            _scriptedVisualEffectManager = scriptedVisualEffectManager;
            _configs = configs;
        }

        
        public UniTask Register(CancellationToken cancellationToken)
        {
            ScriptedVisualEffectPool pool = new ScriptedVisualEffectPool(new List<ScriptedVisualEffectBase>(),
                new AddressablePoolEntityProvider<ScriptedVisualEffectBase>(_configs.FlyingParticlesSprite),
                _pooledObjectHostProvider);

            
            return _scriptedVisualEffectManager.RegisterEffect<FlyingParticleScriptedVisualEffect>(_configs.FlyingParticlesPooledCount, pool, cancellationToken);
        }
    }
}