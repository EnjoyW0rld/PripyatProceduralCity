using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="floor data")]
public class FloorTiles : ScriptableObject
{
    [Tooltip("Set -1 to allow placing at any floor")]
    public int targetFloor = -1;
    public enum TargetFloor { First, Last, Any }
    [Tooltip("Use this to specify floor, use targetFloor for detailed set up")]
    public TargetFloor floorType;
    public GameObject[] wallPanels;
}
