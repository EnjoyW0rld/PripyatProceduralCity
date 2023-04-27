using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadPlacer : MonoBehaviour
{
    [SerializeField] private GameObject roadPrefab;
    [SerializeField, Min(0.1f)] private float scaleFactor;
    //[SerializeField] public Vector3 pos;
    public RoadJunction[] junctions;
    public Connection[] connections;

    private List<GameObject> placedTiles = new List<GameObject>();
    public void PlaceRoad()
    {
        for (int i = 0; i < connections.Length; i++)
        {
            SpawnConnection(connections[i]);
        }
    }


    private void SpawnConnection(Connection connection)
    {
        //direction to build road
        Vector3 direction = (junctions[connection.ids[0]].junctionPos - junctions[connection.ids[1]].junctionPos).normalized;
        //amount of road tiles to place
        float length = (junctions[connection.ids[0]].junctionPos - junctions[connection.ids[1]].junctionPos).magnitude / scaleFactor;
        //parent game object for road tiles
        GameObject newParent = new GameObject("parentForConnection");
        newParent.transform.parent = transform;
        //Starting position of tiles
        Vector3 startPos = junctions[connection.ids[0]].junctionPos;
        for (int i = 0; i < length; i++)
        {
            SpawnTwoRoads(startPos - (direction * i * scaleFactor), Quaternion.LookRotation(direction, Vector3.up),newParent.transform);
        }
        placedTiles.Add(newParent);

    }
    private void SpawnTwoRoads(Vector3 pos, Quaternion dir,Transform parent)
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
public class Connection
{
    public int[] ids = new int[2];
}
[Serializable]
public class RoadJunction
{
    public Vector3 junctionPos;
}