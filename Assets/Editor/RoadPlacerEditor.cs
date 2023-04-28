using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(RoadPlacer))]
public class RoadPlacerEditor : Editor
{
    private bool inConnectionState;
    private bool inAllignState;
    private int selectedConnectJunction = -1;
    private int selectedAllignJunction = -1;
    public bool allowDeselect = true;
    private void OnSceneGUI()
    {
        RoadPlacer placer = target as RoadPlacer;
        if (placer.junctions != null)
        {
            for (int i = 0; i < placer.junctions.Length; i++)
            {
                if (!inConnectionState && !inAllignState)
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
            if (Event.current.button == 0)
            {
                if (inConnectionState)
                {

                    Vector2 pos = Event.current.mousePosition;
                    pos.y -= Camera.current.pixelRect.yMax;
                    pos.y *= -1;
                    Ray ray = Camera.current.ScreenPointToRay(pos);

                    int pointerJunktion = placer.GetJunctionClicked(ray);
                    if (selectedConnectJunction == -1)
                    {
                        selectedConnectJunction = pointerJunktion;
                    }
                    else
                    {
                        if (selectedConnectJunction != pointerJunktion && pointerJunktion != -1)
                        {
                            Undo.RecordObject(placer, "adding connection");

                            Debug.Log("adding connection");
                            placer.junctions[selectedConnectJunction].AddConnection(pointerJunktion);
                            placer.junctions[pointerJunktion].AddConnection(selectedConnectJunction);
                            selectedConnectJunction = -1;
                            inConnectionState = false;
                        }
                    }
                    //Debug.Log(pos);
                    //Debug.Log(placer.GetJunctionClicked(ray));
                }
                if (inAllignState)
                {

                    Vector2 pos = Event.current.mousePosition;
                    pos.y -= Camera.current.pixelRect.yMax;
                    pos.y *= -1;
                    Ray ray = Camera.current.ScreenPointToRay(pos);
                    int pointerJunktion = placer.GetJunctionClicked(ray);

                    if (selectedAllignJunction == -1)
                    {
                        selectedAllignJunction = pointerJunktion;
                    }
                    else if (selectedAllignJunction != pointerJunktion && pointerJunktion != -1)
                    {
                        Undo.RecordObject(placer, "adding connection");
                        AllignRoads(placer.junctions[selectedAllignJunction], placer.junctions[pointerJunktion], placer.GetScale());
                        selectedAllignJunction = -1;
                        inAllignState = false;
                    }

                }
            }
        }

        if (!allowDeselect) Selection.activeGameObject = placer.transform.gameObject;
    }

    private void AllignRoads(RoadJunction junct1, RoadJunction junct2, float scale)
    {
        Vector3 diff = Abs(junct1.junctionPos) - Abs(junct2.junctionPos);
        if (diff.x == 0 || diff.z == 0) return;
        diff = Abs(diff);
        //Debug.Log(diff);

        if (diff.x < diff.z)
        {
            junct1.junctionPos.x = junct2.junctionPos.x;
            float length = (int)((junct1.junctionPos - junct2.junctionPos).magnitude / scale);
            Vector3 dir = (junct1.junctionPos - junct2.junctionPos).normalized;
            junct1.junctionPos.z = (junct2.junctionPos + (dir * length * (scale + 0.01f))).z;


            if (length > scale / 2)
            {
                //junct1.junctionPos.z += (dir * (scale - length)).z - 0.001f;
            }
            else
            {
                //junct1.junctionPos.z += (dir * (-length)).z - 0.001f;
            }
        }
        else
        {
            junct1.junctionPos.z = junct2.junctionPos.z;
            float length = (int)((junct1.junctionPos - junct2.junctionPos).magnitude / scale);
            Debug.Log(length);
            Vector3 dir = (junct1.junctionPos - junct2.junctionPos).normalized;
            junct1.junctionPos.x = (junct2.junctionPos + (dir * length * (scale + 0.01f))).x;

            if (length > scale / 2)
            {
                //junct1.junctionPos.x
                //junct1.junctionPos.x += (dir * (scale - length)).x - 0.01f;
            }
            else
            {
                //junct1.junctionPos.x = (junct2.junctionPos + (dir * length)).x;
                //junct1.junctionPos.x += (dir * (-length)).x + 0.01f;
            }

        }

    }
    private Vector3 Abs(Vector3 value)
    {
        return new Vector3(Mathf.Abs(value.x), Mathf.Abs(value.y), Mathf.Abs(value.z));
    }
    public override void OnInspectorGUI()
    {
        RoadPlacer placer = target as RoadPlacer;
        base.OnInspectorGUI();
        allowDeselect = EditorGUILayout.Toggle("Allow deselect", allowDeselect);

        if (GUILayout.Button("Place"))
        {
            placer.DeletePlaced();
            placer.PlaceRoad();
        }
        if (GUILayout.Button("Delete placed"))
        {
            placer.DeletePlaced();
        }
        if (GUILayout.Button("Connect roads"))
        {
            inConnectionState = !inConnectionState;
            Debug.Log(inConnectionState);
        }
        if (GUILayout.Button("Disconnect roads"))
        {
            Undo.RecordObject(placer, "Roads disconnected");

            for (int i = 0; i < placer.junctions.Length; i++)
            {
                placer.junctions[i].connections = null;
            }
        }
        if (GUILayout.Button("Road Allign mode"))
        {
            inAllignState = !inAllignState;
        }

        if (inConnectionState && inAllignState)
        {
            Debug.LogWarning("Two states been selected, deselecting both");
            inAllignState = false;
            inAllignState = false;
        }
    }
}
