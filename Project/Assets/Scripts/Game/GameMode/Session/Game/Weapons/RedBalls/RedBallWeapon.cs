using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.Session.Game.Data;
using Game.GameMode.Session.Game.Weapons.Utilities;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Utils.UtilityTypes.ObjectPooling;

namespace Game.GameMode.Session.Game.Weapons.RedBalls
{
    public class RedBallWeapon : CooldownItem
    {
        [SerializeField] private AssetReference _assetReference;
        [SerializeField] private int _pooledCount;
        
        private GameObjectPool<Projectile> _projectilePool;

        private IPooledObjectHostProvider _hostProvider;

        public RedBallWeapon(IPooledObjectHostProvider hostProvider)
        {
            _hostProvider = hostProvider;
        }

        public override UniTask Initialize(CancellationToken cancellationToken)
        {
            List<Projectile> projectiles = new List<Projectile>();
            _projectilePool = new GameObjectPool<Projectile>(
                projectiles, 
                new AddressablePoolEntityProvider<Projectile>(_assetReference), 
                _hostProvider);

            return _projectilePool.ExtendBy(_pooledCount, cancellationToken);
        }

        public override async UniTask UpdateInternal(float deltaTime, int itemIndex, SessionRegistry sessionRegistry,
            CancellationToken cancellationToken)
        {
            await base.UpdateInternal(deltaTime, itemIndex, sessionRegistry, cancellationToken);

            if (_currentCooldown > 0)
            {
                return;
            }
            
            
        }
        
        
    }
    
}