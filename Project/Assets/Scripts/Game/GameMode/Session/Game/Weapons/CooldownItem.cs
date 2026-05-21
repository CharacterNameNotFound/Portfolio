using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.Session.Game.Data;
using UnityEngine;

namespace Game.GameMode.Session.Game.Weapons
{
    public abstract class CooldownItem : ScriptableObject, IItem
    {
        public float BaseCooldown;
        [HideInInspector] public float MaxCooldown;
        [HideInInspector] public float CurrentMaxCooldown;
        
        protected float _currentCooldown;
        
        public virtual UniTask Initialize(CancellationToken cancellationToken)
        {
            MaxCooldown = BaseCooldown;
            _currentCooldown = MaxCooldown;
            return UniTask.CompletedTask;
        }

        public virtual UniTask OnObtained(SessionRegistry sessionRegistry, CancellationToken cancellationToken)
        {
            sessionRegistry.ObtainedItems.Add(this);

            return UniTask.CompletedTask;
        }

        public virtual UniTask OnStatsUpdated(SessionRegistry sessionRegistry, CancellationToken cancellationToken)
        {
            CurrentMaxCooldown = MaxCooldown * sessionRegistry.PlayerStats.CooldownModifier;
            return UniTask.CompletedTask;
        }

        public virtual UniTask UpdateInternal(float deltaTime, int itemIndex, SessionRegistry sessionRegistry, CancellationToken cancellationToken)
        {
            _currentCooldown -= deltaTime;
            
            return UniTask.CompletedTask;
        }
        
        public virtual UniTask CleanUp(CancellationToken cancellationToken)
        {
            return UniTask.CompletedTask;
        }

        protected void RestartCooldown()
        {
            _currentCooldown = CurrentMaxCooldown;
        }

    }
}