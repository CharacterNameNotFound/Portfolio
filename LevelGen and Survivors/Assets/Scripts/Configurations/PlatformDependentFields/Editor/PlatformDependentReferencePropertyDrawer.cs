using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Configurations.PlatformDependentFields.Editor
{
    public class PlatformDependentReferencePropertyDrawer<T> : PropertyDrawer
    {
        private bool _isUnfold;
        private string[] _enumValues;
        
        
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            SerializedProperty platformsArray = property.FindPropertyRelative("_platformDependantValues");
            int arrayLength = platformsArray.arraySize;

            _enumValues = Enum.GetNames(typeof(PlatformType));
            Array values = _enumValues;
            int requiredLength = values.Length;

            while (arrayLength < requiredLength)
            {
                platformsArray.InsertArrayElementAtIndex(arrayLength);
                arrayLength++;
            }

            platformsArray.serializedObject.ApplyModifiedPropertiesWithoutUndo();
            return base.CreatePropertyGUI(property);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
            { 
                
                _isUnfold = EditorGUILayout.Foldout(_isUnfold, label);
                if (_isUnfold)
                {
                    DrawValue(property);
                }
                
            }
            EditorGUI.EndProperty();

            property.serializedObject.ApplyModifiedPropertiesWithoutUndo();
        }

        private void DrawValue(SerializedProperty property)
        {
            EditorGUI.indentLevel++;
            {
                EditorGUILayout.PropertyField(property.FindPropertyRelative("_invariantValue"));
                
                EditorGUILayout.PropertyField(property.FindPropertyRelative("_platformOverride"));
                property.serializedObject.ApplyModifiedPropertiesWithoutUndo();
                
                bool isOverriding = property.FindPropertyRelative("_platformOverride").boolValue;
                if (isOverriding)
                {
                    DrawOverrides(property);
                }
                        
            }
            EditorGUI.indentLevel--;
        }

        private void DrawOverrides(SerializedProperty property)
        {
            var platformValuesArray = property.FindPropertyRelative("_platformDependantValues");
                        
            for (int i = 0; i < _enumValues.Length; i++)
            {
                SerializedProperty element = platformValuesArray.GetArrayElementAtIndex(i);
                EditorGUILayout.LabelField(_enumValues[i]);
                
                EditorGUILayout.PropertyField(element);
            }
        }


    }
}