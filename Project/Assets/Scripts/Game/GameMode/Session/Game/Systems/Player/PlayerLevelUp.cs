using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.Session.Game.Data;
using Game.GameMode.Session.Game.Data.Entities;
using Game.GameMode.Session.Game.Items;
using Game.GameMode.Session.Game.Items.Weapons;
using Game.GameMode.Session.Game.Utilities;
using UnityEngine;

namespace Game.GameMode.Session.Game.Systems.Player
{
    public class PlayerLevelUp : ILoopedSystem
    {
        private SessionScreenHolder _sessionScreenHolder;

        private IItem[] _itemList;

        private List<IItem> _combinedUpgradable;

        public PlayerLevelUp(SessionScreenHolder sessionScreenHolder, IItem[] itemList)
        {
            _sessionScreenHolder = sessionScreenHolder;
            _itemList = itemList;
        }

        public UniTask Initialize(SessionRegistry sessionRegistry, CancellationToken cancellationToken)
        {
            _sessionScreenHolder.SessionScreenController.SetExp(0);

            PlayerStats playerStats = sessionRegistry.PlayerStats;
            playerStats.CurrentExp = 0;
            playerStats.Level = 1;

            _combinedUpgradable = new List<IItem>(_itemList);
            
            return UniTask.CompletedTask;
        }

        public async UniTask Update(float deltaTime, SessionRegistry sessionRegistry, CancellationToken cancellationToken)
        {
            PlayerStats playerStats = sessionRegistry.PlayerStats;
            float barValue = playerStats.CurrentExp / (playerStats.RequiredExpPerLevel * playerStats.Level); 
            
            _sessionScreenHolder.SessionScreenController.SetExp(barValue);

            if (_combinedUpgradable.Count < 3)
            {
                return;
            }

            if (!(barValue >= 1))
            {
                return;
            }

            IItem[] upgradeOptions = SelectNextUpgradeSet();

            int option = await _sessionScreenHolder.SessionScreenController.ShowLevelUpScreen(upgradeOptions,
                    cancellationToken);

            await UpgradeItem(upgradeOptions[option], sessionRegistry, cancellationToken);

            playerStats.CurrentExp -= playerStats.RequiredExpPerLevel * playerStats.Level;
            playerStats.Level++;
            barValue = playerStats.CurrentExp / (playerStats.RequiredExpPerLevel * playerStats.Level);
            _sessionScreenHolder.SessionScreenController.SetExp(barValue);
            
        }

        private IItem[] SelectNextUpgradeSet()
        {
            IItem[] result = new IItem[3];
            
            for (int i = 0; i < result.Length; i++)
            {
                int randIndex = Random.Range(0, _combinedUpgradable.Count);

                if (result.Contains(_combinedUpgradable[randIndex]))
                {
                    i--;
                    continue;
                }

                result[i] = _combinedUpgradable[randIndex];
            }
            
            return result;
        }

        private async UniTask UpgradeItem(IItem item, SessionRegistry sessionRegistry, CancellationToken cancellationToken)
        {
            if (sessionRegistry.ObtainedItems.Contains(item))
            {
                await item.OnUpgrade(sessionRegistry, cancellationToken);
            }
            else
            {
                await item.OnObtained(sessionRegistry, cancellationToken);
            }

            if (item.GetLevel() + 1 == item.GetMaxLevel())
            {
                _combinedUpgradable.Remove(item);
            }

            if (item is CooldownItem)
            {
                await item.OnStatsUpdated(sessionRegistry, cancellationToken);
                return;
            }
            
            for (int i = 0; i < sessionRegistry.ObtainedItems.Count; i++)
            {
                await sessionRegistry.ObtainedItems[i].OnStatsUpdated(sessionRegistry, cancellationToken);
            }
            
        }
        
        
        
        
    }
}