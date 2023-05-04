using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing_Movement : MonoBehaviour
{
    public Transform position_;

    public Test_Tile tile_to_go_to;

    public int NrOfThisPawn;

    public bool FirstMove = true;

    public void Start()
    {
        NrOfThisPawn = (int)(position_.position.x + 4.2);
    }

    void OnMouseDown()
    {
        Debug.Log("Pressed A-Pawn");

       
        GameObject Tile_To_Go_To = FindTile(0);

        tile_to_go_to = Tile_To_Go_To.GetComponent<Test_Tile>();

        if (tile_to_go_to.Selected)
        {
            tile_to_go_to.Selected = false;
        }
        else
        {
            tile_to_go_to.Selected = true;
        }
        tile_to_go_to.NrOfPawnThatCalledThisTile = NrOfThisPawn;

        tile_to_go_to.TestFunction();

        //if(FirstMove)
        //{
           // Tile_To_Go_To = FindTile(1);
            //tile_to_go_to = Tile_To_Go_To.GetComponent<Test_Tile>();
            //if (tile_to_go_to.Selected)
            //{
              //  tile_to_go_to.Selected = false;
            //}
            //else
            //{
              //  tile_to_go_to.Selected = true;
            //}
            //tile_to_go_to.NrOfPawnThatCalledThisTile = NrOfThisPawn;

            //tile_to_go_to.TestFunction();

        //}
        FirstMove = false;
    }

    public GameObject FindTile(int x)
    {

        GameObject[] Tiles_To_Be_Selected;
        Tiles_To_Be_Selected = GameObject.FindGameObjectsWithTag("Tag_Tile");
        
        return Tiles_To_Be_Selected[(int)(position_.position.y+4.2 + ((position_.position.x + 5.2f)*8)-13)+x];
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
