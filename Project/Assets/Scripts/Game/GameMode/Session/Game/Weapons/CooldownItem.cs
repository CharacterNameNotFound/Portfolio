using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.Session.Game.Data;
using UnityEngine;

namespace Game.GameMode.Session.Game.Weapons
{
    public abstract class CooldownItem : ScriptableObject, IItem
    {
        public float MaxCooldown;
        
        protected float _currentCooldown;
        
        public virtual UniTask Initialize(CancellationToken cancellationToken)
        {
            _currentCooldown = MaxCooldown;
            return UniTask.CompletedTask;
        }

        public virtual UniTask OnObtained(SessionRegistry sessionRegistry, CancellationToken cancellationToken)
        {
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
    }
}