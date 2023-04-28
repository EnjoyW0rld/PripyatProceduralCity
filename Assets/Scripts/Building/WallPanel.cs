using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallPanel : MyShape
{
    [SerializeField] private GameObject wall;

    public void Initialize(GameObject wall)
    {
        this.wall = wall;
    }

    public void GeneratePanel()
    {
        SpawnPrefab(wall);
    }
    public void Regenerate()
    {
        DeleteGenerated();
        GeneratePanel();
    }

    protected override void Execute()
    {

    }
}
