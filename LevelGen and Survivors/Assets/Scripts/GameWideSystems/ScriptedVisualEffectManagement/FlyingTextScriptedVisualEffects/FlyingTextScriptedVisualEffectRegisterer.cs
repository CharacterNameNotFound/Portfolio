using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Utils.UtilityTypes.ObjectPooling;

namespace GameWideSystems.ScriptedVisualEffectManagement.FlyingTextScriptedVisualEffects
{
    public class FlyingTextScriptedVisualEffectRegisterer
    {
        private FlyingTextScriptedVisualEffectConfigsProvider _scriptedVisualEffectConfigsProvider;
        private IScriptedVisualEffectManager _scriptedVisualEffectManager;
        private IPooledObjectHostProvider _pooledObjectGenericHostProvider;

        public FlyingTextScriptedVisualEffectRegisterer(
            IScriptedVisualEffectManager scriptedVisualEffectManager, 
            FlyingTextScriptedVisualEffectConfigsProvider scriptedVisualEffectConfigsProvider, 
            IPooledObjectHostProvider pooledObjectGenericHostProvider)
        {
            _scriptedVisualEffectManager = scriptedVisualEffectManager;
            _scriptedVisualEffectConfigsProvider = scriptedVisualEffectConfigsProvider;
            _pooledObjectGenericHostProvider = pooledObjectGenericHostProvider;
        }

        public UniTask Register(CancellationToken cancellationToken)
        {
            var pool = new ScriptedVisualEffectPool(
                new List<ScriptedVisualEffectBase>(_scriptedVisualEffectConfigsProvider.PrebuildCount), 
                new AddressablePoolEntityProvider<ScriptedVisualEffectBase>(_scriptedVisualEffectConfigsProvider.FlyingTextScriptedVisualEffectInstance),
                _pooledObjectGenericHostProvider);
            
            return _scriptedVisualEffectManager.RegisterEffect<FlyingTextScriptedVisualEffect>(
                _scriptedVisualEffectConfigsProvider.PrebuildCount,
                pool,
                cancellationToken);
        }

        public void Unregister()
        {
            _scriptedVisualEffectManager.Unregister<ScriptedVisualEffectBase>();
        }
        
    }
}