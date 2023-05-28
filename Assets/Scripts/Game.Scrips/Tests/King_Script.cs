using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King_Script : MonoBehaviour
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


    // Start is called before the first frame update
    void Start()
    {
        Stuck = false;
        position_x = position_.position.x;
        position_y = position_.position.y;
    }

    public void SetGame()
    {
        Tile_Im_On.NrOfPieceThatsOnMe = NrOfThisPiece;
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
        if (((!logic_Manager_.White_Pressed && white) || (!logic_Manager_.Black_Pressed && black)) && !Taken && !Pressed && !Stuck && ((white && logic_Manager_.White_Turn) || (black && logic_Manager_.Black_Turn)))
        {
            Pressed = true;
            if (white)
                logic_Manager_.White_Pressed = true;
            else
                logic_Manager_.Black_Pressed = true;
            int x = 0;
            if (!(Tile_Im_On.Vertical_Edge_Up))
                KingCallTiles(x);
            x = -2;
            if (!(Tile_Im_On.Vertical_Edge_Down))
                KingCallTiles(x);
            x = 7;
            if (!(Tile_Im_On.Horizontal_Edge_Right))
                KingCallTiles(x);
            x = -9;
            if (!(Tile_Im_On.Horizontal_Edge_Left))
                KingCallTiles(x);
            x = 8;
            if (!(Tile_Im_On.Vertical_Edge_Up || Tile_Im_On.Horizontal_Edge_Right))
                KingCallTiles(x);
            x = -8;
            if (!(Tile_Im_On.Vertical_Edge_Up || Tile_Im_On.Horizontal_Edge_Left))
                KingCallTiles(x);
            x = 6;
            if (!(Tile_Im_On.Horizontal_Edge_Right || Tile_Im_On.Vertical_Edge_Down))
                KingCallTiles(x);
            x = -10;
            if (!(Tile_Im_On.Horizontal_Edge_Left || Tile_Im_On.Vertical_Edge_Down))
                KingCallTiles(x);

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


   

    public void KingCallTiles(int x)
    {
        Test_Tile TIle_Script;

        
            
            GameObject xTile = FindTile(x);
            TIle_Script = xTile.GetComponent<Test_Tile>();
        if (((white && !TIle_Script.Attacked_Black) || (black && !TIle_Script.Attacked_White)) && !((TIle_Script.Occupy_White && white) || (TIle_Script.Occupy_Black && black)))
        {
            TIle_Script.TestFunction();
            TIle_Script.NrOfPawnThatCalledThisTile = NrOfThisPiece;
            TIle_Script.Selected = false;
        }

            if (((white && TIle_Script.Occupy_Black) || (black && TIle_Script.Occupy_White)) && ((white && !TIle_Script.Attacked_Black) || (black && !TIle_Script.Attacked_White)))
            {
                if (white)
                    TIle_Script.Occupy_Black = false;
                else
                    TIle_Script.Occupy_White = false;

            }
            

        
    }



    public void CheckIfStuck()
    {
        int conditions = 0;

        FindTileImOn();

        Test_Tile tile_to_go_to;

        if (Tile_Im_On.Vertical_Edge_Up)
        {
            conditions += 3;
            if (Tile_Im_On.Horizontal_Edge_Left)
            {
                conditions += 2;

                Tile_To_Go_To = FindTile(7);
                tile_to_go_to = Tile_To_Go_To.GetComponent<Test_Tile>();
                if ((tile_to_go_to.Occupy_White && white) || tile_to_go_to.Occupy_Black && black)
                    conditions++;

                Tile_To_Go_To = FindTile(-2);
                tile_to_go_to = Tile_To_Go_To.GetComponent<Test_Tile>();
                if ((tile_to_go_to.Occupy_White && white) || tile_to_go_to.Occupy_Black && black)
                    conditions++;

                Tile_To_Go_To = FindTile(6);
                tile_to_go_to = Tile_To_Go_To.GetComponent<Test_Tile>();
                if ((tile_to_go_to.Occupy_White && white) || tile_to_go_to.Occupy_Black && black)
                    conditions++;
            }
            else if (Tile_Im_On.Horizontal_Edge_Right)
            {
                conditions += 2;

                Tile_To_Go_To = FindTile(-9);
                tile_to_go_to = Tile_To_Go_To.GetComponent<Test_Tile>();
                if ((tile_to_go_to.Occupy_White && white) || tile_to_go_to.Occupy_Black && black)
                    conditions++;
                Tile_To_Go_To = FindTile(-2);
                tile_to_go_to = Tile_To_Go_To.GetComponent<Test_Tile>();
                if ((tile_to_go_to.Occupy_White && white) || tile_to_go_to.Occupy_Black && black)
                    conditions++;
                Tile_To_Go_To = FindTile(-10);
                tile_to_go_to = Tile_To_Go_To.GetComponent<Test_Tile>();
                if ((tile_to_go_to.Occupy_White && white) || tile_to_go_to.Occupy_Black && black)
                    conditions++;
            }
            else
            {

                Tile_To_Go_To = FindTile(-9);
                tile_to_go_to = Tile_To_Go_To.GetComponent<Test_Tile>();
                if ((tile_to_go_to.Occupy_White && white) || tile_to_go_to.Occupy_Black && black)
                    conditions++;
                Tile_To_Go_To = FindTile(-10);
                tile_to_go_to = Tile_To_Go_To.GetComponent<Test_Tile>();
                if ((tile_to_go_to.Occupy_White && white) || tile_to_go_to.Occupy_Black && black)
                    conditions++;
                Tile_To_Go_To = FindTile(7);
                tile_to_go_to = Tile_To_Go_To.GetComponent<Test_Tile>();
                if ((tile_to_go_to.Occupy_White && white) || tile_to_go_to.Occupy_Black && black)
                    conditions++;

                Tile_To_Go_To = FindTile(-2);
                tile_to_go_to = Tile_To_Go_To.GetComponent<Test_Tile>();
                if ((tile_to_go_to.Occupy_White && white) || tile_to_go_to.Occupy_Black && black)
                    conditions++;

                Tile_To_Go_To = FindTile(6);
                tile_to_go_to = Tile_To_Go_To.GetComponent<Test_Tile>();
                if ((tile_to_go_to.Occupy_White && white) || tile_to_go_to.Occupy_Black && black)
                    conditions++;
            }

        }

        else if (Tile_Im_On.Vertical_Edge_Down)
        {
            conditions += 3;
            if (Tile_Im_On.Horizontal_Edge_Left)
            {
                conditions += 2;

                Tile_To_Go_To = FindTile(0);
                tile_to_go_to = Tile_To_Go_To.GetComponent<Test_Tile>();
                if ((tile_to_go_to.Occupy_White && white) || tile_to_go_to.Occupy_Black && black)
                    conditions++;
                Tile_To_Go_To = FindTile(7);
                tile_to_go_to = Tile_To_Go_To.GetComponent<Test_Tile>();
                if ((tile_to_go_to.Occupy_White && white) || tile_to_go_to.Occupy_Black && black)
                    conditions++;

                Tile_To_Go_To = FindTile(8);
                tile_to_go_to = Tile_To_Go_To.GetComponent<Test_Tile>();
                if ((tile_to_go_to.Occupy_White && white) || tile_to_go_to.Occupy_Black && black)
                    conditions++;

            }
            else if (Tile_Im_On.Horizontal_Edge_Right)
            {
                conditions += 2;
                Tile_To_Go_To = FindTile(-8);
                tile_to_go_to = Tile_To_Go_To.GetComponent<Test_Tile>();
                if ((tile_to_go_to.Occupy_White && white) || tile_to_go_to.Occupy_Black && black)
                    conditions++;
                Tile_To_Go_To = FindTile(0);
                tile_to_go_to = Tile_To_Go_To.GetComponent<Test_Tile>();
                if ((tile_to_go_to.Occupy_White && white) || tile_to_go_to.Occupy_Black && black)
                    conditions++;
                Tile_To_Go_To = FindTile(-9);
                tile_to_go_to = Tile_To_Go_To.GetComponent<Test_Tile>();
                if ((tile_to_go_to.Occupy_White && white) || tile_to_go_to.Occupy_Black && black)
                    conditions++;
            }
            else
            {

                Tile_To_Go_To = FindTile(8);
                tile_to_go_to = Tile_To_Go_To.GetComponent<Test_Tile>();
                if ((tile_to_go_to.Occupy_White && white) || tile_to_go_to.Occupy_Black && black)
                    conditions++;
                Tile_To_Go_To = FindTile(-8);
                tile_to_go_to = Tile_To_Go_To.GetComponent<Test_Tile>();
                if ((tile_to_go_to.Occupy_White && white) || tile_to_go_to.Occupy_Black && black)
                    conditions++;
                Tile_To_Go_To = FindTile(7);
                tile_to_go_to = Tile_To_Go_To.GetComponent<Test_Tile>();
                if ((tile_to_go_to.Occupy_White && white) || tile_to_go_to.Occupy_Black && black)
                    conditions++;
                Tile_To_Go_To = FindTile(-9);
                tile_to_go_to = Tile_To_Go_To.GetComponent<Test_Tile>();
                if ((tile_to_go_to.Occupy_White && white) || tile_to_go_to.Occupy_Black && black)
                    conditions++;
                Tile_To_Go_To = FindTile(0);
                tile_to_go_to = Tile_To_Go_To.GetComponent<Test_Tile>();
                if ((tile_to_go_to.Occupy_White && white) || tile_to_go_to.Occupy_Black && black)
                    conditions++;
            }



        }
        else if (Tile_Im_On.Horizontal_Edge_Right)
        {
            conditions += 3;
            Tile_To_Go_To = FindTile(-8);
            tile_to_go_to = Tile_To_Go_To.GetComponent<Test_Tile>();
            if ((tile_to_go_to.Occupy_White && white) || tile_to_go_to.Occupy_Black && black)
                conditions++;
            Tile_To_Go_To = FindTile(-10);
            tile_to_go_to = Tile_To_Go_To.GetComponent<Test_Tile>();
            if ((tile_to_go_to.Occupy_White && white) || tile_to_go_to.Occupy_Black && black)
                conditions++;
            Tile_To_Go_To = FindTile(0);
            tile_to_go_to = Tile_To_Go_To.GetComponent<Test_Tile>();
            if ((tile_to_go_to.Occupy_White && white) || tile_to_go_to.Occupy_Black && black)
                conditions++;
            Tile_To_Go_To = FindTile(-2);
            tile_to_go_to = Tile_To_Go_To.GetComponent<Test_Tile>();
            if ((tile_to_go_to.Occupy_White && white) || tile_to_go_to.Occupy_Black && black)
                conditions++;
            Tile_To_Go_To = FindTile(-9);
            tile_to_go_to = Tile_To_Go_To.GetComponent<Test_Tile>();
            if ((tile_to_go_to.Occupy_White && white) || tile_to_go_to.Occupy_Black && black)
                conditions++;

        }
        else if (Tile_Im_On.Horizontal_Edge_Left)
        {
            conditions += 3;
            Tile_To_Go_To = FindTile(6);
            tile_to_go_to = Tile_To_Go_To.GetComponent<Test_Tile>();
            if ((tile_to_go_to.Occupy_White && white) || tile_to_go_to.Occupy_Black && black)
                conditions++;

            Tile_To_Go_To = FindTile(8);
            tile_to_go_to = Tile_To_Go_To.GetComponent<Test_Tile>();
            if ((tile_to_go_to.Occupy_White && white) || tile_to_go_to.Occupy_Black && black)
                conditions++;
            Tile_To_Go_To = FindTile(0);
            tile_to_go_to = Tile_To_Go_To.GetComponent<Test_Tile>();
            if ((tile_to_go_to.Occupy_White && white) || tile_to_go_to.Occupy_Black && black)
                conditions++;
            Tile_To_Go_To = FindTile(-2);
            tile_to_go_to = Tile_To_Go_To.GetComponent<Test_Tile>();
            if ((tile_to_go_to.Occupy_White && white) || tile_to_go_to.Occupy_Black && black)
                conditions++;
            Tile_To_Go_To = FindTile(7);
            tile_to_go_to = Tile_To_Go_To.GetComponent<Test_Tile>();
            if ((tile_to_go_to.Occupy_White && white) || tile_to_go_to.Occupy_Black && black)
                conditions++;
        }
        else
        {
            Tile_To_Go_To = FindTile(-9);
            tile_to_go_to = Tile_To_Go_To.GetComponent<Test_Tile>();
            if ((tile_to_go_to.Occupy_White && white) || tile_to_go_to.Occupy_Black && black)
                conditions++;
            Tile_To_Go_To = FindTile(6);
            tile_to_go_to = Tile_To_Go_To.GetComponent<Test_Tile>();
            if ((tile_to_go_to.Occupy_White && white) || tile_to_go_to.Occupy_Black && black)
                conditions++;

            Tile_To_Go_To = FindTile(8);
            tile_to_go_to = Tile_To_Go_To.GetComponent<Test_Tile>();
            if ((tile_to_go_to.Occupy_White && white) || tile_to_go_to.Occupy_Black && black)
                conditions++;
            Tile_To_Go_To = FindTile(0);
            tile_to_go_to = Tile_To_Go_To.GetComponent<Test_Tile>();
            if ((tile_to_go_to.Occupy_White && white) || tile_to_go_to.Occupy_Black && black)
                conditions++;
            Tile_To_Go_To = FindTile(-2);
            tile_to_go_to = Tile_To_Go_To.GetComponent<Test_Tile>();
            if ((tile_to_go_to.Occupy_White && white) || tile_to_go_to.Occupy_Black && black)
                conditions++;
            Tile_To_Go_To = FindTile(7);
            tile_to_go_to = Tile_To_Go_To.GetComponent<Test_Tile>();
            if ((tile_to_go_to.Occupy_White && white) || tile_to_go_to.Occupy_Black && black)
                conditions++;
            Tile_To_Go_To = FindTile(-10);
            tile_to_go_to = Tile_To_Go_To.GetComponent<Test_Tile>();
            if ((tile_to_go_to.Occupy_White && white) || tile_to_go_to.Occupy_Black && black)
                conditions++;
            Tile_To_Go_To = FindTile(-8);
            tile_to_go_to = Tile_To_Go_To.GetComponent<Test_Tile>();
            if ((tile_to_go_to.Occupy_White && white) || tile_to_go_to.Occupy_Black && black)
                conditions++;
        }


        if (conditions == 8)
        {
            Stuck = true;
        }
        else
            Stuck = false;
    
    }

    void attack()
    {

    }

    public GameObject FindTile(int x)
    {

        GameObject[] Tiles_To_Be_Selected;
        Tiles_To_Be_Selected = GameObject.FindGameObjectsWithTag("Tag_Tile");

        return Tiles_To_Be_Selected[(int)(position_y + 4.2 + 0.25f + ((position_x + 5.2f) * 8) - 13) + x];
    }
    public void FindTileImOn()
    {

        Tile_To_Go_To = FindTile(-1);

        Tile_Im_On = Tile_To_Go_To.GetComponent<Test_Tile>();



    }
}
