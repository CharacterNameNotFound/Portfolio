using UnityEngine;
using UnityEngine.AddressableAssets;
using Utils.UtilityTypes.ObjectPooling;

namespace Game.GameMode.Session.Game.Items.Utilities
{
    public class Projectile : PoolableGameObject
    {
        public Transform Transform;
        
        [HideInInspector] public Vector2 Direction;
        [HideInInspector] public float ProjectileTime;
        [HideInInspector] public int CreationFrame;
        
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