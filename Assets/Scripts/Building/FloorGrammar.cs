using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorGrammar : MyShape
{
    [SerializeField] private int _width;
    [SerializeField] private int _length;
    [SerializeField] private GameObject _prefab;
    private FloorTiles _tiles;
    private Vector3 size;
    private Vector3 start;
    private int scaleFactor;

    public void Initialize(Vector3 startPos, Vector3 size, FloorTiles floorTiles, int scaleFactor)
    {
        this.size = size;
        this.scaleFactor = scaleFactor;
        this._tiles = floorTiles;
        this.start = startPos;
    }
    public void Initialize(Vector3 startPos, Vector3 size, GameObject panel, int scaleFactor)
    {
        this.size = size;
        this.scaleFactor = scaleFactor;
        this.start = startPos;
        _prefab = panel;
    }

    public void GenerateFloor()
    {
        Vector3 pos = start;
        for (int i = 0; i < 4; i++)
        {
            Quaternion rotation = Quaternion.Euler(0, -90 * i, 0);


            WallGrammar wallGrammar = CreateSymbol<WallGrammar>("wall", pos, rotation);
            //axis
            Vector3 axisUnclamped = i % 2 == 0 ? new Vector3(size.x, 0, 0) : new Vector3(0, 0, size.z);

            int length = (int)axisUnclamped.magnitude / scaleFactor;// / scaleFactor;

            wallGrammar.Initialize(_tiles, Quaternion.identity, length, scaleFactor);
            wallGrammar.GenerateWall();
            if (i < 2) pos += axisUnclamped;
            else pos -= axisUnclamped;
        }

    }

    protected override void Execute()
    {
        GenerateFloor();
    }
}
