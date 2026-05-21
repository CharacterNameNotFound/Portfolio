using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.Session.Game.Data;
using Game.GameMode.Session.Game.Data.Entities;
using Game.GameMode.Session.Game.Items.Utilities;
using Game.GameMode.Session.Game.Utilities;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Utils.UtilityTypes.ObjectPooling;
using Random = UnityEngine.Random;

namespace Game.GameMode.Session.Game.Items.Weapons
{
    // it is copy paste of red ball by a lot. But this demo not about code design, so I let it be to save time
    public class SwordWeapon : CooldownItem
    {
        [SerializeField] private AssetReference _assetReference;
        [SerializeField] private int _pooledCount;
        [SerializeField] private AudioClip _sfx;

        
        [Header("Weapon stas")]
        [SerializeField] private float[] _damage;
        [SerializeField] private int _projectileCount;
        
        [SerializeField] private Vector2 _projectileExtends;
        [SerializeField] private float _projectileSpeed;
        [SerializeField] private float _projectileLifeTime;
        [SerializeField] private float _projectileShootingDelay;

        private Transform _poolHost;
            
        private GameObjectPool<Projectile> _projectilePool;
        private List<Projectile> _activeProjectiles;


        private float _baseScale;
        private int _projectilesShot;
        private float _shootingDelayLeft;
        

        public override async UniTask Initialize(CancellationToken cancellationToken)
        {
            await base.Initialize(cancellationToken);
            
            _poolHost = new GameObject("SwordPool").transform;
            
            List<Projectile> projectiles = new List<Projectile>();
            _projectilePool = new GameObjectPool<Projectile>(
                projectiles, 
                new AddressablePoolEntityProvider<Projectile>(_assetReference), 
                new AssignablePooledObjectHostProvider(_poolHost));

            _activeProjectiles = new List<Projectile>(_pooledCount);

            await _projectilePool.ExtendBy(_pooledCount, cancellationToken);
            Projectile projectile = await _projectilePool.GetObject(cancellationToken);
            _baseScale = projectile.transform.localScale.x;
        }

        public override async UniTask UpdateInternal(float deltaTime, int itemIndex, SessionRegistry sessionRegistry,
            CancellationToken cancellationToken)
        {
            await base.UpdateInternal(deltaTime, itemIndex, sessionRegistry, cancellationToken);
            UpdateProjectiles(itemIndex, deltaTime, sessionRegistry);
            
            if (_currentCooldown > 0)
            {
                return;
            }

            _shootingDelayLeft -= deltaTime;
            if (_shootingDelayLeft > 0)
            {
                return;
            }

            await CreateProjectile(sessionRegistry, cancellationToken);

            if (_projectilesShot >= _projectileCount + sessionRegistry.PlayerStats.ProjectileCount)
            {
                _projectilesShot = 0;
                RestartCooldown(sessionRegistry);
            }
        }

        public override void CleanUp()
        {
            base.CleanUp();

            foreach (Projectile projectile in _activeProjectiles)
            {
                projectile.Dispose();
            }

            _activeProjectiles = null;
            
            _projectilePool.ReleaseAll();
            
        }

        private async UniTask CreateProjectile(SessionRegistry sessionRegistry, CancellationToken cancellationToken)
        {
            Projectile projectile = await _projectilePool.GetObject(cancellationToken);
            
            _activeProjectiles.Add(projectile);
            projectile.gameObject.SetActive(true);
            
            float scale = _baseScale * sessionRegistry.PlayerStats.RadiusModifier;
            projectile.Transform.localScale = new Vector3(scale, scale, scale);
            projectile.Transform.position = sessionRegistry.PlayerCharacterComponent.Transform.position;
            
            if (sessionRegistry.Enemies.Count > 0)
            {
                float distance = float.MaxValue;
                EnemyComponent enemyComponent = sessionRegistry.Enemies[0];

                for (int i = 0; i < sessionRegistry.Enemies.Count; i++)
                {
                    if (distance < sessionRegistry.Enemies[i].SquareDistanceToPlayer)
                    {
                        continue;
                    }

                    distance = sessionRegistry.Enemies[i].SquareDistanceToPlayer;
                    enemyComponent = sessionRegistry.Enemies[i];
                }

                projectile.Direction = enemyComponent.Transform.position -
                                       sessionRegistry.PlayerCharacterComponent.Transform.position;
            }
            else
            {
                projectile.Direction = Random.onUnitCircle;
            }

            projectile.Direction.Normalize();
            projectile.ProjectileTime = _projectileLifeTime;
            projectile.Transform.right = projectile.Direction;
            
            projectile.CreationFrame = Time.frameCount;
            sessionRegistry.AudioManager.PlaySFX(_sfx, cancellationToken).Forget();

            _shootingDelayLeft = _projectileShootingDelay;
            _projectilesShot++;
        }

        private void UpdateProjectiles(int itemIndex, float deltaTime, SessionRegistry sessionRegistry)
        {
            Vector2 projectileRadius = _projectileExtends * sessionRegistry.PlayerStats.RadiusModifier;
            float damage = _damage[CurrentLevel - 1] * sessionRegistry.PlayerStats.DamageModifier;
            
            for (int i = 0; i < _activeProjectiles.Count; i++)
            {
                _activeProjectiles[i].Transform.position += (Vector3) _activeProjectiles[i].Direction * (deltaTime * _projectileSpeed);

                _activeProjectiles[i].ProjectileTime -= deltaTime;
                
                ProcessCollision(_activeProjectiles[i], sessionRegistry, projectileRadius, damage, itemIndex);
                
                if (_activeProjectiles[i].ProjectileTime > 0)
                {
                    continue;
                }

                _projectilePool.ReturnToPool(_activeProjectiles[i]);
                _activeProjectiles.RemoveAt(i);
                i--;
            }
            
            
        }

        private bool ProcessCollision(Projectile projectile, SessionRegistry sessionRegistry, Vector2 projectileExtends, float damage, int itemIndex)
        {
            Vector3 projectilePosition = projectile.Transform.position;
            float rotationRad = projectile.Transform.rotation.z * Mathf.Deg2Rad;
            bool result = false;
            
            foreach (EnemyComponent enemy in sessionRegistry.Enemies)
            {
                if (enemy.InteractedFrame[itemIndex] >= projectile.CreationFrame)
                {
                    continue;
                }
                
                if (!CollisionChecks.IsCircleRotatedBoxCollided(enemy.Transform.position, enemy.Radius, projectilePosition, projectileExtends, rotationRad))
                {
                    continue;
                }

                enemy.InteractedFrame[itemIndex] = projectile.CreationFrame;
                enemy.Hp -= damage;
                result = true;
            }

            return result;
        }
    }
}