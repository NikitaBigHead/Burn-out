using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(DoorSpawnAttribute))]
public class DoorSpawnDrawer : PropertyDrawer
{
    // Draw the property inside the given rect
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        DoorSpawnAttribute doorSpawnAttribute = attribute as DoorSpawnAttribute;

        Rect valueRect = position;
        valueRect.width -= position.width * 0.1f;

        Rect boolRect = position;
        boolRect.x += valueRect.width;
        boolRect.width -= position.width * 0.9f;

        doorSpawnAttribute.enumMode = EditorGUI.Toggle(boolRect, doorSpawnAttribute.enumMode);
        if (doorSpawnAttribute.enumMode)
            EditorGUI.EnumPopup(valueRect, label, SceneLoader.Position.Center);
        else
            EditorGUI.Vector3Field(valueRect, label, property.vector3Value);
    }
}
