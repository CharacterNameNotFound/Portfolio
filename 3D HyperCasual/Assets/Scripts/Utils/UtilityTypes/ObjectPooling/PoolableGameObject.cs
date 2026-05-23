using UnityEngine;

namespace Utils.UtilityTypes.ObjectPooling
{
    public abstract class PoolableGameObject : MonoBehaviour, IPoolableEntity
    {
        public abstract void OnPooled();

        public abstract void Dispose();
    }
}