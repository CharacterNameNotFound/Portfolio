using GameWideSystems.TooltipsManagement;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Utils.UtilityTypes.AssetReferencing;

namespace Game.UI.Tooltips
{
    public class TooltipConfigurations : ScriptableObject, ITooltipConfigurationProvider
    {
        [field: SerializeField] public AssetReference TextTooltipPrefab { get; private set; }
        public AssetReference TextTooltipPrefabReference => TextTooltipPrefab;
    }
}