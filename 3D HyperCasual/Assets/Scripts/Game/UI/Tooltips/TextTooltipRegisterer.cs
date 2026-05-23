using System.Collections.Generic;
using GameWideSystems.TooltipsManagement;
using UnityEngine;
using Utils.UtilityTypes.ObjectPooling;

namespace Game.UI.Tooltips
{
    public class TextTooltipRegisterer
    {
        private ITooltipManager _tooltipManager;
        private IPooledObjectHostProvider _pooledObjectGenericHostProvider;
        private ITooltipConfigurationProvider _tooltipConfigurations;
        

        public TextTooltipRegisterer(ITooltipManager tooltipManager, ITooltipConfigurationProvider tooltipConfigurations, IPooledObjectHostProvider pooledObjectGenericHostProvider)
        {
            _tooltipManager = tooltipManager;
            _pooledObjectGenericHostProvider = pooledObjectGenericHostProvider;
            _tooltipConfigurations = tooltipConfigurations;
        }

        public void Register()
        {
            var textTooltip = new TooltipPool(new List<TooltipBase>(), new AddressablePoolEntityProvider<TooltipBase>(_tooltipConfigurations.TextTooltipPrefabReference), _pooledObjectGenericHostProvider);
            _tooltipManager.RegisterTooltip(TooltipType.GenericText, textTooltip, Application.exitCancellationToken);
        }
    }
}