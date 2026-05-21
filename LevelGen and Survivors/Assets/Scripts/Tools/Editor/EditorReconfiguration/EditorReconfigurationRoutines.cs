using System.Linq;
using UnityEditor;
using UnityEditor.Build;

namespace Tools.Editor
{
    public static class EditorReconfigurationRoutines
    {
        public static void AddScriptingDefinition(string definition)
        {
            BuildTarget activeBuildTarget = EditorUserBuildSettings.activeBuildTarget;
            NamedBuildTarget named = NamedBuildTarget.FromBuildTargetGroup(BuildPipeline.GetBuildTargetGroup(activeBuildTarget));
            PlayerSettings.GetScriptingDefineSymbols(named, out string[] defines);

            if (defines.Contains(definition))
            {
                return;
            }

            defines = defines
                .Concat(new[] {definition})
                .ToArray();

            PlayerSettings.SetScriptingDefineSymbols(named, defines);
        }

        public static void RemoveScriptingDefinition(string definition)
        {
            BuildTarget activeBuildTarget = EditorUserBuildSettings.activeBuildTarget;
            NamedBuildTarget named = NamedBuildTarget.FromBuildTargetGroup(BuildPipeline.GetBuildTargetGroup(activeBuildTarget));
            PlayerSettings.GetScriptingDefineSymbols(named, out string[] defines);

            if (!defines.Contains(definition))
            {
                return;
            }
            
            defines = defines
                .Except(new []{definition})
                .ToArray(); 
            
            PlayerSettings.SetScriptingDefineSymbols(named, defines);
        }
    }
}