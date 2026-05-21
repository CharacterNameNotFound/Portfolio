using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;
using Utils.UtilityTypes.AssetReferencing;

namespace Utils.UtilityTypes.ObjectPooling
{
    public class AddressablePoolEntityProvider<T> : IPoolEntityBuilder<T> where T : Component
    {
        private AssetReference _assetReference;
        
        public AddressablePoolEntityProvider(AssetReference assetReference)
        {
            _assetReference = assetReference;
        }
        
        public UniTask<T> Build(CancellationToken cancellationToken)
        {
            return _assetReference.Instantiate<T>(new InstantiationParameters(), cancellationToken);
        }

        public async UniTask<IEnumerable<T>> Build(int count, CancellationToken cancellationToken)
        {
            List<UniTask<T>> tasks = new List<UniTask<T>>();
            
            for (int i = 0; i < count; i++)
            {
                tasks.Add(_assetReference.Instantiate<T>(new InstantiationParameters(), cancellationToken));
            }

            T[] results = await tasks;
            
            return results;
        }
    }
}