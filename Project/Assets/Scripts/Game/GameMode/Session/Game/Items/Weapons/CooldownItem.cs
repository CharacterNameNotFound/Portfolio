using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.Session.Game.Data;
using MergetoolGui;
using UnityEngine;

namespace Game.GameMode.Session.Game.Items.Weapons
{
    public abstract class CooldownItem : ScriptableObject, IItem
    {
        [SerializeField] protected string _description;
        [SerializeField] protected string _upgradeDescription;
        [SerializeField] protected Sprite _itemSprite;
        
        public int MaxLevel;
        public float[] BaseCooldown;
        
        [HideInInspector] public int CurrentLevel;
        
        
        protected float _currentCooldown;

        public int GetLevel()
        {
            return CurrentLevel;
        }

        public int GetMaxLevel()
        {
            return MaxLevel;
        }

        public virtual UniTask Initialize(CancellationToken cancellationToken)
        {
            _currentCooldown = BaseCooldown[0];
            
            return UniTask.CompletedTask;
        }

        public virtual UniTask OnObtained(SessionRegistry sessionRegistry, CancellationToken cancellationToken)
        {
            sessionRegistry.ObtainedItems.Add(this);
            CurrentLevel = 1;

            return UniTask.CompletedTask;
        }

        public UniTask OnUpgrade(SessionRegistry sessionRegistry, CancellationToken cancellationToken)
        {
            CurrentLevel++;
            return UniTask.CompletedTask;
        }

        public virtual UniTask OnStatsUpdated(SessionRegistry sessionRegistry, CancellationToken cancellationToken)
        {
            return UniTask.CompletedTask;
        }

        public virtual UniTask UpdateInternal(float deltaTime, int itemIndex, SessionRegistry sessionRegistry, CancellationToken cancellationToken)
        {
            _currentCooldown -= deltaTime;
            
            return UniTask.CompletedTask;
        }
        
        public virtual void CleanUp()
        {
        }

        public Sprite GetItemImage()
        {
            return _itemSprite;
        }

        public string GetItemName()
        {
            return name;
        }

        public string GetObtainDescription()
        {
            return _description;
        }

        public string GetLevelUpDescription()
        {
            return _upgradeDescription;
        }

        protected void RestartCooldown(SessionRegistry sessionRegistry)
        {
            _currentCooldown = BaseCooldown[CurrentLevel - 1] * sessionRegistry.PlayerStats.CooldownModifier;
        }

    }
}