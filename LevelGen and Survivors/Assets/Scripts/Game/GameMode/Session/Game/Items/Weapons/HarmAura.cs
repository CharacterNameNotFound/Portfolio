using System.Globalization;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.Session.Game.Data;
using Game.GameMode.Session.Game.Data.Entities;
using Game.GameMode.Session.Game.Utilities;
using GameWideSystems.ScriptedVisualEffectManagement.FlyingTextScriptedVisualEffects;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;
using Utils.UtilityTypes.AssetReferencing;

namespace Game.GameMode.Session.Game.Items.Weapons
{
    public class HarmAura : CooldownItem
    {
        [SerializeField] private AssetReferenceGameObject _auraPrefab;
        
        public float[] Damage;
        public float[] Radius;

        private GameObject _auraInstance;
        private float _baseScale;
        
        public override async UniTask Initialize(CancellationToken cancellationToken)
        {
            await base.Initialize(cancellationToken);
            _auraInstance = await _auraPrefab.Instantiate(new InstantiationParameters(), cancellationToken);
            _auraInstance.SetActive(false);
            _baseScale = _auraInstance.transform.localScale.x;
        }

        public override UniTask OnObtained(SessionRegistry sessionRegistry, CancellationToken cancellationToken)
        {
            _auraInstance.transform.SetParent(sessionRegistry.PlayerCharacterComponent.Transform, false);
            _auraInstance.gameObject.SetActive(true);
            
            float scale = _baseScale * sessionRegistry.PlayerStats.RadiusModifier;
            _auraInstance.transform.localScale = new Vector3(scale, scale, scale);
            
            return base.OnObtained(sessionRegistry, cancellationToken);
        }

        public override async UniTask OnStatsUpdated(SessionRegistry sessionRegistry, CancellationToken cancellationToken)
        {
            await base.OnStatsUpdated(sessionRegistry, cancellationToken);

            float baseScaleMod = Radius[CurrentLevel] / Radius[0];
            
            float scale = _baseScale * baseScaleMod * sessionRegistry.PlayerStats.RadiusModifier;
            _auraInstance.transform.localScale = new Vector3(scale, scale, scale);
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

            float damage = Damage[CurrentLevel - 1] * sessionRegistry.PlayerStats.DamageModifier;
            float radius = Radius[CurrentLevel - 1] * sessionRegistry.PlayerStats.RadiusModifier;
            
            foreach (EnemyComponent enemy in sessionRegistry.Enemies)
            {
                if (!CollisionChecks.IsCirclesCollided(characterPosition, enemy.Transform.position, radius, enemy.Radius))
                {
                    continue;
                }

                sessionRegistry.ScriptedVisualEffectManager.Play<FlyingTextScriptedVisualEffect>(
                    new FlyingTextScriptedVisualEffectParams(
                        null, 
                        enemy.Transform.position, 
                        enemy.Transform.position + Vector3.up, 
                        0.5f, 
                        0.5f, 
                        0.5f, 
                        damage.ToString(CultureInfo.InvariantCulture), 
                        1f), 
                    cancellationToken);
                
                enemy.Hp -= damage;
            }

            RestartCooldown(sessionRegistry);
        }

        public override void CleanUp()
        {
            base.CleanUp();
            
            _auraInstance.transform.SetParent(null);
            Addressables.ReleaseInstance(_auraInstance);
        }
        
    }
}