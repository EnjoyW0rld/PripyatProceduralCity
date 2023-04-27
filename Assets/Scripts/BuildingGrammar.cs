using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingGrammar : MyShape
{
    [SerializeField] public Bounds bounds;
    [SerializeField] private GameObject wallPanel;
    [SerializeField] private GameObject roofPrefab;
    [SerializeField] private int floors;
    [SerializeField] public int scaleFactor;


    /**
    public void GenerateFloor()
    {
        Vector3 size = bounds.size;
        Vector3 start = bounds.min;
        Vector3 pos = start;
        for (int i = 0; i < walls; i++)
        {
            Quaternion rotation = Quaternion.Euler(0, -90 * i, 0);


            WallGrammar wallGrammar = CreateSymbol<WallGrammar>("wall", pos, rotation);
            //axis
            Vector3 axisUnclamped = i%2 == 0 ? new Vector3(size.x,0,0) : new Vector3(0,0,size.z);
            //wall direction
            Vector3 dir = (axisUnclamped.normalized);
            
            wallGrammar.Initialize(wallPanel, dir, Quaternion.identity, (int)size.x);
            wallGrammar.GenerateWall();
            if (i < 2) pos += axisUnclamped;
            else pos -= axisUnclamped;
        }
    }
        /**/

    private void Start()
    {
        //wallPanel.
    }

    public void GenerateBuilding()
    {
        for (int i = 0; i < floors; i++)
        {
            Vector3 startPos = bounds.min;
            startPos.y = i * scaleFactor;
            FloorGrammar floorGrammar = CreateSymbol<FloorGrammar>("floor", bounds.center, Quaternion.identity);
            floorGrammar.Initialize(startPos, bounds.size, wallPanel, scaleFactor);
            floorGrammar.GenerateFloor();
        }
        GenerateRoof();
    }

    private void GenerateRoof()
    {
        GameObject roof = Instantiate(roofPrefab, transform);//GameObject.CreatePrimitive(PrimitiveType.Plane);
        createdObjects.Add(roof);
        roof.gameObject.name = "roof";
        //roof.transform.parent = this.transform;
        roof.transform.position = new Vector3(transform.position.x, scaleFactor * floors + 0.01f, transform.position.z);
        roof.transform.localScale = new Vector3(bounds.extents.x / (scaleFactor / 2f), roof.transform.localScale.y, bounds.extents.z / (scaleFactor / 2f));
        roof.isStatic = true;
    }
    [ContextMenu("Execute")]
    protected override void Execute()
    {
        DeleteGenerated();
        GenerateBuilding();
    }
}
