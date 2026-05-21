using UnityEditor;

namespace Tools.Editor
{
    public static class EditorReconfigurator
    {
        [MenuItem(itemName: "Game/Debug/Battle/Enable Battle Logging")]
        public static void EnableBattleLogging()
        {
            EditorReconfigurationRoutines.AddScriptingDefinition(ScriptingDefinitions.GameBattleLogScriptingDefinition);
        }

        [MenuItem(itemName: "Game/Debug/Battle/Disable Battle Logging")]
        public static void DisableBattleLogging()
        {
            EditorReconfigurationRoutines.RemoveScriptingDefinition(ScriptingDefinitions.GameBattleLogScriptingDefinition);
        }
        
        [MenuItem(itemName: "Game/Debug/Battle/Injection/Enable Dolly Injection")]
        public static void EnableDollyInjections()
        {
            EditorReconfigurationRoutines.AddScriptingDefinition(ScriptingDefinitions.BattleDollyInstallation);
        }

        [MenuItem(itemName: "Game/Debug/Battle/Injection/Disable Dolly Injection")]
        public static void DisableDollyInjections()
        {
            EditorReconfigurationRoutines.RemoveScriptingDefinition(ScriptingDefinitions.BattleDollyInstallation);
        }


    }
}