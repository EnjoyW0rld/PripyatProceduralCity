using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(ButtonAttribute))]
public class ButtonAttributeEditor : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        base.OnGUI(position, property, label);
        object value = property.serializedObject.targetObject;
        var type = value.GetType();
        
        if (GUILayout.Button(label))
        {
            
        }
    }
}
