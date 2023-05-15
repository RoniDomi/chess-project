using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rook_Script : MonoBehaviour
{
    public int NrOfThisPiece;
    public bool Pressed = false;

    public bool Stuck = false;

    public GameObject[] Tiles_To_Be_Selected;

    // Start is called before the first frame update
    void Start()
    {
        Tiles_To_Be_Selected = GameObject.FindGameObjectsWithTag("Tag_Tile");


    }

    public void OnMouseDown()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
