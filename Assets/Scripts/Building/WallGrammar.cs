using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallGrammar : MyShape
{
    [SerializeField] private int length;
    //[SerializeField] private GameObject wallPanel;
    [SerializeField] private Quaternion rotation;
    private FloorTiles tiles;
    private int scaleFactor;

    public void Initialize(FloorTiles floorTiles, Quaternion rotation, int length,int scaleFactor)
    {
        this.rotation = rotation;
        this.length = length;
        this.scaleFactor = scaleFactor;
        tiles = floorTiles;
    }

    
    public void GenerateWall()
    {
        Vector3 wallPos = new Vector3(.5f * scaleFactor, 0, 0);//direction.normalized * .5f;//new Vector3(0, 0, 0);//Vector3.zero;
        for (int i = 0; i < length; i++)
        {
            GameObject toSpawn = tiles.wallPanels[Random.Range(0, tiles.wallPanels.Length)];
            SpawnPrefab(toSpawn, wallPos, rotation);
            wallPos += new Vector3(1 * scaleFactor, 0, 0);//direction;
        }
    }

    [ContextMenu("Execute")]
    protected override void Execute()
    {
        GenerateWall();
    }
}
