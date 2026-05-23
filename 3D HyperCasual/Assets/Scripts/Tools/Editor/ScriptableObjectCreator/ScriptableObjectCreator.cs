using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Tools.Editor.ScriptableObjectCreator
{
    public class ScriptableObjectCreator : EditorWindow
    {
        private string _pathToCreate;
        private string _scriptType;
        private string _fileName;

        [MenuItem("Game/Tools/Scriptable creator")]
        public static void Open()
        {
            ScriptableObjectCreator window = GetWindow<ScriptableObjectCreator>();

            window.titleContent = new GUIContent("Scriptable creator");
            
            window.minSize = new Vector2(450, 100);
            window.maxSize = new Vector2(450, 100);
        }

        private void OnGUI()
        {
            _pathToCreate = EditorGUILayout.TextField(new GUIContent("Path"), _pathToCreate);
            _scriptType = EditorGUILayout.TextField(new GUIContent("Type"), _scriptType);
            _fileName = EditorGUILayout.TextField(new GUIContent("File Name"), _fileName);
            
            if (GUILayout.Button("Create") && IsPossibleToCreate())
            {
                ScriptableObject scriptableObject = CreateInstance(_scriptType);

                string path = Path.Combine(_pathToCreate, _fileName + ".asset");
                
                AssetDatabase.CreateAsset(scriptableObject, path);
            }
        }


        private bool IsPossibleToCreate()
        {
            if (string.IsNullOrWhiteSpace(_pathToCreate) && string.IsNullOrWhiteSpace(_scriptType) && string.IsNullOrWhiteSpace(_fileName))
            {
                return false;
            }
            
            if (!AssetDatabase.IsValidFolder(_pathToCreate))
            {
                return false;
            }

            string path = Path.Combine(_pathToCreate, _fileName + ".asset");
            
            if (!string.IsNullOrEmpty(AssetDatabase.AssetPathToGUID(path)))
            {
                return false;
            }

            if (Path.GetInvalidFileNameChars().Any(_fileName.Contains))
            {
                return false;
            }

            return true;
        }
        
    }
}