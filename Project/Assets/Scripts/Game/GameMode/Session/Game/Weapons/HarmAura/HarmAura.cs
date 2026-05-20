using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.Session.Game.Data;
using Game.GameMode.Session.Game.Data.Entities;
using Game.GameMode.Session.Game.Utilities;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;
using Utils.UtilityTypes.AssetReferencing;

namespace Game.GameMode.Session.Game.Weapons.HarmAura
{
    public class HarmAura : CooldownItem
    {
        [SerializeField] private AssetReferenceGameObject _auraPrefab;
        
        public float Damage;
        public float Radius;

        private GameObject _auraInstance;
        
        public override async UniTask Initialize(CancellationToken cancellationToken)
        {
            await base.Initialize(cancellationToken);
            _auraInstance = await _auraPrefab.Instantiate(new InstantiationParameters(), cancellationToken);
            _auraInstance.SetActive(false);
        }

        public override UniTask OnObtained(SessionRegistry sessionRegistry, CancellationToken cancellationToken)
        {
            sessionRegistry.ObtainedItems.Add(this);

            _auraInstance.transform.SetParent(sessionRegistry.PlayerCharacterComponent.Transform, false);
            _auraInstance.gameObject.SetActive(true);
            
            return base.OnObtained(sessionRegistry, cancellationToken);
        }

        public override async UniTask UpdateInternal(float deltaTime, int itemIndex, SessionRegistry sessionRegistry,
            CancellationToken cancellationToken)
        {
            await base.UpdateInternal(deltaTime, itemIndex, sessionRegistry, cancellationToken);

            if (_currentCooldown > 0)
            {
                return;
            }
            
            Vector3 characterPosition = sessionRegistry.PlayerCharacterComponent.Transform.position;

            foreach (EnemyComponent enemy in sessionRegistry.Enemies)
            {
                if (!CollisionChecks.IsCirclesCollided(characterPosition, enemy.Transform.position, Radius, enemy.Radius))
                {
                    continue;
                }

                enemy.Hp -= Damage;
            }

            _currentCooldown = MaxCooldown;
        }
        
        
        
    }
}