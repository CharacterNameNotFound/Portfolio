using UnityEngine;
using UnityEngine.AddressableAssets;
using Utils.UtilityTypes.ObjectPooling;

namespace Game.GameMode.Session.Game.Data.Enteties
{
    public class EnemyComponent : PoolableGameObject
    {
        public Transform Transform;
        public SpriteRenderer SpriteRenderer;
        
        [HideInInspector] public float Radius;
        [HideInInspector] public float Hp;
        [HideInInspector] public float Speed;
        [HideInInspector] public float Dps;
        
        [HideInInspector] public bool InPlayerRadius;

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