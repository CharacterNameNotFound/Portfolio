using Configurations.PlatformDependentFields;
using Configurations.PlatformDependentFields.Implementations;
using GameWideSystems.UIManagement.Screen;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Utils.UtilityTypes.AssetReferencing;

namespace Game.GameMode.MainHub.UI.Screen
{
    public class MainHubGameModeAddressableProvider : ScriptableObject, IScreenAddressableReferenceProvider<MainHubScreenController>
    {
        [field: SerializeField] public PlatformDependentAssetReference ScreenReference { get; private set; }
        
        public AssetReference GetScreenRuntimeKey(PlatformType platformType)
        {
            return ScreenReference.Get(platformType);
        }
    }
    
}