using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorGrammar : MyShape
{
    [SerializeField] private int _width;
    [SerializeField] private int _length;
    [SerializeField] private GameObject _prefab;


    private void GenerateFloor()
    {

    }

    protected override void Execute()
    {
        GenerateFloor();
    }
}
