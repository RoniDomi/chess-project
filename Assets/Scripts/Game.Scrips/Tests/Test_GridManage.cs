using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_GridManage : MonoBehaviour
{
    public int _width, _height;

     public GameObject Tile;

    //private char Letters='A';

    void Start()
    {
        GenerateGrid();

    }

    void GenerateGrid()
    {
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                var spawnedTile = Instantiate(Tile, new Vector3(x, y,1)- (new Vector3((3.5f),(3.5f))), Quaternion.identity);
                spawnedTile.name = $"Tile {(x+1)} {y+1}"; 
                

            }
        }
    }



}
