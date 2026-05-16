using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Utils.UtilityTypes.ObjectPooling
{
    public class Pool<T> : IPool<T> where T : IPoolableEntity
    {
        protected readonly List<T> _pool;
        private IPoolEntityBuilder<T> _entityBuilder;

        public Pool(List<T> pool, IPoolEntityBuilder<T> entityBuilder)
        {
            _pool = pool;
            _entityBuilder = entityBuilder;
        }

        public int GetPooledCount()
        {
            return _pool.Count;
        }
        
        public virtual async UniTask ExtendBy(int count, CancellationToken cancellationToken)
        {
            IEnumerable<T> poolableEntities = await _entityBuilder.Build(count, cancellationToken);
            _pool.AddRange(poolableEntities);
        }

        public void Trim(int trimTo)
        {
            int trimCount = _pool.Count - trimTo;

            int rangeStart = _pool.Count - 1;
            for (int i = 0; i < trimCount; i++)
            {
                T poolableEntity = _pool[rangeStart - i];
                poolableEntity.Dispose();
            }
            
            _pool.RemoveRange(rangeStart, trimCount);
        }

        public void Release(int count)
        {
            count = Mathf.Min(count, _pool.Count);

            for (int i = _pool.Count - count; i < _pool.Count; i++)
            {
                _pool[i].Dispose();
            }
            
            _pool.RemoveRange(_pool.Count - count, count);
        }

        public void ReleaseAll()
        {
            foreach (T item in _pool)
            {
                item.Dispose();
            }
            _pool.Clear();
        }

        public async UniTask<T> GetObject(CancellationToken cancellationToken)
        {
            T item;
            
            if (_pool.Count == 0)
            {
                item = await _entityBuilder.Build(cancellationToken);
            }
            else
            {
                int index = _pool.Count - 1;
                item = _pool[index];
                _pool.RemoveAt(index);
            }

            return item;
        }

        public virtual void ReturnToPool(T item)
        {
            item.OnPooled();
            _pool.Add(item);
        }
    }
}