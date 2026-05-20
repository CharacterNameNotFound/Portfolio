using Game.GameMode.Session.Game.Utilities;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Utils.UtilityTypes.ObjectPooling;

namespace Game.GameMode.Session.Game.Data.Entities
{
    public class EnemyComponent : PoolableGameObject
    {
        public Transform Transform;
        public SpriteRenderer SpriteRenderer;
        
        [HideInInspector] public float Radius;
        [HideInInspector] public float Hp;
        [HideInInspector] public float Speed;
        [HideInInspector] public float Dps;
        [HideInInspector] public float Exp;
        
        [HideInInspector] public bool InPlayerRadius;
        [HideInInspector] public float SquareDistanceToPlayer;
        [HideInInspector] public int[] InteractedFrame = new int[GameConfigs.WeaponCount];

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