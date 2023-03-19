using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.IMGUI.Controls;

[CustomEditor(typeof(BuildingGrammar))]
public class BuildingGrammarEditor : Editor
{
    private BoxBoundsHandle boxBoundsHandle = new BoxBoundsHandle();
    private void OnSceneGUI()
    {
        BuildingGrammar grammar = target as BuildingGrammar;
        Vector3 pos = grammar.transform.position;

        //BoxBoundsHandle box = new BoxBoundsHandle();
        boxBoundsHandle.center = grammar.transform.position;
        Vector3 size = grammar.bounds.size;
        size.y = 0;
        boxBoundsHandle.size = size;

        EditorGUI.BeginChangeCheck();
        boxBoundsHandle.DrawHandle();
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(grammar, "bounds change");
            Bounds newBounds = new Bounds();
            newBounds.center = boxBoundsHandle.center;
            newBounds.size = boxBoundsHandle.size;
            grammar.bounds = newBounds;
            EditorUtility.SetDirty(grammar);
        }

    }

    public override void OnInspectorGUI()
    {
        BuildingGrammar grammar = target as BuildingGrammar;
        base.OnInspectorGUI();

        if (GUILayout.Button("Execute"))
        {
            grammar.GenerateBuilding();
        }
        if (GUILayout.Button("Delete generated"))
        {
            grammar.DeleteGenerated();
        }
    }
}
