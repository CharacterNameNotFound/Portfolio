using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Utils.UtilityTypes.ObjectPooling;

namespace GameWideSystems.ScriptedVisualEffectManagement
{
    public abstract class ScriptedVisualEffectBase : MonoBehaviour, IPoolableEntity
    {
        public abstract UniTask Play(ScriptedVisualEffectParams parameters, CancellationToken cancellationToken);
        
        public void OnPooled()
        {
            gameObject.SetActive(false);
        }

        public abstract void Dispose();
    }
}