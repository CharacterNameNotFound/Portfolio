using Configurations.PlatformDependentFields.Implementations;
using UnityEditor;

namespace Configurations.PlatformDependentFields.Editor.Binds
{
    [CustomPropertyDrawer(typeof(PlatformDependentString))]
    public class PlatformDependentStringPropertyDrawer : PlatformDependentReferencePropertyDrawer<PlatformDependentStringPropertyDrawer>
    {

    }
}