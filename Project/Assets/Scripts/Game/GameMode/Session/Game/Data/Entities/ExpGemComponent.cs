using UnityEngine;
using UnityEngine.AddressableAssets;
using Utils.UtilityTypes.ObjectPooling;

namespace Game.GameMode.Session.Game.Data.Entities
{
    public class ExpGemComponent : PoolableGameObject
    {
        public Transform Transform;
        
        public float Value;
        
        public override void OnPooled()
        {
            gameObject.SetActive(false);
        }

        public override void Dispose()
        {
            Addressables.ReleaseInstance(gameObject);
        }
    }
}