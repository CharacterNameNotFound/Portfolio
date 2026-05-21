using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Object = UnityEngine.Object;

namespace GameWideSystems.ScriptedVisualEffectManagement
{
    public class ScriptedVisualEffectManager : IScriptedVisualEffectManager
    {
        private Dictionary<Type, ScriptedVisualEffectPool> _pools = new();
        
        private Logger.Logger _logger;

        public ScriptedVisualEffectManager(Logger.Logger logger)
        {
            _logger = logger;
        }
        
        public UniTask RegisterEffect<T>(int preparedEffects, ScriptedVisualEffectPool scriptedVisualEffectPool, CancellationToken cancellationToken) where T : ScriptedVisualEffectBase
        {
            _pools[typeof(T)] = scriptedVisualEffectPool;
            
            if (preparedEffects <= 0)
            {
                return UniTask.CompletedTask;
            }

            return scriptedVisualEffectPool.ExtendBy(preparedEffects, cancellationToken);
        }

        public async UniTask Play<T>(ScriptedVisualEffectParams scriptedVisualParams, CancellationToken cancellationToken) where T : ScriptedVisualEffectBase
        {
            if (!_pools.TryGetValue(typeof(T), out ScriptedVisualEffectPool pool))
            {
                _logger.Warn($"No SVE of type {typeof(T).Name} registered");
                return;
            }

            ScriptedVisualEffectBase poolableEntity = await pool.GetObject(cancellationToken);
            T visualEffect = (T)poolableEntity;
            
            scriptedVisualParams.Manager = this;
            
            visualEffect.Play(scriptedVisualParams, cancellationToken).Forget();
        }
        
        public async UniTask<T> Get<T>(ScriptedVisualEffectParams scriptedVisualParams, CancellationToken cancellationToken) where T : ScriptedVisualEffectBase
        {
            if (!_pools.TryGetValue(typeof(T), out ScriptedVisualEffectPool pool))
            {
                _logger.Warn($"No SVE of type {typeof(T).Name} registered");
                return null;
            }

            ScriptedVisualEffectBase poolableEntity = await pool.GetObject(cancellationToken);
            T visualEffect = (T)poolableEntity;
            
            scriptedVisualParams.Manager = this;

            return visualEffect;
        }
        

        public void ReturnEffect<T>(T effect) where T : ScriptedVisualEffectBase
        {
            if (_pools.TryGetValue(typeof(T), out ScriptedVisualEffectPool pool))
            {
                pool.ReturnToPool(effect);
            }
            else
            {
                _logger.Warn($"No pool for SVE of type {typeof(T).Name}");
                Object.Destroy(effect);
            }
            
        }

        public void Unregister<T>()
        {
            _pools.Remove(typeof(T));
        }
    }
}