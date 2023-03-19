using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallGrammar : MyShape
{
    [SerializeField] private int length;
    [SerializeField] private GameObject wallPanel;
    [SerializeField] private Quaternion rotation;

    public void Initialize(GameObject wallPanel, Quaternion rotation, int length)
    {
        this.rotation = rotation;
        this.length = length;
        this.wallPanel = wallPanel;
    }

    
    public void GenerateWall()
    {
        Vector3 wallPos = new Vector3(.5f, 0, 0);//direction.normalized * .5f;//new Vector3(0, 0, 0);//Vector3.zero;
        for (int i = 0; i < length; i++)
        {
            SpawnPrefab(wallPanel, wallPos, rotation);
            wallPos += new Vector3(1, 0, 0);//direction;
        }
    }

    [ContextMenu("Execute")]
    protected override void Execute()
    {
        GenerateWall();
    }
}
