using Configurations.PlatformDependentFields.Implementations;
using UnityEditor;

namespace Configurations.PlatformDependentFields.Editor.Binds
{
    [CustomPropertyDrawer(typeof(PlatformDependentAssetReference))]
    public class PlatformDependantAssetReferenceDrawer : PlatformDependentReferencePropertyDrawer<PlatformDependantAssetReferenceDrawer>
    {
        
    }
}