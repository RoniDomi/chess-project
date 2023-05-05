using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing_Movement : MonoBehaviour
{
    public bool Pressed = false;

    public int NrOfThisPawn_x;
    public int NrOfThisPawn_y;

    public Testing_Movement Pawnscript;

    public GameObject[] AllPawns;

    public Transform position_;

    public Test_Tile tile_to_go_to;

    public int NrOfThisPawn;

    public bool FirstMove = true;

    public GameObject Tile_To_Go_To;

    public Test_Tile Tile_Im_On;

    public void Start()
    {
        NrOfThisPawn = (int)(position_.position.x + 4.2);

        Tile_To_Go_To = FindTile(0);


        tile_to_go_to = Tile_To_Go_To.GetComponent<Test_Tile>();

        NrOfThisPawn_x = tile_to_go_to.NrOfThisTile_x;
        NrOfThisPawn_y = tile_to_go_to.NrOfThisTile_y;
    }

    void OnMouseDown()
    {
        if (OnlyOnePressed())
        {
            Tile_To_Go_To= FindTile(-1);
            
            Tile_Im_On=Tile_To_Go_To.GetComponent<Test_Tile>();



            Pressed = true;
            Debug.Log("Pressed A-Pawn");


            Tile_To_Go_To = FindTile(0);


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


            if (FirstMove)
            {
                Tile_To_Go_To = FindTile(1);
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


            }


        }
        else
        {
            Tile_To_Go_To = FindTile(0);
            tile_to_go_to = Tile_To_Go_To.GetComponent<Test_Tile>();
            tile_to_go_to.Selected = true;
            tile_to_go_to.Called = false;
            tile_to_go_to._renderer.color = tile_to_go_to._SavedColor;
            Tile_To_Go_To = FindTile(1);
            tile_to_go_to = Tile_To_Go_To.GetComponent<Test_Tile>();
            tile_to_go_to.Selected = true;
            tile_to_go_to.Called = false;
            tile_to_go_to._renderer.color = tile_to_go_to._SavedColor;
            Pressed = false;
        }
    }



    public GameObject FindTile(int x)
    {

        GameObject[] Tiles_To_Be_Selected;
        Tiles_To_Be_Selected = GameObject.FindGameObjectsWithTag("Tag_Tile");
        
        return Tiles_To_Be_Selected[(int)(position_.position.y+4.2 + ((position_.position.x + 5.2f)*8)-13)+x];
    }

    public bool OnlyOnePressed()
    {

        AllPawns = GameObject.FindGameObjectsWithTag("Pawns");

        for (int x = 0; x < 8; x++)
        {
            GameObject xPawn = AllPawns[x];
            Pawnscript = xPawn.GetComponent<Testing_Movement>();
            if (Pawnscript.Pressed)
            {
                return false;
            }
        }

        return true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
