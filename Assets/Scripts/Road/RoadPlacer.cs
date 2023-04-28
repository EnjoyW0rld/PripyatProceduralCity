using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadPlacer : MonoBehaviour
{
    [SerializeField] private GameObject roadPrefab;
    [SerializeField, Min(0.1f)] private float scaleFactor;
    [SerializeField] private GameObject turnPrefab;

    public RoadJunction[] junctions;
    //public Connection[] connections;

    private List<GameObject> placedTiles = new List<GameObject>();
    public void PlaceRoad()
    {

        for (int i = 0; i < junctions.Length; i++)
        {
            SpawnJunction(junctions[i]);
        }
    }


    private void SpawnJunction(RoadJunction junction)
    {
        if (junction.connections.Length == 2)
        {
            GameObject turn = Instantiate(turnPrefab, transform);
            placedTiles.Add(turn);
            Vector3 dirToZero = (junctions[junction.connections[0]].junctionPos - junction.junctionPos).normalized;
            turn.transform.position = junction.junctionPos;
            turn.transform.forward = dirToZero;
            Vector3 dirToFirst = (junctions[junction.connections[1]].junctionPos - junction.junctionPos).normalized;

            //Cross product to determine wher need to turn road
            Vector3 cross = Vector3.Cross(dirToZero, dirToFirst);

            //Mirroring turn accordingly to where it is facing
            turn.transform.localScale = new Vector3(
                turn.transform.localScale.x * (cross.y > 0 ? -1 : 1)
                , turn.transform.localScale.y,
                turn.transform.localScale.z);

            for (int i = 0; i < junction.connections.Length; i++)
            {
                SpawnConnection(junction.junctionPos, junctions[junction.connections[i]].junctionPos);
            }
        }
    }

    private void SpawnConnection(Vector3 startPos, Vector3 desiredPos)
    {
        Vector3 direction = (startPos - desiredPos).normalized;
        startPos -= (direction * scaleFactor) / 2f;
        float length = (startPos - desiredPos).magnitude / scaleFactor;
        GameObject newParent = new GameObject("parentForConnection");
        newParent.transform.parent = transform;
        for (int i = 1; i < length; i++)
        {
            SpawnTwoRoads(startPos - (direction * i * scaleFactor), Quaternion.LookRotation(direction, Vector3.up), newParent.transform);
        }
        placedTiles.Add(newParent);
    }

    private void SpawnTwoRoads(Vector3 pos, Quaternion dir, Transform parent)
    {
        for (int i = 0; i < 2; i++)
        {
            Vector3 angle = new Vector3(0, 180 * i, 0);
            GameObject obj = Instantiate(roadPrefab, pos, Quaternion.Euler(angle) * dir);
            placedTiles.Add(obj);
            obj.transform.parent = parent; //set parent for created object
        }
    }

    public void DeletePlaced()
    {
        if (placedTiles.Count > 0)
        {
            for (int i = 0; i < placedTiles.Count; i++)
            {
                DestroyImmediate(placedTiles[i].gameObject);
            }
            placedTiles.Clear();
        }
        for (int i = 0; i < junctions.Length; i++)
        {
            junctions[i].placed = false;
        }
    }
    private void OnDrawGizmosSelected()
    {
        for (int i = 0; i < junctions.Length; i++)
        {
            Gizmos.DrawSphere(junctions[i].junctionPos, 1);
        }
    }

    public int GetJunctionClicked(Ray ray)
    {
        //print("mouse pos " + Input.mousePosition);
        Plane pl = new Plane(Vector3.up, Vector3.zero);
        pl.Raycast(ray, out float distance);


        Vector3 point = ray.GetPoint(distance);
        for (int i = 0; i < junctions.Length; i++)
        {
            if ((junctions[i].junctionPos - point).magnitude <= 1)
            {
                return i;
            }
        }
        return -1;
    }
}


[Serializable]
public class RoadJunction
{
    public Vector3 junctionPos;
    public int[] connections;
    public bool placed;

    public void AddConnection(int id)
    {
        if (connections == null)
        {
            connections = new int[] { id };
            Debug.Log("now has connection " + connections.Length);
        }
        else
        {
            for (int i = 0; i < connections.Length; i++)
            {
                if (connections[i] == id) return;
            }
            int[] temp = new int[connections.Length + 1];
            /*
            for (int i = 0; i < connections.Length; i++)
            {
                temp[i] = connections[i];
            }
             */
            connections.CopyTo(temp, 0);
            temp[connections.Length] = id;
            connections = temp;
            Debug.Log("now has connection " + connections.Length);
        }
    }
}