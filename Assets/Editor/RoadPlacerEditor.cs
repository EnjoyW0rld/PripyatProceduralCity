using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(RoadPlacer))]
public class RoadPlacerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Place")){
            //SerializedObject obj = new SerializedObject(target);
                //serializedObject.FindProperty(22).
        }
    }
}
