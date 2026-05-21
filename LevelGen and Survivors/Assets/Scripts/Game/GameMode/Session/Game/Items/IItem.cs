using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.Session.Game.Data;
using UnityEngine;

namespace Game.GameMode.Session.Game.Items
{
    public interface IItem
    {
        public int GetLevel();
        public int GetMaxLevel();
        public UniTask Initialize(CancellationToken cancellationToken);
        public UniTask OnObtained(SessionRegistry sessionRegistry, CancellationToken cancellationToken);
        public UniTask OnUpgrade(SessionRegistry sessionRegistry, CancellationToken cancellationToken);
        public UniTask OnStatsUpdated(SessionRegistry sessionRegistry, CancellationToken cancellationToken);
        public UniTask UpdateInternal(float deltaTime, int itemIndex, SessionRegistry sessionRegistry, CancellationToken cancellationToken);
        public void CleanUp();

        public Sprite GetItemImage();
        public string GetItemName();
        public string GetObtainDescription();
        public string GetLevelUpDescription();
    }
}