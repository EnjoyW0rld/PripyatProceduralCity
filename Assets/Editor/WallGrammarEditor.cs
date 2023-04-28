using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(WallGrammar))]
public class WallGrammarEditor : Editor
{
    public override void OnInspectorGUI()
    {
        WallGrammar grammar = (WallGrammar)target;
        base.OnInspectorGUI();
        if(GUILayout.Button("Regenarate wall"))
        {
            grammar.Regenerate();
        }
    }
}
