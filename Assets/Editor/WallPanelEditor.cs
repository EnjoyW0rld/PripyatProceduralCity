using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(WallPanel))]
public class WallPanelEditor : Editor
{
    public override void OnInspectorGUI()
    {
        WallPanel panel = (WallPanel)target;
        base.OnInspectorGUI();
        if (GUILayout.Button("Regenarate"))
        {
            panel.Regenerate();
        }
    }
}
