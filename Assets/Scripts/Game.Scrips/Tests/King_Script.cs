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
    public bool Castling;
    public GameObject[] Rooks;
    


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
        Castling = true;
    }

    public void SetGame()
    {
      
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


           
            if(Castling && ((!logic_Manager_.check_white && white) || (!logic_Manager_.check_black && black)))
            {
                Rook_Script rooks;
                Test_Tile tile = FindTile(7).GetComponent<Test_Tile>();
                rooks =Rooks[0].GetComponent<Rook_Script>();
                if (rooks.Castling && ((white && !(tile.Attacked_Black || tile.Occupy_White)) || (black && !(tile.Attacked_White || tile.Occupy_Black))))
                {
                    x = 15;
                    KingCallTiles(x);
                }
                tile = FindTile(-9).GetComponent<Test_Tile>();
                rooks = Rooks[1].GetComponent<Rook_Script>();
                if (rooks.Castling && ((white && !(tile.Attacked_Black || tile.Occupy_White)) || (black && !(tile.Attacked_White || tile.Occupy_Black))))
                {
                    x = -17;
                    KingCallTiles(x);
                }
               
                
            }

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


    void KingCallTiles2(int x)
    {
        Test_Tile TIle_Script;



        GameObject xTile = FindTile(x);
        TIle_Script = xTile.GetComponent<Test_Tile>();
        

            if (white)
                TIle_Script.Attacked_White = true;
            else
                TIle_Script.Attacked_Black = true;

        

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
              if(x == 15)
               {
                
                if((white && !(TIle_Script.Attacked_Black)) || (black && !(TIle_Script.Attacked_White)))
                  TIle_Script.Castle_King_Side = true;
               }
              if (x == -17)
              {
                if ((white && !(TIle_Script.Attacked_Black)) || (black && !(TIle_Script.Attacked_White)))
                TIle_Script.Castle_Queen_Side = true;
              }

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
                if (((tile_to_go_to.Occupy_White || tile_to_go_to.Attacked_Black) && white) || ((tile_to_go_to.Occupy_Black || tile_to_go_to.Attacked_White) && black))
                    conditions++;

                Tile_To_Go_To = FindTile(-2);
                tile_to_go_to = Tile_To_Go_To.GetComponent<Test_Tile>();
                if (((tile_to_go_to.Occupy_White || tile_to_go_to.Attacked_Black) && white) || ((tile_to_go_to.Occupy_Black || tile_to_go_to.Attacked_White) && black))
                    conditions++;

                Tile_To_Go_To = FindTile(6);
                tile_to_go_to = Tile_To_Go_To.GetComponent<Test_Tile>();
                if (((tile_to_go_to.Occupy_White || tile_to_go_to.Attacked_Black) && white) || ((tile_to_go_to.Occupy_Black || tile_to_go_to.Attacked_White) && black))
                    conditions++;
            }
            else if (Tile_Im_On.Horizontal_Edge_Right)
            {
                conditions += 2;

                Tile_To_Go_To = FindTile(-9);
                tile_to_go_to = Tile_To_Go_To.GetComponent<Test_Tile>();
                if (((tile_to_go_to.Occupy_White || tile_to_go_to.Attacked_Black) && white) || ((tile_to_go_to.Occupy_Black || tile_to_go_to.Attacked_White) && black))
                    conditions++;
                Tile_To_Go_To = FindTile(-2);
                tile_to_go_to = Tile_To_Go_To.GetComponent<Test_Tile>();
                if (((tile_to_go_to.Occupy_White || tile_to_go_to.Attacked_Black) && white) || ((tile_to_go_to.Occupy_Black || tile_to_go_to.Attacked_White) && black))
                    conditions++;
                Tile_To_Go_To = FindTile(-10);
                tile_to_go_to = Tile_To_Go_To.GetComponent<Test_Tile>();
                if (((tile_to_go_to.Occupy_White || tile_to_go_to.Attacked_Black) && white) || ((tile_to_go_to.Occupy_Black || tile_to_go_to.Attacked_White) && black))
                    conditions++;
            }
            else
            {

                Tile_To_Go_To = FindTile(-9);
                tile_to_go_to = Tile_To_Go_To.GetComponent<Test_Tile>();
                if (((tile_to_go_to.Occupy_White || tile_to_go_to.Attacked_Black) && white) || ((tile_to_go_to.Occupy_Black || tile_to_go_to.Attacked_White) && black))
                    conditions++;
                Tile_To_Go_To = FindTile(-10);
                tile_to_go_to = Tile_To_Go_To.GetComponent<Test_Tile>();
                if (((tile_to_go_to.Occupy_White || tile_to_go_to.Attacked_Black) && white) || ((tile_to_go_to.Occupy_Black || tile_to_go_to.Attacked_White) && black))
                    conditions++;
                Tile_To_Go_To = FindTile(7);
                tile_to_go_to = Tile_To_Go_To.GetComponent<Test_Tile>();
                if (((tile_to_go_to.Occupy_White || tile_to_go_to.Attacked_Black) && white) || ((tile_to_go_to.Occupy_Black || tile_to_go_to.Attacked_White) && black))
                    conditions++;

                Tile_To_Go_To = FindTile(-2);
                tile_to_go_to = Tile_To_Go_To.GetComponent<Test_Tile>();
                if (((tile_to_go_to.Occupy_White || tile_to_go_to.Attacked_Black) && white) || ((tile_to_go_to.Occupy_Black || tile_to_go_to.Attacked_White) && black))
                    conditions++;

                Tile_To_Go_To = FindTile(6);
                tile_to_go_to = Tile_To_Go_To.GetComponent<Test_Tile>();
                if (((tile_to_go_to.Occupy_White || tile_to_go_to.Attacked_Black) && white) || ((tile_to_go_to.Occupy_Black || tile_to_go_to.Attacked_White) && black))
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
                if (((tile_to_go_to.Occupy_White || tile_to_go_to.Attacked_Black) && white) || ((tile_to_go_to.Occupy_Black || tile_to_go_to.Attacked_White) && black))
                    conditions++;
                Tile_To_Go_To = FindTile(7);
                tile_to_go_to = Tile_To_Go_To.GetComponent<Test_Tile>();
                if (((tile_to_go_to.Occupy_White || tile_to_go_to.Attacked_Black) && white) || ((tile_to_go_to.Occupy_Black || tile_to_go_to.Attacked_White) && black))
                    conditions++;

                Tile_To_Go_To = FindTile(8);
                tile_to_go_to = Tile_To_Go_To.GetComponent<Test_Tile>();
                if (((tile_to_go_to.Occupy_White || tile_to_go_to.Attacked_Black) && white) || ((tile_to_go_to.Occupy_Black || tile_to_go_to.Attacked_White) && black))
                    conditions++;

            }
            else if (Tile_Im_On.Horizontal_Edge_Right)
            {
                conditions += 2;
                Tile_To_Go_To = FindTile(-8);
                tile_to_go_to = Tile_To_Go_To.GetComponent<Test_Tile>();
                if (((tile_to_go_to.Occupy_White || tile_to_go_to.Attacked_Black) && white) || ((tile_to_go_to.Occupy_Black || tile_to_go_to.Attacked_White) && black))
                    conditions++;
                Tile_To_Go_To = FindTile(0);
                tile_to_go_to = Tile_To_Go_To.GetComponent<Test_Tile>();
                if (((tile_to_go_to.Occupy_White || tile_to_go_to.Attacked_Black) && white) || ((tile_to_go_to.Occupy_Black || tile_to_go_to.Attacked_White) && black))
                    conditions++;
                Tile_To_Go_To = FindTile(-9);
                tile_to_go_to = Tile_To_Go_To.GetComponent<Test_Tile>();
                if (((tile_to_go_to.Occupy_White || tile_to_go_to.Attacked_Black) && white) || ((tile_to_go_to.Occupy_Black || tile_to_go_to.Attacked_White) && black))
                    conditions++;
            }
            else
            {

                Tile_To_Go_To = FindTile(8);
                tile_to_go_to = Tile_To_Go_To.GetComponent<Test_Tile>();
                if (((tile_to_go_to.Occupy_White || tile_to_go_to.Attacked_Black) && white) || ((tile_to_go_to.Occupy_Black || tile_to_go_to.Attacked_White) && black))
                    conditions++;
                Tile_To_Go_To = FindTile(-8);
                tile_to_go_to = Tile_To_Go_To.GetComponent<Test_Tile>();
                if (((tile_to_go_to.Occupy_White || tile_to_go_to.Attacked_Black) && white) || ((tile_to_go_to.Occupy_Black || tile_to_go_to.Attacked_White) && black))
                    conditions++;
                Tile_To_Go_To = FindTile(7);
                tile_to_go_to = Tile_To_Go_To.GetComponent<Test_Tile>();
                if (((tile_to_go_to.Occupy_White || tile_to_go_to.Attacked_Black) && white) || ((tile_to_go_to.Occupy_Black || tile_to_go_to.Attacked_White) && black))
                    conditions++;
                Tile_To_Go_To = FindTile(-9);
                tile_to_go_to = Tile_To_Go_To.GetComponent<Test_Tile>();
                if (((tile_to_go_to.Occupy_White || tile_to_go_to.Attacked_Black) && white) || ((tile_to_go_to.Occupy_Black || tile_to_go_to.Attacked_White) && black))
                    conditions++;
                Tile_To_Go_To = FindTile(0);
                tile_to_go_to = Tile_To_Go_To.GetComponent<Test_Tile>();
                if (((tile_to_go_to.Occupy_White || tile_to_go_to.Attacked_Black) && white) || ((tile_to_go_to.Occupy_Black || tile_to_go_to.Attacked_White) && black))
                    conditions++;
            }



        }
        else if (Tile_Im_On.Horizontal_Edge_Right)
        {
            conditions += 3;
            Tile_To_Go_To = FindTile(-8);
            tile_to_go_to = Tile_To_Go_To.GetComponent<Test_Tile>();
            if (((tile_to_go_to.Occupy_White || tile_to_go_to.Attacked_Black) && white) || ((tile_to_go_to.Occupy_Black || tile_to_go_to.Attacked_White) && black))
                conditions++;
            Tile_To_Go_To = FindTile(-10);
            tile_to_go_to = Tile_To_Go_To.GetComponent<Test_Tile>();
            if (((tile_to_go_to.Occupy_White || tile_to_go_to.Attacked_Black) && white) || ((tile_to_go_to.Occupy_Black || tile_to_go_to.Attacked_White) && black))
                conditions++;
            Tile_To_Go_To = FindTile(0);
            tile_to_go_to = Tile_To_Go_To.GetComponent<Test_Tile>();
            if (((tile_to_go_to.Occupy_White || tile_to_go_to.Attacked_Black) && white) || ((tile_to_go_to.Occupy_Black || tile_to_go_to.Attacked_White) && black))
                conditions++;
            Tile_To_Go_To = FindTile(-2);
            tile_to_go_to = Tile_To_Go_To.GetComponent<Test_Tile>();
            if (((tile_to_go_to.Occupy_White || tile_to_go_to.Attacked_Black) && white) || ((tile_to_go_to.Occupy_Black || tile_to_go_to.Attacked_White) && black))
                conditions++;
            Tile_To_Go_To = FindTile(-9);
            tile_to_go_to = Tile_To_Go_To.GetComponent<Test_Tile>();
            if (((tile_to_go_to.Occupy_White || tile_to_go_to.Attacked_Black) && white) || ((tile_to_go_to.Occupy_Black || tile_to_go_to.Attacked_White) && black))
                conditions++;

        }
        else if (Tile_Im_On.Horizontal_Edge_Left)
        {
            conditions += 3;
            Tile_To_Go_To = FindTile(6);
            tile_to_go_to = Tile_To_Go_To.GetComponent<Test_Tile>();
            if (((tile_to_go_to.Occupy_White || tile_to_go_to.Attacked_Black) && white) || ((tile_to_go_to.Occupy_Black || tile_to_go_to.Attacked_White) && black))
                conditions++;

            Tile_To_Go_To = FindTile(8);
            tile_to_go_to = Tile_To_Go_To.GetComponent<Test_Tile>();
            if (((tile_to_go_to.Occupy_White || tile_to_go_to.Attacked_Black) && white) || ((tile_to_go_to.Occupy_Black || tile_to_go_to.Attacked_White) && black))
                conditions++;
            Tile_To_Go_To = FindTile(0);
            tile_to_go_to = Tile_To_Go_To.GetComponent<Test_Tile>();
            if (((tile_to_go_to.Occupy_White || tile_to_go_to.Attacked_Black) && white) || ((tile_to_go_to.Occupy_Black || tile_to_go_to.Attacked_White) && black))
                conditions++;
            Tile_To_Go_To = FindTile(-2);
            tile_to_go_to = Tile_To_Go_To.GetComponent<Test_Tile>();
            if (((tile_to_go_to.Occupy_White || tile_to_go_to.Attacked_Black) && white) || ((tile_to_go_to.Occupy_Black || tile_to_go_to.Attacked_White) && black))
                conditions++;
            Tile_To_Go_To = FindTile(7);
            tile_to_go_to = Tile_To_Go_To.GetComponent<Test_Tile>();
            if (((tile_to_go_to.Occupy_White || tile_to_go_to.Attacked_Black) && white) || ((tile_to_go_to.Occupy_Black || tile_to_go_to.Attacked_White) && black))
                conditions++;
        }
        else
        {
            Tile_To_Go_To = FindTile(-9);
            tile_to_go_to = Tile_To_Go_To.GetComponent<Test_Tile>();
            if (((tile_to_go_to.Occupy_White || tile_to_go_to.Attacked_Black) && white) || ((tile_to_go_to.Occupy_Black || tile_to_go_to.Attacked_White) && black))
                conditions++;
            Tile_To_Go_To = FindTile(6);
            tile_to_go_to = Tile_To_Go_To.GetComponent<Test_Tile>();
            if (((tile_to_go_to.Occupy_White || tile_to_go_to.Attacked_Black) && white) || ((tile_to_go_to.Occupy_Black || tile_to_go_to.Attacked_White) && black))
                conditions++;

            Tile_To_Go_To = FindTile(8);
            tile_to_go_to = Tile_To_Go_To.GetComponent<Test_Tile>();
            if (((tile_to_go_to.Occupy_White || tile_to_go_to.Attacked_Black) && white) || ((tile_to_go_to.Occupy_Black || tile_to_go_to.Attacked_White) && black))
                conditions++;
            Tile_To_Go_To = FindTile(0);
            tile_to_go_to = Tile_To_Go_To.GetComponent<Test_Tile>();
            if (((tile_to_go_to.Occupy_White || tile_to_go_to.Attacked_Black) && white) || ((tile_to_go_to.Occupy_Black || tile_to_go_to.Attacked_White) && black))
                conditions++;
            Tile_To_Go_To = FindTile(-2);
            tile_to_go_to = Tile_To_Go_To.GetComponent<Test_Tile>();
            if (((tile_to_go_to.Occupy_White || tile_to_go_to.Attacked_Black) && white) || ((tile_to_go_to.Occupy_Black || tile_to_go_to.Attacked_White) && black))
                conditions++;
            Tile_To_Go_To = FindTile(7);
            tile_to_go_to = Tile_To_Go_To.GetComponent<Test_Tile>();
            if (((tile_to_go_to.Occupy_White || tile_to_go_to.Attacked_Black) && white) || ((tile_to_go_to.Occupy_Black || tile_to_go_to.Attacked_White) && black))
                conditions++;
            Tile_To_Go_To = FindTile(-10);
            tile_to_go_to = Tile_To_Go_To.GetComponent<Test_Tile>();
            if (((tile_to_go_to.Occupy_White || tile_to_go_to.Attacked_Black) && white) || ((tile_to_go_to.Occupy_Black || tile_to_go_to.Attacked_White) && black))
                conditions++;
            Tile_To_Go_To = FindTile(-8);
            tile_to_go_to = Tile_To_Go_To.GetComponent<Test_Tile>();
            if (((tile_to_go_to.Occupy_White || tile_to_go_to.Attacked_Black) && white) || ((tile_to_go_to.Occupy_Black || tile_to_go_to.Attacked_White) && black))
                conditions++;
        }


        if (conditions == 8)
        {
            Stuck = true;
        }
        else
            Stuck = false;
    
    }

    public void PinPiece()
    {
        int x = 0;
        if (!(Tile_Im_On.Vertical_Edge_Up))
            RookCallTiles(1, x);
        x = -2;
        if (!(Tile_Im_On.Vertical_Edge_Down))
            RookCallTiles(-1, x);
        x = 7;
        if (!(Tile_Im_On.Horizontal_Edge_Right))
            RookCallTiles(8, x);
        x = -9;
        if (!(Tile_Im_On.Horizontal_Edge_Left))
            RookCallTiles(-8, x);
        x = 8;
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

    public void BishopCallTiles(int i, int j)
    {
        bool found=false;
        Test_Tile TIle_Script;
        GameObject xTile = FindTile(j);
        TIle_Script = xTile.GetComponent<Test_Tile>();
        if ((white && (!TIle_Script.Attacked_Black || !TIle_Script.Attacked_Black_Secondary) && (!TIle_Script.canbepinned_black && !TIle_Script.canbepinned_black_Bishop)) || (black && (!TIle_Script.Attacked_White || !TIle_Script.Attacked_White_Secondary) && (!TIle_Script.canbepinned_white && !TIle_Script.canbepinned_white_Bishop)))
            return;
        int x = j;
        int cnt = 0;
        while (true)
        {
            xTile = FindTile(j);
            TIle_Script = xTile.GetComponent<Test_Tile>();
            if ((black && TIle_Script.Occupy_Black) || (white && TIle_Script.Occupy_White))
                cnt++;

            if ((white && TIle_Script.Occupy_Black) || (black && TIle_Script.Occupy_White))
            {
                if ((white && TIle_Script.Occupy_Black && (TIle_Script.canbepinned_black || TIle_Script.canbepinned_black_Bishop)) || (black && TIle_Script.Occupy_White && (TIle_Script.canbepinned_white || TIle_Script.canbepinned_white_Bishop)))
                    found = true;

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

        if (cnt > 1 || cnt == 0)
        {
            return;
        }
       
        if (found)
        {
            while (true)
            {
                xTile = FindTile(x);
                TIle_Script = xTile.GetComponent<Test_Tile>();
                if (white)
                {
                    if (TIle_Script.canbepinned_black_Bishop)
                        TIle_Script.pinnedTile_White_Bishop = true;
                    else if (TIle_Script.canbepinned_black)
                        TIle_Script.pinnedTile_White = true;
                }
                else
                {
                    if (TIle_Script.canbepinned_white_Bishop)
                        TIle_Script.pinnedTile_Black_Bishop = true;
                    else if (TIle_Script.canbepinned_white)
                        TIle_Script.pinnedTile_Black = true;
                }
                if ((white && TIle_Script.Occupy_Black) || (black && TIle_Script.Occupy_White))
                {
                    break;

                }
                x += i;
            }
        }

    }

    public void RookCallTiles(int i, int j)
    {
        bool found = false;
        Test_Tile TIle_Script;
        GameObject xTile = FindTile(j);
        TIle_Script = xTile.GetComponent<Test_Tile>();
        if ((white && (!TIle_Script.Attacked_Black || !TIle_Script.Attacked_Black_Secondary) && (!TIle_Script.canbepinned_black && !TIle_Script.canbepinned_black_Bishop)) || (black && (!TIle_Script.Attacked_White || !TIle_Script.Attacked_White_Secondary) && (!TIle_Script.canbepinned_white && !TIle_Script.canbepinned_white_Bishop)))
            return;
        int x = j;
        int cnt = 0;
        while (true)
        {
            xTile = FindTile(j);
            TIle_Script = xTile.GetComponent<Test_Tile>();
            if ((black && TIle_Script.Occupy_Black) || (white && TIle_Script.Occupy_White))
                cnt++;

            if ((white && TIle_Script.Occupy_Black) || (black && TIle_Script.Occupy_White)) 
            { 
                if ((white && TIle_Script.Occupy_Black && (TIle_Script.canbepinned_black || TIle_Script.canbepinned_black_Bishop)) || (black && TIle_Script.Occupy_White && (TIle_Script.canbepinned_white || TIle_Script.canbepinned_white_Bishop)))
                 found = true;
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

    

        if (cnt > 1 || cnt == 0)
        {
            return;
        }
        if (found) 
        { 
            while (true)
            {
                 xTile = FindTile(x);
                TIle_Script = xTile.GetComponent<Test_Tile>();
                if (white)
                {
                    if (TIle_Script.canbepinned_black_Bishop)
                        TIle_Script.pinnedTile_White_Bishop = true;
                    else if (TIle_Script.canbepinned_black)
                        TIle_Script.pinnedTile_White = true;
                }
                else
                {
                    if (TIle_Script.canbepinned_white_Bishop)
                        TIle_Script.pinnedTile_Black_Bishop = true;
                    else if (TIle_Script.canbepinned_white)
                        TIle_Script.pinnedTile_Black = true;
                }
                if ((white && TIle_Script.Occupy_Black) || (black && TIle_Script.Occupy_White))
                {
                    break;

                }
                x += i;
            }
        }

    }

    public void BishopCheckTIles(int i, int j)
    {
        Test_Tile TIle_Script;
        GameObject xTile;
        bool found_piece = false;
        bool found_smth = false;
        int x = j;
        while (true)
        {
            xTile = FindTile(j);
            TIle_Script = xTile.GetComponent<Test_Tile>();
            if ((black && TIle_Script.Occupy_Black) || (white && TIle_Script.Occupy_White))
                return;

            if ((white && TIle_Script.Occupy_Black) || (black && TIle_Script.Occupy_White))
            {
                if (TIle_Script.NrOfPieceThatsOnMe >= 20 && TIle_Script.NrOfPieceThatsOnMe < 26)
                    found_piece = true;
                else
                    found_smth = true;
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

        if (found_piece)
        {
            while (true)
            {

                xTile = FindTile(x);
                TIle_Script = xTile.GetComponent<Test_Tile>();
                if (black && TIle_Script.Occupy_Black || white && TIle_Script.Occupy_White)
                    break;

                if (white)
                    TIle_Script.Checked_White = true;
                else
                    TIle_Script.Checked_Black = true; ;


                if ((white && TIle_Script.Occupy_Black) || (black && TIle_Script.Occupy_White))
                {
                    if (white)
                        TIle_Script.Checked_White = true;
                    else
                        TIle_Script.Checked_Black = true;
                    break;

                }
              x += i;
            }
            return;
        }
        if(found_smth)
        {
            xTile = FindTile(x);
            TIle_Script = xTile.GetComponent<Test_Tile>();
            if (black && TIle_Script.Occupy_Black || white && TIle_Script.Occupy_White)
                return;

            if ((white && TIle_Script.Occupy_Black) || (black && TIle_Script.Occupy_White))
            {
                if (white)
                    TIle_Script.Checked_White = true;
                else
                    TIle_Script.Checked_Black = true;
                return;

            }
        }
    }

    public void RookCheckTiles(int i, int j)
    {
        Test_Tile TIle_Script;
        GameObject xTile;
        bool found_piece = false;
        bool found_smth = false;
        int x = j;
        while (true)
        {
            xTile = FindTile(j);
            TIle_Script = xTile.GetComponent<Test_Tile>();
            if ((black && TIle_Script.Occupy_Black) || (white && TIle_Script.Occupy_White))
                return;

            if ((white && TIle_Script.Occupy_Black) || (black && TIle_Script.Occupy_White))
            {
                if ((TIle_Script.NrOfPieceThatsOnMe >= 16 && TIle_Script.NrOfPieceThatsOnMe < 20 )|| TIle_Script.NrOfPieceThatsOnMe == 24 || TIle_Script.NrOfPieceThatsOnMe == 25)
                    found_piece = true;
                else
                    found_smth = true;
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

        if (found_piece) {
            while (true)
            {

                xTile = FindTile(x);
                TIle_Script = xTile.GetComponent<Test_Tile>();
                if (black && TIle_Script.Occupy_Black || white && TIle_Script.Occupy_White)
                    break;

                if (white)
                    TIle_Script.Checked_White = true;
                else
                    TIle_Script.Checked_Black = true; ;


                if ((white && TIle_Script.Occupy_Black) || (black && TIle_Script.Occupy_White))
                {
                    if (white)
                        TIle_Script.Checked_White = true;
                    else
                        TIle_Script.Checked_Black = true;
                    break;

                }
               
                x += i;

            }
            return;
        }
        if (found_smth)
        {
            xTile = FindTile(x);
            TIle_Script = xTile.GetComponent<Test_Tile>();
            if (black && TIle_Script.Occupy_Black || white && TIle_Script.Occupy_White)
                return;

            if ((white && TIle_Script.Occupy_Black) || (black && TIle_Script.Occupy_White))
            {
                if (white)
                    TIle_Script.Checked_White = true;
                else
                    TIle_Script.Checked_Black = true;
                return;

            }
        }

    }

    public void attack()
    {
        FindTileImOn();

        if (white)
        {
            Tile_Im_On.king_white = true;
        }
        else
            Tile_Im_On.king_black = true;

        int x = 0;
        if (!(Tile_Im_On.Vertical_Edge_Up))
            KingCallTiles2(x);
        x = -2;
        if (!(Tile_Im_On.Vertical_Edge_Down))
            KingCallTiles2(x);
        x = 7;
        if (!(Tile_Im_On.Horizontal_Edge_Right))
            KingCallTiles2(x);
        x = -9;
        if (!(Tile_Im_On.Horizontal_Edge_Left))
            KingCallTiles2(x);
        x = 8;
        if (!(Tile_Im_On.Vertical_Edge_Up || Tile_Im_On.Horizontal_Edge_Right))
            KingCallTiles2(x);
        x = -8;
        if (!(Tile_Im_On.Vertical_Edge_Up || Tile_Im_On.Horizontal_Edge_Left))
            KingCallTiles2(x);
        x = 6;
        if (!(Tile_Im_On.Horizontal_Edge_Right || Tile_Im_On.Vertical_Edge_Down))
            KingCallTiles2(x);
        x = -10;
        if (!(Tile_Im_On.Horizontal_Edge_Left || Tile_Im_On.Vertical_Edge_Down))
            KingCallTiles2(x);
    }

    public void FindCheckedTiles()
    {
        int x = 0;
        if (!(Tile_Im_On.Vertical_Edge_Up))
            RookCheckTiles(1, x);
        x = -2;
        if (!(Tile_Im_On.Vertical_Edge_Down))
            RookCheckTiles(-1, x);
        x = 7;
        if (!(Tile_Im_On.Horizontal_Edge_Right))
            RookCheckTiles(8, x);
        x = -9;
        if (!(Tile_Im_On.Horizontal_Edge_Left))
            RookCheckTiles(-8, x);
        x = 8;
        if (!(Tile_Im_On.Vertical_Edge_Up || Tile_Im_On.Horizontal_Edge_Right))
            BishopCheckTIles(9, x);
        x = -8;
        if (!(Tile_Im_On.Vertical_Edge_Up || Tile_Im_On.Horizontal_Edge_Left))
            BishopCheckTIles(-7, x);
        x = 6;
        if (!(Tile_Im_On.Horizontal_Edge_Right || Tile_Im_On.Vertical_Edge_Down))
            BishopCheckTIles(7, x);
        x = -10;
        if (!(Tile_Im_On.Horizontal_Edge_Left || Tile_Im_On.Vertical_Edge_Down))
            BishopCheckTIles(-9, x);
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

        Tile_Im_On.NrOfPieceThatsOnMe = NrOfThisPiece;



    }
}
