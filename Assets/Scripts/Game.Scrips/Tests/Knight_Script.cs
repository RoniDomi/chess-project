using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kight_Script : MonoBehaviour
{
    public bool black;
    public bool white;
    public Transform position_;
    public int NrOfThisPiece;
    public bool Pressed = false;
    public bool Taken = false;
    public bool Stuck = false;

    public GameObject[] Tiles_To_Be_Selected;

    public GameObject Logic_Manager;

    public Logic_Management_Script logic_Manager_;

    public double position_x;
    public double position_y;
    public GameObject Tile_To_Go_To;
    public Test_Tile Tile_Im_On;


    void Awake()
    {
        logic_Manager_ = Logic_Manager.GetComponent<Logic_Management_Script>();
    }
    public void SetGame()
    {
        Tile_Im_On.NrOfPieceThatsOnMe = NrOfThisPiece;
    }


    // Start is called before the first frame update
    void Start()
    {
        Stuck = false;
        position_x = position_.position.x;
        position_y = position_.position.y;
    }


    public void OnMouseDown()
    {

        FindTileImOn();

        if (Tile_Im_On.Called)
        {
            Tile_Im_On.OnMouseDown();
            Pressed = false;
            Taken = true;
        }
        CheckIfStuck();
        if (((!logic_Manager_.White_Pressed && white) || (!logic_Manager_.Black_Pressed && black)) && !Taken && !Pressed && !Stuck)
        {
            Pressed = true;
            if (white)
                logic_Manager_.White_Pressed = true;
            else
                logic_Manager_.Black_Pressed = true;
            int x = 0;
            RookCallTiles(1, x);
            x = -2;
            RookCallTiles(-1, x);
            x = 7;
            RookCallTiles(8, x);
            x = -9;
            RookCallTiles(-8, x);

        }
        else if (Pressed && (logic_Manager_.White_Pressed || logic_Manager_.Black_Pressed))
        {
            Pressed = false;
            if (white)
                logic_Manager_.White_Pressed = false;
            else
                logic_Manager_.Black_Pressed = false;
            Tile_Im_On.UncallTiles();




        }
    }

    public void RookCallTiles(int i, int j)
    {
        Test_Tile TIle_Script;

        while (true)
        {
            if (i == 1)
            {
                if (Tile_Im_On.Vertical_Edge_Up)
                    break;
            }
            else if (i == -1)
            {

                if (Tile_Im_On.Vertical_Edge_Down)
                    break;
            }
            else if (i == 8)
            {

                if (Tile_Im_On.Horizontal_Edge_Right)
                    break;
            }
            else
            {

                if (Tile_Im_On.Horizontal_Edge_Left)
                    break;
            }
            GameObject xTile = FindTile(j);
            TIle_Script = xTile.GetComponent<Test_Tile>();
            if (black && TIle_Script.Occupy_Black || white && TIle_Script.Occupy_White)
                break;
            TIle_Script.TestFunction();
            TIle_Script.NrOfPawnThatCalledThisTile = NrOfThisPiece;
            TIle_Script.Selected = false;

            if ((white && TIle_Script.Occupy_Black) || (black && TIle_Script.Occupy_White))
            {
                if (white)
                    TIle_Script.Occupy_Black = false;
                else
                    TIle_Script.Occupy_White = false;
                break;

            }
            if (i == 1)
            {
                if (TIle_Script.Vertical_Edge_Up)
                    break;
            }
            else if (i == -1)
            {

                if (TIle_Script.Vertical_Edge_Down)
                    break;
            }
            else if (i == 8)
            {

                if (TIle_Script.Horizontal_Edge_Right)
                    break;
            }
            else
            {

                if (TIle_Script.Horizontal_Edge_Left)
                    break;
            }
            j += i;


        }
    }

    public void CheckIfStuck()
    {
        int conditions = 0;

        FindTileImOn();

        if (Tile_Im_On.position_.position.y < 3.5)
        {
            Tile_To_Go_To = FindTile(0);
            Test_Tile tile_to_go_to = Tile_To_Go_To.GetComponent<Test_Tile>();
            if ((tile_to_go_to.Occupy_White && white) || tile_to_go_to.Occupy_Black && black)
                conditions++;
        }
        else
            conditions++;
        if (Tile_Im_On.position_.position.y > -3.5)
        {
            Tile_To_Go_To = FindTile(-2);
            Test_Tile tile_to_go_to = Tile_To_Go_To.GetComponent<Test_Tile>();
            if ((tile_to_go_to.Occupy_White && white) || tile_to_go_to.Occupy_Black && black)
                conditions++;
        }
        else
            conditions++;
        if (Tile_Im_On.position_.position.x < 3.5)
        {
            Tile_To_Go_To = FindTile(7);
            Test_Tile tile_to_go_to = Tile_To_Go_To.GetComponent<Test_Tile>();
            if ((tile_to_go_to.Occupy_White && white) || tile_to_go_to.Occupy_Black && black)
                conditions++;
        }
        else
            conditions++;
        if (Tile_Im_On.position_.position.x > -3.5)
        {
            Tile_To_Go_To = FindTile(-9);
            Test_Tile tile_to_go_to = Tile_To_Go_To.GetComponent<Test_Tile>();
            if ((tile_to_go_to.Occupy_White && white) || tile_to_go_to.Occupy_Black && black)
                conditions++;
        }
        else
            conditions++;
        if (conditions == 4)
        {
            Stuck = true;
        }
        else
            Stuck = false;
    }

    public GameObject FindTile(int x)
    {

        GameObject[] Tiles_To_Be_Selected;
        Tiles_To_Be_Selected = GameObject.FindGameObjectsWithTag("Tag_Tile");

        return Tiles_To_Be_Selected[(int)(position_y + 4.2 + 0.15f + ((position_x + 5.2f) * 8) - 13) + x];
    }
    public void FindTileImOn()
    {

        Tile_To_Go_To = FindTile(-1);

        Tile_Im_On = Tile_To_Go_To.GetComponent<Test_Tile>();



    }
}