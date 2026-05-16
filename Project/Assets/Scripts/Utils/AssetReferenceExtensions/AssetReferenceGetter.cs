using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Utils.AssetReferenceExtensions
{
    public static class AssetReferenceGetter
    {
        public static UniTask<T> Get<T>(this AssetReferenceT<T> reference) where T : Object
        {
            if (reference.Asset)
            {
                return UniTask.FromResult<T>((T) reference.Asset);
            }

            return reference.LoadAssetAsync().ToUniTask();
        }
        
        public static T GetImmediate<T>(this AssetReferenceT<T> reference) where T : Object
        {
            if (reference.Asset)
            {
                return (T) reference.Asset;
            }

            return reference.LoadAssetAsync().WaitForCompletion();
        }
        
    }
}