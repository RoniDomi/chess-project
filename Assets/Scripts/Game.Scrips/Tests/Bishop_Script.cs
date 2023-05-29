using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bishop_Script : MonoBehaviour
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
   public  void SetGame()
    {
        Tile_Im_On.NrOfPieceThatsOnMe = NrOfThisPiece;
    }



    public void OnMouseDown()
    {

        FindTileImOn();

        if (Tile_Im_On.Called)
        {
           Tile_Im_On.NrOfPieceThatsOnMe = NrOfThisPiece;
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
            int x = 8;
            if (!(Tile_Im_On.Vertical_Edge_Up || Tile_Im_On.Horizontal_Edge_Right))
                BishopCallTiles(9, x);
            x = -8;
            if (!(Tile_Im_On.Vertical_Edge_Up || Tile_Im_On.Horizontal_Edge_Left))
                BishopCallTiles(-7, x);
            x = 6;
            if (!(Tile_Im_On.Horizontal_Edge_Right || Tile_Im_On.Vertical_Edge_Down))
                BishopCallTiles(7, x);
            x = -10;
            if (!(Tile_Im_On.Horizontal_Edge_Left || Tile_Im_On.Vertical_Edge_Down))
                BishopCallTiles(-9, x);

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

    public void BishopCallTiles(int i, int j)
    {
        Test_Tile TIle_Script;

        while (true)
        {
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
            if (i == 9)
            {
                if (TIle_Script.Vertical_Edge_Up || TIle_Script.Horizontal_Edge_Right)
                    break;
            }
            else if (i == -7)
            {

                if (TIle_Script.Vertical_Edge_Up || TIle_Script.Horizontal_Edge_Left)
                    break;
            }
            else if (i == 7)
            {

                if (TIle_Script.Horizontal_Edge_Right || TIle_Script.Vertical_Edge_Down)
                    break;
            }
            else
            {

                if (TIle_Script.Horizontal_Edge_Left || TIle_Script.Vertical_Edge_Down)
                    break;
            }
            j += i;


        }
    }

    public void CheckIfStuck()
    {
        int conditions = 0;

        FindTileImOn();

        Test_Tile tile_to_go_to;

        if (Tile_Im_On.Vertical_Edge_Up)
        {
            conditions += 2;
            if (Tile_Im_On.Horizontal_Edge_Left)
            {
                conditions++;

                Tile_To_Go_To = FindTile(6);
                tile_to_go_to = Tile_To_Go_To.GetComponent<Test_Tile>();
                if ((tile_to_go_to.Occupy_White && white) || tile_to_go_to.Occupy_Black && black)
                    conditions++;
            }
            else if (Tile_Im_On.Horizontal_Edge_Right)
            {
                conditions++;
                Tile_To_Go_To = FindTile(-10);
                tile_to_go_to = Tile_To_Go_To.GetComponent<Test_Tile>();
                if ((tile_to_go_to.Occupy_White && white) || tile_to_go_to.Occupy_Black && black)
                    conditions++;
            }
            else
            {

                Tile_To_Go_To = FindTile(6);
                tile_to_go_to = Tile_To_Go_To.GetComponent<Test_Tile>();
                if ((tile_to_go_to.Occupy_White && white) || tile_to_go_to.Occupy_Black && black)
                    conditions++;
                Tile_To_Go_To = FindTile(-10);
                tile_to_go_to = Tile_To_Go_To.GetComponent<Test_Tile>();
                if ((tile_to_go_to.Occupy_White && white) || tile_to_go_to.Occupy_Black && black)
                    conditions++;
            }

        }

        if (Tile_Im_On.Vertical_Edge_Down)
        {
            conditions += 2;
            if (Tile_Im_On.Horizontal_Edge_Left)
            {
                conditions++;

                Tile_To_Go_To = FindTile(8);
                tile_to_go_to = Tile_To_Go_To.GetComponent<Test_Tile>();
                if ((tile_to_go_to.Occupy_White && white) || tile_to_go_to.Occupy_Black && black)
                    conditions++;
            }
            else if (Tile_Im_On.Horizontal_Edge_Right)
            {
                conditions++;
                Tile_To_Go_To = FindTile(-8);
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
            }



        }
        if (Tile_Im_On.Horizontal_Edge_Right)
        {
            conditions += 2;
            Tile_To_Go_To = FindTile(-8);
           tile_to_go_to = Tile_To_Go_To.GetComponent<Test_Tile>();
            if ((tile_to_go_to.Occupy_White && white) || tile_to_go_to.Occupy_Black && black)
                conditions++;
            Tile_To_Go_To = FindTile(-10);
           tile_to_go_to = Tile_To_Go_To.GetComponent<Test_Tile>();
            if ((tile_to_go_to.Occupy_White && white) || tile_to_go_to.Occupy_Black && black)
                conditions++;

        }
        if (Tile_Im_On.Horizontal_Edge_Left)
        {
            conditions += 2;
            Tile_To_Go_To = FindTile(6);
            tile_to_go_to = Tile_To_Go_To.GetComponent<Test_Tile>();
            if ((tile_to_go_to.Occupy_White && white) || tile_to_go_to.Occupy_Black && black)
                conditions++;

            Tile_To_Go_To = FindTile(8);
              tile_to_go_to = Tile_To_Go_To.GetComponent<Test_Tile>();
            if ((tile_to_go_to.Occupy_White && white) || tile_to_go_to.Occupy_Black && black)
                conditions++;
        }
        
        if (conditions == 4)
        {
            Stuck = true;
        }
        else
            Stuck = false;
    }
   public void attack()
    {
        Test_Tile TIle_Script;
        int x;
        bool secondary = false;

        FindTileImOn();
        x = 8;
        if (!(Tile_Im_On.Vertical_Edge_Up || Tile_Im_On.Horizontal_Edge_Right))
        {

            while (true)
            {
                GameObject xTile = FindTile(x);
                TIle_Script = xTile.GetComponent<Test_Tile>();
                if (secondary)
                {
                    if (white)
                        TIle_Script.Attacked_White_Secondary = true;
                    else
                        TIle_Script.Attacked_Black_Secondary = true;
                    if ((black && TIle_Script.Occupy_White) || (white && TIle_Script.Occupy_Black))
                    {
                        break;
                    }
                }
                else
                {

                    if (white)
                        TIle_Script.Attacked_White = true;
                    else
                        TIle_Script.Attacked_Black = true;
                }
                if ((black && TIle_Script.Occupy_White) || (white && TIle_Script.Occupy_Black) && !TIle_Script.king)
                {
                    secondary = true;
                }
                if (white && TIle_Script.Occupy_White || black && TIle_Script.Occupy_Black && !TIle_Script.king)
                    break;
                if (TIle_Script.Vertical_Edge_Up || TIle_Script.Horizontal_Edge_Right)
                    break;
                x += 9;
            }
        }
        secondary = false;
        x = -8;
        if (!(Tile_Im_On.Vertical_Edge_Up || Tile_Im_On.Horizontal_Edge_Left))
        {

            while (true)
            {
                GameObject xTile = FindTile(x);
                TIle_Script = xTile.GetComponent<Test_Tile>();
                if (secondary)
                {
                    if (white)
                        TIle_Script.Attacked_White_Secondary = true;
                    else
                        TIle_Script.Attacked_Black_Secondary = true;
                    if ((black && TIle_Script.Occupy_White) || (white && TIle_Script.Occupy_Black))
                    {
                        break;
                    }
                }
                else
                {

                    if (white)
                        TIle_Script.Attacked_White = true;
                    else
                        TIle_Script.Attacked_Black = true;
                }
                if (((black && TIle_Script.Occupy_White) || (white && TIle_Script.Occupy_Black)) && !TIle_Script.king)
                {
                    secondary = true;
                }
                if (((white && TIle_Script.Occupy_White) ||(black && TIle_Script.Occupy_Black)) && !TIle_Script.king)
                    break;
                if (TIle_Script.Vertical_Edge_Up || TIle_Script.Horizontal_Edge_Left)
                    break;
                x += -7;
            }
        }
        secondary = false;
        x = 6;
        if (!(Tile_Im_On.Horizontal_Edge_Right || Tile_Im_On.Vertical_Edge_Down))
        {

            while (true)
            {
                GameObject xTile = FindTile(x);
                TIle_Script = xTile.GetComponent<Test_Tile>();
                if (secondary)
                {
                    if (white)
                        TIle_Script.Attacked_White_Secondary = true;
                    else
                        TIle_Script.Attacked_Black_Secondary = true;
                    if ((black && TIle_Script.Occupy_White) || (white && TIle_Script.Occupy_Black))
                    {
                        break;
                    }
                }
                else
                {

                    if (white)
                        TIle_Script.Attacked_White = true;
                    else
                        TIle_Script.Attacked_Black = true;
                }
                if (((black && TIle_Script.Occupy_White) || (white && TIle_Script.Occupy_Black)) && !TIle_Script.king)
                {
                    secondary = true;
                }
                if (((white && TIle_Script.Occupy_White) || (black && TIle_Script.Occupy_Black)) && !TIle_Script.king)
                    break;
                if (TIle_Script.Horizontal_Edge_Right || TIle_Script.Vertical_Edge_Down)
                    break;
                x += 7;
            }
        }
        secondary = false;
        x = -10;
        if (!(Tile_Im_On.Horizontal_Edge_Left || Tile_Im_On.Vertical_Edge_Down))
        {

            while (true)
            {
                GameObject xTile = FindTile(x);
                TIle_Script = xTile.GetComponent<Test_Tile>();
                if (secondary)
                {
                    if (white)
                        TIle_Script.Attacked_White_Secondary = true;
                    else
                        TIle_Script.Attacked_Black_Secondary = true;
                    if ((black && TIle_Script.Occupy_White) || (white && TIle_Script.Occupy_Black))
                    {
                        break;
                    }
                }
                else
                {

                    if (white)
                        TIle_Script.Attacked_White = true;
                    else
                        TIle_Script.Attacked_Black = true;
                }
                if (((black && TIle_Script.Occupy_White) || (white && TIle_Script.Occupy_Black)) && !TIle_Script.king)
                {
                    secondary = true;
                }
                if (((white && TIle_Script.Occupy_White) || (black && TIle_Script.Occupy_Black)) && !TIle_Script.king)
                    break;
                if (TIle_Script.Horizontal_Edge_Left || TIle_Script.Vertical_Edge_Down)
                    break;
                x += -9;
            }
        }
    }

    public GameObject FindTile(int x)
    {

        GameObject[] Tiles_To_Be_Selected;
        Tiles_To_Be_Selected = GameObject.FindGameObjectsWithTag("Tag_Tile");

        return Tiles_To_Be_Selected[(int)(position_y + 4.2 + 0.1275f + ((position_x + 5.2f) * 8) - 13) + x];
    }
    public void FindTileImOn()
    {

        Tile_To_Go_To = FindTile(-1);

        Tile_Im_On = Tile_To_Go_To.GetComponent<Test_Tile>();



    }
}