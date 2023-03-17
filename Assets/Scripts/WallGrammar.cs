using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallGrammar : MyShape
{
    //GameObject buildingBlock;
    [SerializeField] private int length;
    [SerializeField] private GameObject wallPanel;
    Vector3 direction;
    Quaternion rotation;

    private void Start()
    {
        Initialize(new Vector3(0, 0, 1), Quaternion.Euler(0,90,0));
        GenerateWall();
    }
    private void Initialize(Vector3 direction, Quaternion rotation)
    {
        this.direction = direction;
        this.rotation = rotation;
    }


    private void GenerateWall()
    {
        Vector3 wallPos = Vector3.zero;
        for (int i = 0; i < length; i++)
        {
            SpawnPrefab(wallPanel, wallPos, rotation);
            wallPos += direction;
        }
    }

    [ContextMenu("Execute")]
    protected override void Execute()
    {
        GenerateWall();
    }
}
