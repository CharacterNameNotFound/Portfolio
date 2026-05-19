using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Utils.UtilityTypes.ObjectPooling
{
    public class GameObjectPool<T> : Pool<T> where T : Component, IPoolableEntity
    {
        private IPooledObjectHostProvider _pooledObjectHostProvider;
        
        public GameObjectPool(List<T> pool, IPoolEntityBuilder<T> entityBuilder, IPooledObjectHostProvider pooledObjectHostProvider) : base(pool, entityBuilder)
        {
            _pooledObjectHostProvider = pooledObjectHostProvider;
        }

        public override async UniTask ExtendBy(int count, CancellationToken cancellationToken)
        {
            await base.ExtendBy(count, cancellationToken);
            
            Transform parent = _pooledObjectHostProvider.GetHost();
            
            for (int i = 0; i < count; i++)
            {
                _pool[_pool.Count - 1 - i].transform.SetParent(parent);
                _pool[_pool.Count - 1 - i].gameObject.SetActive(false);
            }
        }

        public override void ReturnToPool(T item)
        {
            item.transform.SetParent(_pooledObjectHostProvider.GetHost());
            item.gameObject.SetActive(false);
            base.ReturnToPool(item);
        }
    }
}