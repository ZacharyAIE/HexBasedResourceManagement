using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using ResourceManagement.BuildingSystem;

[CustomPropertyDrawer(typeof(Cost))]
public class BuildingCostDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {

        // Using BeginProperty / EndProperty on the parent property means that
        // prefab override logic works on the entire property.
        EditorGUI.BeginProperty(position, label, property);

        // Draw label
        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        // Don't make child fields be indented
        var indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        // Calculate rects
        float w1 = (EditorGUIUtility.currentViewWidth - position.x) * 0.5f;
        float w2 = (EditorGUIUtility.currentViewWidth - position.x) * 0.5f;
        var statRect = new Rect(position.x, position.y, w1, position.height);
        var valueRect = new Rect(position.x + w1, position.y, w2, position.height);
        // Draw fields - passs GUIContent.none to each so they are drawn without labels
        EditorGUI.PropertyField(statRect, property.FindPropertyRelative(nameof(Cost.resourceType)), GUIContent.none);
        EditorGUI.PropertyField(valueRect, property.FindPropertyRelative(nameof(Cost.resourceAmount)), GUIContent.none);
        // Set indent back to what it was
        EditorGUI.indentLevel = indent;

        EditorGUI.EndProperty();
    }
}
