using Configurations.PlatformDependentFields;
using Configurations.PlatformDependentFields.Implementations;
using Game.GameMode.Session.UI;
using GameWideSystems.UIManagement.Screen;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Game.GameMode.Session.Controller
{
    public class SessionGameModeAddressableProvider : ScriptableObject, IScreenAddressableReferenceProvider<SessionScreenController>
    {
        [field: SerializeField] public PlatformDependentAssetReference ScreenReference { get; private set; }
        
        public AssetReference GetScreenRuntimeKey(PlatformType platformType)
        {
            return ScreenReference.Get(platformType);
        }
    }
}