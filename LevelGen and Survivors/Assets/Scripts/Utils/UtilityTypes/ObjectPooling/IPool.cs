using System.Threading;
using Cysharp.Threading.Tasks;

namespace Utils.UtilityTypes.ObjectPooling
{
    public interface IPool<T> where T : IPoolableEntity
    {
        public int GetPooledCount();

        /// <summary>
        /// Adding additional pooled objects requires time, so 
        /// </summary>
        public UniTask ExtendBy(int count, CancellationToken cancellationToken);
        public void Trim(int count);
        public void Release(int count);
        public void ReleaseAll();
        public UniTask<T> GetObject(CancellationToken cancellationToken);
        public void ReturnToPool(T item);
    }
}