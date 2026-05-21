using System.Threading;
using Cysharp.Threading.Tasks;

namespace Utils.UtilityTypes.LifeCycle
{
    public interface IInitializableGameObject
    {
        public UniTask Initialize(CancellationToken cancellationToken);
    }
}