using System.Threading;
using Cysharp.Threading.Tasks;

namespace Utils.UtilityTypes.CompositePreloadable
{
    // Sometimes we will need to preload all items inside of data structure before usage 
    public interface ICompositePreloadable
    {
        public bool IsLoaded { get; }
        public UniTask LoadComposite(CancellationToken cancellationToken);
        public void UnloadComposite();
    }
}