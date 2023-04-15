using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadPlacer : MonoBehaviour
{
    [SerializeField] private GameObject roadPrefab;
    private List<GameObject> placedTiles;

    private void PlaceRoad()
    {
            
    }

    private void DeletePlaced()
    {
        if (placedTiles.Count > 0)
        {
            for (int i = 0; i < placedTiles.Count; i++)
            {
                Destroy(placedTiles[i].gameObject);
            }
            placedTiles.Clear();
        }
    }
}
