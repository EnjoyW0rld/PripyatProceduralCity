using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(RoadPlacer))]
public class RoadPlacerEditor : Editor
{
    private void OnSceneGUI()
    {
        RoadPlacer placer = target as RoadPlacer;
        if (placer.junctions != null)
        {
            for (int i = 0; i < placer.junctions.Length; i++)
            {
                EditorGUI.BeginChangeCheck();
                Vector3 junctPos = Handles.PositionHandle(placer.junctions[i].junctionPos, Quaternion.identity);
                if (EditorGUI.EndChangeCheck())
                {
                    Undo.RecordObject(placer, "handle move");
                    placer.junctions[i].junctionPos = junctPos;
                }
                if (placer.junctions[i].ids != null)
                {
                    for (int f = 0; f < placer.junctions[i].ids.Length; f++)
                    {
                        Handles.DrawLine(placer.junctions[placer.junctions[i].ids[f]].junctionPos, placer.junctions[i].junctionPos);
                    }
                }
            }
        }
        /*
        if (placer.connections != null)
        {
            for (int i = 0; i < placer.connections.Length; i++)
            {
                if (placer.connections[i].ids != null && placer.connections[i].ids.Length == 2)
                Handles.DrawLine(placer.junctions[placer.connections[i].ids[0]].junctionPos, placer.junctions[placer.connections[i].ids[1]].junctionPos);
            }
        }
         */

        //Check where pressed
        if (Event.current.type == EventType.KeyDown)
        {
            if (Event.current.keyCode != KeyCode.Space) return;
            Event.current.Use();
            //Debug.Log(placer.GetJunctionClicked(ray));
            Vector2 pos = Event.current.mousePosition;
            pos.y -= Camera.current.pixelRect.yMax;
            pos.y *= -1;
            Ray ray = Camera.current.ScreenPointToRay(pos);

            Debug.Log(pos);
            placer.GetJunctionClicked(ray);
        }
    }

    public override void OnInspectorGUI()
    {
        RoadPlacer placer = target as RoadPlacer;
        base.OnInspectorGUI();

        if (GUILayout.Button("Place"))
        {
            placer.PlaceRoad();
        }
        if (GUILayout.Button("Delete placed"))
        {
            placer.DeletePlaced();
        }
    }
}
