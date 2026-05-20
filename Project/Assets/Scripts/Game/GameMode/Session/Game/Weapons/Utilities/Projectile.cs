using UnityEngine.AddressableAssets;
using Utils.UtilityTypes.ObjectPooling;

namespace Game.GameMode.Session.Game.Weapons.Utilities
{
    public class Projectile : PoolableGameObject
    {
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