using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.Session.Game.Data;
using UnityEngine;

namespace Game.GameMode.Session.Game.Items.BuffItem
{
    public class PassiveItem : ScriptableObject, IItem
    {
        [SerializeField] protected string Description;
        [SerializeField] protected string LevelUpDescription;
        [SerializeField] protected Sprite _itemSprite;

        
        public int MaxLevel;
        
        [HideInInspector] public int CurrentLevel;

        public int GetLevel()
        {
            return CurrentLevel;
        }

        public int GetMaxLevel()
        {
            return MaxLevel;
        }

        public UniTask Initialize(CancellationToken cancellationToken)
        {
            CurrentLevel = 0;
            return UniTask.CompletedTask;
        }

        public virtual UniTask OnObtained(SessionRegistry sessionRegistry, CancellationToken cancellationToken)
        {
            CurrentLevel = 1;
            sessionRegistry.ObtainedItems.Add(this);
            return UniTask.CompletedTask;
        }

        public virtual UniTask OnUpgrade(SessionRegistry sessionRegistry, CancellationToken cancellationToken)
        {
            CurrentLevel++;
            return UniTask.CompletedTask;
        }

        public UniTask OnStatsUpdated(SessionRegistry sessionRegistry, CancellationToken cancellationToken)
        {
            return UniTask.CompletedTask;
        }

        public UniTask UpdateInternal(float deltaTime, int itemIndex, SessionRegistry sessionRegistry,
            CancellationToken cancellationToken)
        {
            return UniTask.CompletedTask;
        }

        public void CleanUp()
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
            return Description;
        }

        public string GetLevelUpDescription()
        {
            return LevelUpDescription;
        }
    }
}