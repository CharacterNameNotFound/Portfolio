using Configurations.PlatformDependentFields;
using UnityEngine.AddressableAssets;
using Utils.UtilityTypes.AssetReferencing;

namespace GameWideSystems.UIManagement.Screen
{
    public interface IScreenAddressableReferenceProvider<T> where T : UIScreenBase
    {
        public AssetReference GetScreenRuntimeKey(PlatformType platformType);
    }
}