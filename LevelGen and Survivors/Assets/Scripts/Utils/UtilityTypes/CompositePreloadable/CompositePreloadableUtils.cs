using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Utils.UtilityTypes.CompositePreloadable
{
    public static class CompositePreloadableUtils
    {
        public static async UniTask<T> LoadComposite<T>(string assetAddress, CancellationToken cancellationToken) where T : Object, ICompositePreloadable
        {
            T asset = await Addressables.LoadAssetAsync<T>(assetAddress).ToUniTask(cancellationToken: cancellationToken);

            await asset.LoadComposite(cancellationToken);

            return asset;
        }
        
        public static async UniTask<T> LoadComposite<T>(this AssetReferenceT<T> assetReference, CancellationToken cancellationToken) where T : Object, ICompositePreloadable
        {
            await assetReference.LoadAssetAsync<T>().ToUniTask(cancellationToken: cancellationToken);

            await ((T)assetReference.Asset).LoadComposite(cancellationToken);
            
            return (T)assetReference.Asset;
        }
        
        public static void ReleaseComposite<T>(T item) where T : Object, ICompositePreloadable
        {
            item.UnloadComposite();

            Addressables.Release(item);
        }
        
        public static void ReleaseComposite<T>(this AssetReferenceT<T> assetReference) where T : Object, ICompositePreloadable
        {
            ((T)assetReference.Asset).UnloadComposite();
            
            assetReference.ReleaseAsset();
        }
        
    }
}