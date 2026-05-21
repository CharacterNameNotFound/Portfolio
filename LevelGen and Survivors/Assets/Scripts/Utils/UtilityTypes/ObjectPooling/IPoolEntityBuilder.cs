using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace Utils.UtilityTypes.ObjectPooling
{
    public interface IPoolEntityBuilder<T>
    {
        public UniTask<T> Build(CancellationToken cancellationToken);
        public UniTask<IEnumerable<T>> Build(int count, CancellationToken cancellationToken);
    }
}