using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(RoadPlacer))]
public class RoadPlacerEditor : Editor
{
    private bool inConnectionState;
    private int selectedJunction = -1;
    public bool allowDeselect = true;
    private void OnSceneGUI()
    {
        RoadPlacer placer = target as RoadPlacer;
        if (placer.junctions != null)
        {
            for (int i = 0; i < placer.junctions.Length; i++)
            {
                if (!inConnectionState)
                {
                    EditorGUI.BeginChangeCheck();
                    Vector3 junctPos = Handles.PositionHandle(placer.junctions[i].junctionPos, Quaternion.identity);
                    if (EditorGUI.EndChangeCheck())
                    {
                        Undo.RecordObject(placer, "handle move");
                        placer.junctions[i].junctionPos = junctPos;
                    }
                }

                if (placer.junctions[i].connections != null)
                {
                    for (int f = 0; f < placer.junctions[i].connections.Length; f++)
                    {
                        Handles.DrawLine(placer.junctions[placer.junctions[i].connections[f]].junctionPos, placer.junctions[i].junctionPos);
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
        if (Event.current.type == EventType.MouseDown)
        {
            if (inConnectionState && Event.current.button == 0)
            {

                //Event.current.Use();
                //Debug.Log(placer.GetJunctionClicked(ray));
                Vector2 pos = Event.current.mousePosition;
                pos.y -= Camera.current.pixelRect.yMax;
                pos.y *= -1;
                Ray ray = Camera.current.ScreenPointToRay(pos);

                int pointerJunktion = placer.GetJunctionClicked(ray);
                if (selectedJunction == -1)
                {
                    selectedJunction = pointerJunktion;
                    Debug.Log("now selected is " + selectedJunction);
                }
                else
                {
                    if(selectedJunction != pointerJunktion && pointerJunktion != -1)
                    {
                        Debug.Log("adding connection");
                        placer.junctions[selectedJunction].AddConnection(pointerJunktion);
                        placer.junctions[pointerJunktion].AddConnection(selectedJunction);
                        selectedJunction = -1;
                        inConnectionState = false;
                    }
                }
                //Debug.Log(pos);
                //Debug.Log(placer.GetJunctionClicked(ray));
            }
        }

        if (!allowDeselect) Selection.activeGameObject = placer.transform.gameObject;
    }

    public override void OnInspectorGUI()
    {
        RoadPlacer placer = target as RoadPlacer;
        base.OnInspectorGUI();
        allowDeselect = EditorGUILayout.Toggle("Allow deselect",allowDeselect);

        if (GUILayout.Button("Place"))
        {
            placer.PlaceRoad();
        }
        if (GUILayout.Button("Delete placed"))
        {
            placer.DeletePlaced();
        }
        if (GUILayout.Button("Connect rodes"))
        {
            inConnectionState = !inConnectionState;
            Debug.Log(inConnectionState);
            //ActiveEditorTracker.sharedTracker.isLocked = inConnectionState;
        }
    }
}
