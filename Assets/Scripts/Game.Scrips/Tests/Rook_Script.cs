using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rook_Script : MonoBehaviour
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
    public bool Castling;
    public bool Pinned;
    public bool Pinned_By_Bishop = false;


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
        Castling = true;
    }


    public void OnMouseDown()
    {

        FindTileImOn();

        if(Tile_Im_On.Called)
        {
            Tile_Im_On.OnMouseDown();
            Pressed = false;
            Taken = true;
        }
        CheckIfStuck();
        if (((!logic_Manager_.White_Pressed && white) || (!logic_Manager_.Black_Pressed && black)) && !Taken && !Pressed && !Stuck && ((white && logic_Manager_.White_Turn) || (black && logic_Manager_.Black_Turn)))
        {
            GameObject xTile;
            Test_Tile TIle_Script;
            int y = 1;
            Pressed = true;
            if (white)
            logic_Manager_.White_Pressed = true;
            else
                logic_Manager_.Black_Pressed = true;
            int x = 0;
            if (!(Tile_Im_On.Vertical_Edge_Up))
            {
                xTile = FindTile(x);
                TIle_Script = xTile.GetComponent<Test_Tile>();
                if (Pinned)
                {
                    if (TIle_Script.pinnedTile_White && white || black && TIle_Script.pinnedTile_Black)
                        RookCallTiles(y, x);
                }
                else if (Pinned_By_Bishop)
                {
                    if (TIle_Script.pinnedTile_White_Bishop && white || black && TIle_Script.pinnedTile_Black_Bishop)
                        RookCallTiles(y, x);
                }
                else
                    RookCallTiles(y, x);
            }
            x = -2;
            y = -1;
            if (!(Tile_Im_On.Vertical_Edge_Down))
            {
                xTile = FindTile(x);
                TIle_Script = xTile.GetComponent<Test_Tile>();
                if (Pinned)
                {
                    if (TIle_Script.pinnedTile_White && white || black && TIle_Script.pinnedTile_Black)
                        RookCallTiles(y, x);
                }
                else if (Pinned_By_Bishop)
                {
                    if (TIle_Script.pinnedTile_White_Bishop && white || black && TIle_Script.pinnedTile_Black_Bishop)
                        RookCallTiles(y, x);
                }
                else
                    RookCallTiles(y, x);
            }
            x = 7;
            y = 8;
            if (!(Tile_Im_On.Horizontal_Edge_Right))
            {
                xTile = FindTile(x);
                TIle_Script = xTile.GetComponent<Test_Tile>();
                if (Pinned)
                {
                    if (TIle_Script.pinnedTile_White && white || black && TIle_Script.pinnedTile_Black)
                        RookCallTiles(y, x);
                }
                else if (Pinned_By_Bishop)
                {
                    if (TIle_Script.pinnedTile_White_Bishop && white || black && TIle_Script.pinnedTile_Black_Bishop)
                        RookCallTiles(y, x);
                }
                else
                    RookCallTiles(y, x);
            }
            x = -9;
            y = -8;
            if (!(Tile_Im_On.Horizontal_Edge_Left))
            {
                xTile = FindTile(x);
                TIle_Script = xTile.GetComponent<Test_Tile>();
                if (Pinned)
                {
                    if (TIle_Script.pinnedTile_White && white || black && TIle_Script.pinnedTile_Black)
                        RookCallTiles(y, x);
                }
                else if (Pinned_By_Bishop)
                {
                    if (TIle_Script.pinnedTile_White_Bishop && white || black && TIle_Script.pinnedTile_Black_Bishop)
                        RookCallTiles(y, x);
                }
                else
                    RookCallTiles(y, x);
            }

        }
        else if(Pressed && (logic_Manager_.White_Pressed  || logic_Manager_.Black_Pressed))
        {
            Pressed = false;
            if(white)
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
           
            GameObject xTile = FindTile(j);
            TIle_Script = xTile.GetComponent<Test_Tile>();
            if (black && TIle_Script.Occupy_Black || white && TIle_Script.Occupy_White)
                break;

            if ((logic_Manager_.check_white && white || logic_Manager_.check_black && black) && !Taken)
            {
                if ((TIle_Script.Checked_White && white) || (TIle_Script.Checked_Black && black))
                {

                    TIle_Script.TestFunction();
                    TIle_Script.NrOfPawnThatCalledThisTile = NrOfThisPiece;
                    TIle_Script.Selected = false;
                    Stuck = false;

                    if ((white && TIle_Script.Occupy_Black) || (black && TIle_Script.Occupy_White))
                    {
                        if (white)
                            TIle_Script.Occupy_Black = false;
                        else
                            TIle_Script.Occupy_White = false;
                        break;

                    }
                }
            }
            else if (!Taken)
            {
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

            }


            if (i == 1)
            {
                if (TIle_Script.Vertical_Edge_Up)
                    break;
            }else if(i==-1)
            {

                if (TIle_Script.Vertical_Edge_Down)
                    break;
            }
            else if(i==8)
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

       
        if ((white && Tile_Im_On.pinnedTile_White) || (black && Tile_Im_On.pinnedTile_Black))
        {
            Pinned = true;
            Pinned_By_Bishop = false;
        }
        else if ((white && Tile_Im_On.pinnedTile_White_Bishop) || (black && Tile_Im_On.pinnedTile_Black_Bishop))
        {
            Pinned_By_Bishop = true;
            Pinned = false;
        }
        else
        {
            Pinned_By_Bishop = false;
            Pinned = false;
        }

        if (Tile_Im_On.position_.position.y < 3.5)
        {
            Tile_To_Go_To = FindTile(0);
            Test_Tile tile_to_go_to = Tile_To_Go_To.GetComponent<Test_Tile>();
            if (((tile_to_go_to.Occupy_White || (logic_Manager_.check_white && !tile_to_go_to.Checked_White)) && white) || (tile_to_go_to.Occupy_Black || ((logic_Manager_.check_black && !tile_to_go_to.Checked_Black)) && black) || Pinned_By_Bishop)
                conditions++;
        }
        else
            conditions++;
        if (Tile_Im_On.position_.position.y > -3.5)
        {
            Tile_To_Go_To = FindTile(-2);
            Test_Tile tile_to_go_to = Tile_To_Go_To.GetComponent<Test_Tile>();
            if (((tile_to_go_to.Occupy_White || (logic_Manager_.check_white && !tile_to_go_to.Checked_White)) && white) || (tile_to_go_to.Occupy_Black || ((logic_Manager_.check_black && !tile_to_go_to.Checked_Black)) && black) || Pinned_By_Bishop)
                conditions++;
        }
        else
            conditions++;
        if (Tile_Im_On.position_.position.x < 3.5)
        {
            Tile_To_Go_To = FindTile(7);
            Test_Tile tile_to_go_to = Tile_To_Go_To.GetComponent<Test_Tile>();
            if (((tile_to_go_to.Occupy_White || (logic_Manager_.check_white && !tile_to_go_to.Checked_White)) && white) || (tile_to_go_to.Occupy_Black || ((logic_Manager_.check_black && !tile_to_go_to.Checked_Black)) && black) || Pinned_By_Bishop)
                conditions++;
        }
        else
            conditions++;
        if (Tile_Im_On.position_.position.x > -3.5)
        {
            Tile_To_Go_To = FindTile(-9);
            Test_Tile tile_to_go_to = Tile_To_Go_To.GetComponent<Test_Tile>();
            if (((tile_to_go_to.Occupy_White || (logic_Manager_.check_white && !tile_to_go_to.Checked_White)) && white) || (tile_to_go_to.Occupy_Black || ((logic_Manager_.check_black && !tile_to_go_to.Checked_Black)) && black) || Pinned_By_Bishop)
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
        if (white && !logic_Manager_.double_check_white || black && !logic_Manager_.double_check_black)
            undostuck();
        if (white && logic_Manager_.double_check_white || black && logic_Manager_.double_check_black)
            Stuck = true;
    }

    public void attack()
    {
        Test_Tile TIle_Script;
        int x;
        bool secondary = false;

        FindTileImOn();

        if (!(Tile_Im_On.Vertical_Edge_Up))
        {
            x = 0;
            while (true)
            {
                GameObject xTile = FindTile(x);
                TIle_Script = xTile.GetComponent<Test_Tile>();
                if (secondary)
                {
                    if (white)
                    {
                        TIle_Script.Attacked_White_Secondary = true;
                        TIle_Script.canbepinned_white = true;
                    }
                    else
                    {
                        TIle_Script.Attacked_Black_Secondary = true;
                        TIle_Script.canbepinned_black = true;
                    }
                    if ((black && TIle_Script.Occupy_White) || (white && TIle_Script.Occupy_Black))
                    {
                        break;
                    }
                }
                else
                {

                    if (white)
                    {
                        TIle_Script.Attacked_White = true;
                        TIle_Script.canbepinned_white = true;
                    }
                    else
                    {
                        TIle_Script.Attacked_Black = true;
                        TIle_Script.canbepinned_black = true;
                    }
                }
                if (((black && TIle_Script.Occupy_White) || (white && TIle_Script.Occupy_Black)) )
                {
                    secondary = true;
                }
                if (((white && TIle_Script.Occupy_White) || (black && TIle_Script.Occupy_Black)))
                    break;
                if (TIle_Script.Vertical_Edge_Up)
                    break;
                x++;
            }
        }
        secondary = false;
        x = -2;
        if (!(Tile_Im_On.Vertical_Edge_Down))
        {

            while (true)
            {
                GameObject xTile = FindTile(x);
                TIle_Script = xTile.GetComponent<Test_Tile>();
                if (secondary)
                {
                    if (white)
                    {
                        TIle_Script.Attacked_White_Secondary = true;
                        TIle_Script.canbepinned_white = true;
                    }
                    else
                    {
                        TIle_Script.Attacked_Black_Secondary = true;
                        TIle_Script.canbepinned_black = true;
                    }
                    if ((black && TIle_Script.Occupy_White) || (white && TIle_Script.Occupy_Black))
                    {
                        break;
                    }
                }
                else
                {
                    if (white)
                    {
                        TIle_Script.Attacked_White = true;
                        TIle_Script.canbepinned_white = true;
                    }
                    else
                    {
                        TIle_Script.Attacked_Black = true;
                        TIle_Script.canbepinned_black = true;
                    }
                }
                if (((black && TIle_Script.Occupy_White) || (white && TIle_Script.Occupy_Black)))
                {
                    secondary = true;
                }
                if (((white && TIle_Script.Occupy_White) || (black && TIle_Script.Occupy_Black)))
                    break;
                if (TIle_Script.Vertical_Edge_Down)
                    break;
                x--;
            }
        }
        secondary = false;
        x = 7;
        if (!(Tile_Im_On.Horizontal_Edge_Right))
        {

            while (true)
            {
                GameObject xTile = FindTile(x);
                TIle_Script = xTile.GetComponent<Test_Tile>();
                if (secondary)
                {
                    if (white)
                    {
                        TIle_Script.Attacked_White_Secondary = true;
                        TIle_Script.canbepinned_white = true;
                    }
                    else
                    {
                        TIle_Script.Attacked_Black_Secondary = true;
                        TIle_Script.canbepinned_black = true;
                    }
                    if ((black && TIle_Script.Occupy_White) || (white && TIle_Script.Occupy_Black))
                    {
                        break;
                    }
                }
                else
                {

                    if (white)
                    {
                        TIle_Script.Attacked_White = true;
                        TIle_Script.canbepinned_white = true;
                    }
                    else
                    {
                        TIle_Script.Attacked_Black = true;
                        TIle_Script.canbepinned_black = true;
                    }
                }
                if (((black && TIle_Script.Occupy_White) || (white && TIle_Script.Occupy_Black)))
                {
                    secondary = true;
                }
                if (((white && TIle_Script.Occupy_White) || (black && TIle_Script.Occupy_Black)))
                    break;
                if (TIle_Script.Horizontal_Edge_Right)
                    break;
                x += 8;
            }
        }
        secondary = false;
        x = -9;
        if (!(Tile_Im_On.Horizontal_Edge_Left))
        {

            while (true)
            {
                GameObject xTile = FindTile(x);
                TIle_Script = xTile.GetComponent<Test_Tile>();
                if (secondary)
                {
                    if (white)
                    {
                        TIle_Script.Attacked_White_Secondary = true;
                        TIle_Script.canbepinned_white = true;
                    }
                    else
                    {
                        TIle_Script.Attacked_Black_Secondary = true;
                        TIle_Script.canbepinned_black = true;
                    }
                    if ((black && TIle_Script.Occupy_White) || (white && TIle_Script.Occupy_Black))
                    {
                        break;
                    }
                }
                else
                {

                    if (white)
                    {
                        TIle_Script.Attacked_White = true;
                        TIle_Script.canbepinned_white = true;
                    }
                    else
                    {
                        TIle_Script.Attacked_Black = true;
                        TIle_Script.canbepinned_black = true;
                    }
                }
                if (((black && TIle_Script.Occupy_White) || (white && TIle_Script.Occupy_Black)))
                {
                    secondary = true;
                }
                if (((white && TIle_Script.Occupy_White) || (black && TIle_Script.Occupy_Black)))
                    break;
                if (TIle_Script.Horizontal_Edge_Left)
                    break;
                x -= 8;
            }
        }
    }

    void undostuck()
    {
        int x = 0;
        int y = 1;
        GameObject xTile;
        Test_Tile TIle_Script;
        if ((white && logic_Manager_.check_white || black && logic_Manager_.check_black) && !Taken)
        {
            if (!(Tile_Im_On.Vertical_Edge_Up))
            {
                xTile = FindTile(x);
                TIle_Script = xTile.GetComponent<Test_Tile>();
                if (Pinned)
                {
                    if (TIle_Script.pinnedTile_White && white || black && TIle_Script.pinnedTile_Black)
                        RookCallTiles(y, x);
                }
                else if (Pinned_By_Bishop)
                {
                    if (TIle_Script.pinnedTile_White_Bishop && white || black && TIle_Script.pinnedTile_Black_Bishop)
                        RookCallTiles(y, x);
                }

                else
                    RookCallTiles(y, x);
            }
            x = -2;
            y = -1;
            if (!(Tile_Im_On.Vertical_Edge_Down))
            {
                xTile = FindTile(x);
                TIle_Script = xTile.GetComponent<Test_Tile>();
                if (Pinned)
                {
                    if (TIle_Script.pinnedTile_White && white || black && TIle_Script.pinnedTile_Black)
                        RookCallTiles(y, x);
                }
                else if (Pinned_By_Bishop)
                {
                    if (TIle_Script.pinnedTile_White_Bishop && white || black && TIle_Script.pinnedTile_Black_Bishop)
                        RookCallTiles(y, x);
                }
                else
                    RookCallTiles(y, x);
            }
            x = 7;
            y = 8;
            if (!(Tile_Im_On.Horizontal_Edge_Right))
            {
                xTile = FindTile(x);
                TIle_Script = xTile.GetComponent<Test_Tile>();
                if (Pinned)
                {
                    if (TIle_Script.pinnedTile_White && white || black && TIle_Script.pinnedTile_Black)
                        RookCallTiles(y, x);
                }
                else if (Pinned_By_Bishop)
                {
                    if (TIle_Script.pinnedTile_White_Bishop && white || black && TIle_Script.pinnedTile_Black_Bishop)
                        RookCallTiles(y, x);
                }
                else
                    RookCallTiles(y, x);
            }
            x = -9;
            y = -8;
            if (!(Tile_Im_On.Horizontal_Edge_Left))
            {
                xTile = FindTile(x);
                TIle_Script = xTile.GetComponent<Test_Tile>();
                if (Pinned)
                {
                    if (TIle_Script.pinnedTile_White && white || black && TIle_Script.pinnedTile_Black)
                        RookCallTiles(y, x);
                }
                else if (Pinned_By_Bishop)
                {
                    if (TIle_Script.pinnedTile_White_Bishop && white || black && TIle_Script.pinnedTile_Black_Bishop)
                        RookCallTiles(y, x);
                }
                else
                    RookCallTiles(y, x);
            }
            
        }
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

        if (white)
            Tile_Im_On.canbepinned_white = true;
        else
            Tile_Im_On.canbepinned_black = true;


    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
