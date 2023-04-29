using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(FloorGrammar))]
public class FloorGrammarEditor : Editor
{
    public override void OnInspectorGUI()
    {
        FloorGrammar grammar = target as FloorGrammar;
        base.OnInspectorGUI();

        if (GUILayout.Button("Regenerate floor"))
        {
            grammar.Regenerate();
        }
    }
}
