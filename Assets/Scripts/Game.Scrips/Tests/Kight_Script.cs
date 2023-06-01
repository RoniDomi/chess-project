using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight_Script : MonoBehaviour
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
    public bool Pinned;


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
        if (((!logic_Manager_.White_Pressed && white) || (!logic_Manager_.Black_Pressed && black)) && !Taken && !Pressed && !Stuck && ((white && logic_Manager_.White_Turn) || (black && logic_Manager_.Black_Turn)))
        {
            int x = 0;
            
            GameObject xTile;
            Test_Tile TIle_Script;
            Pressed = true;
            if (white)
                logic_Manager_.White_Pressed = true;
            else
                logic_Manager_.Black_Pressed = true;
            x = 9;
            if(((int)(position_y + 4.2 + 0.15f + ((position_x + 5.2f) * 8) - 13) + x) >= 0 && ((int)(position_y + 4.2 + 0.15f + ((position_x + 5.2f) * 8) - 13) + x) <= 64 &&  Tile_Im_On.position_.position.y<= 1.5 && Tile_Im_On.position_.position.x <= 2.5 )
            {
                xTile = FindTile(x);
                TIle_Script = xTile.GetComponent<Test_Tile>();
                if (Pinned)
                {
                    if (TIle_Script.pinnedTile_White && white || black && TIle_Script.pinnedTile_Black)
                        KnightCallTiles(x);
                }
                else
                    KnightCallTiles(x);
            }
            x = -7;
            if (((int)(position_y + 4.2 + 0.15f + ((position_x + 5.2f) * 8) - 13) + x) >= 0 && ((int)(position_y + 4.2 + 0.15f + ((position_x + 5.2f) * 8) - 13) + x) <= 64 && Tile_Im_On.position_.position.y <= 1.5 && Tile_Im_On.position_.position.x >= -2.5)
            {
                xTile = FindTile(x);
                TIle_Script = xTile.GetComponent<Test_Tile>();
                if (Pinned)
                {
                    if (TIle_Script.pinnedTile_White && white || black && TIle_Script.pinnedTile_Black)
                        KnightCallTiles(x);
                }
                else
                    KnightCallTiles(x);
            }
            x = 14;
            if (((int)(position_y + 4.2 + 0.15f + ((position_x + 5.2f) * 8) - 13) + x) >= 0 && ((int)(position_y + 4.2 + 0.15f + ((position_x + 5.2f) * 8) - 13) + x) <= 64 && Tile_Im_On.position_.position.y >= -2.5 && Tile_Im_On.position_.position.x <= 1.5)
            {
                xTile = FindTile(x);
                TIle_Script = xTile.GetComponent<Test_Tile>();
                if (Pinned)
                {
                    if (TIle_Script.pinnedTile_White && white || black && TIle_Script.pinnedTile_Black)
                        KnightCallTiles(x);
                }
                else
                    KnightCallTiles(x);
            }
            x = 16;
            if (((int)(position_y + 4.2 + 0.15f + ((position_x + 5.2f) * 8) - 13) + x) >= 0 && ((int)(position_y + 4.2 + 0.15f + ((position_x + 5.2f) * 8) - 13) + x) <= 64 && Tile_Im_On.position_.position.y <= 2.5 && Tile_Im_On.position_.position.x <= 1.5)
            {
                xTile = FindTile(x);
                TIle_Script = xTile.GetComponent<Test_Tile>();
                if (Pinned)
                {
                    if (TIle_Script.pinnedTile_White && white || black && TIle_Script.pinnedTile_Black)
                        KnightCallTiles(x);
                }
                else
                    KnightCallTiles(x);
            }
            x = -16;
            if (((int)(position_y + 4.2 + 0.15f + ((position_x + 5.2f) * 8) - 13) + x) >= 0 && ((int)(position_y + 4.2 + 0.15f + ((position_x + 5.2f) * 8) - 13) + x) <= 64 && Tile_Im_On.position_.position.y <= 2.5 && Tile_Im_On.position_.position.x >= -1.5)
            {
                xTile = FindTile(x);
                TIle_Script = xTile.GetComponent<Test_Tile>();
                if (Pinned)
                {
                    if (TIle_Script.pinnedTile_White && white || black && TIle_Script.pinnedTile_Black)
                        KnightCallTiles(x);
                }
                else
                    KnightCallTiles(x);
            }
            x = -18;
            if (((int)(position_y + 4.2 + 0.15f + ((position_x + 5.2f) * 8) - 13) + x) >= 0 && ((int)(position_y + 4.2 + 0.15f + ((position_x + 5.2f) * 8) - 13) + x) <= 64 && Tile_Im_On.position_.position.y >= -2.5 && Tile_Im_On.position_.position.x >= -1.5)
            {
                xTile = FindTile(x);
                TIle_Script = xTile.GetComponent<Test_Tile>();
                if (Pinned)
                {
                    if (TIle_Script.pinnedTile_White && white || black && TIle_Script.pinnedTile_Black)
                        KnightCallTiles(x);
                }
                else
                    KnightCallTiles(x);
            }
            x = 5;
            if (((int)(position_y + 4.2 + 0.15f + ((position_x + 5.2f) * 8) - 13) + x) >= 0 && ((int)(position_y + 4.2 + 0.15f + ((position_x + 5.2f) * 8) - 13) + x) <= 64 && Tile_Im_On.position_.position.y >= -1.5 && Tile_Im_On.position_.position.x <= 2.5)
            {
                xTile = FindTile(x);
                TIle_Script = xTile.GetComponent<Test_Tile>();
                if (Pinned)
                {
                    if (TIle_Script.pinnedTile_White && white || black && TIle_Script.pinnedTile_Black)
                        KnightCallTiles(x);
                }
                else
                    KnightCallTiles(x);
            }
            x = -11;
            if (((int)(position_y + 4.2 + 0.15f + ((position_x + 5.2f) * 8) - 13) + x) >= 0 && ((int)(position_y + 4.2 + 0.15f + ((position_x + 5.2f) * 8) - 13) + x) <= 64 && Tile_Im_On.position_.position.y >= -1.5 && Tile_Im_On.position_.position.x >= -2.5)
            {
                xTile = FindTile(x);
                TIle_Script = xTile.GetComponent<Test_Tile>();
                if (Pinned)
                {
                    if (TIle_Script.pinnedTile_White && white || black && TIle_Script.pinnedTile_Black)
                        KnightCallTiles(x);
                }
                else
                    KnightCallTiles(x);
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

    public void KnightCallTiles(int x)
    {
        Test_Tile TIle_Script;

        GameObject xTile = FindTile(x);

        TIle_Script = xTile.GetComponent<Test_Tile>();

        if (!((white && TIle_Script.Occupy_White) || (black && TIle_Script.Occupy_Black)))
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
            }
        }



    }
    

    public void CheckIfStuck()
    {
        Test_Tile tile;
        int conditions = 0;

        FindTileImOn();

        if ((white && Tile_Im_On.pinnedTile_White) || (black && Tile_Im_On.pinnedTile_Black))
        {
            Pinned = true;
            Stuck = true;
            return;
        }
        else
        {
            Pinned = false;
        }

        int x = 9;
        if (((int)(position_y + 4.2 + 0.15f + ((position_x + 5.2f) * 8) - 13) + x) < 0 && ((int)(position_y + 4.2 + 0.15f + ((position_x + 5.2f) * 8) - 13) + x) > 64 && Tile_Im_On.position_.position.y <= 1.5 && Tile_Im_On.position_.position.x <= 2.5)
        {
            GameObject xTile = FindTile(x);
            tile = xTile.GetComponent<Test_Tile>();

            if ((white && tile.Occupy_White) || (black && tile.Occupy_Black))
                conditions++;

        }
        else
            conditions++;

        x = -7;
        if (((int)(position_y + 4.2 + 0.15f + ((position_x + 5.2f) * 8) - 13) + x) >= 0 && ((int)(position_y + 4.2 + 0.15f + ((position_x + 5.2f) * 8) - 13) + x) <= 64 && Tile_Im_On.position_.position.y <= 1.5 && Tile_Im_On.position_.position.x >= -2.5)
        {

            GameObject xTile = FindTile(x);
            tile = xTile.GetComponent<Test_Tile>();

            if ((white && tile.Occupy_White) || (black && tile.Occupy_Black))
                    conditions++;

        }
        else
            conditions++;


        x = 14;
        if (((int)(position_y + 4.2 + 0.15f + ((position_x + 5.2f) * 8) - 13) + x) >= 0 && ((int)(position_y + 4.2 + 0.15f + ((position_x + 5.2f) * 8) - 13) + x) <= 64 && Tile_Im_On.position_.position.y >= -2.5 && Tile_Im_On.position_.position.x <= 1.5)
        {

                GameObject xTile = FindTile(x);
                tile = xTile.GetComponent<Test_Tile>();

                if ((white && tile.Occupy_White) || (black && tile.Occupy_Black))
                    conditions++;

            }
            else
                conditions++;


            x = 16;
        if (((int)(position_y + 4.2 + 0.15f + ((position_x + 5.2f) * 8) - 13) + x) >= 0 && ((int)(position_y + 4.2 + 0.15f + ((position_x + 5.2f) * 8) - 13) + x) <= 64 && Tile_Im_On.position_.position.y <= 2.5 && Tile_Im_On.position_.position.x <= 1.5)
        {

                GameObject xTile = FindTile(x);
                tile = xTile.GetComponent<Test_Tile>();

                if ((white && tile.Occupy_White) || (black && tile.Occupy_Black))
                    conditions++;

            }
            else
                conditions++;


            x = -16;
        if (((int)(position_y + 4.2 + 0.15f + ((position_x + 5.2f) * 8) - 13) + x) >= 0 && ((int)(position_y + 4.2 + 0.15f + ((position_x + 5.2f) * 8) - 13) + x) <= 64 && Tile_Im_On.position_.position.y <= 2.5 && Tile_Im_On.position_.position.x >= -1.5)
        {

                GameObject xTile = FindTile(x);
                tile = xTile.GetComponent<Test_Tile>();

                if ((white && tile.Occupy_White) || (black && tile.Occupy_Black))
                    conditions++;

            }
            else
                conditions++;


            x = -18;
        if (((int)(position_y + 4.2 + 0.15f + ((position_x + 5.2f) * 8) - 13) + x) >= 0 && ((int)(position_y + 4.2 + 0.15f + ((position_x + 5.2f) * 8) - 13) + x) <= 64 && Tile_Im_On.position_.position.y >= -2.5 && Tile_Im_On.position_.position.x >= -1.5)
        {


                GameObject xTile = FindTile(x);
                tile = xTile.GetComponent<Test_Tile>();

                if ((white && tile.Occupy_White) || (black && tile.Occupy_Black))
                    conditions++;

            }
            else
                conditions++;

            x = 5;
        if (((int)(position_y + 4.2 + 0.15f + ((position_x + 5.2f) * 8) - 13) + x) >= 0 && ((int)(position_y + 4.2 + 0.15f + ((position_x + 5.2f) * 8) - 13) + x) <= 64 && Tile_Im_On.position_.position.y >= -1.5 && Tile_Im_On.position_.position.x <= 2.5)
        {

                GameObject xTile = FindTile(x);
                tile = xTile.GetComponent<Test_Tile>();

                if ((white && tile.Occupy_White) || (black && tile.Occupy_Black))
                    conditions++;

            }
            else
                conditions++;


            x = -11;
        if (((int)(position_y + 4.2 + 0.15f + ((position_x + 5.2f) * 8) - 13) + x) >= 0 && ((int)(position_y + 4.2 + 0.15f + ((position_x + 5.2f) * 8) - 13) + x) <= 64 && Tile_Im_On.position_.position.y >= -1.5 && Tile_Im_On.position_.position.x >= -2.5)
        {


                GameObject xTile = FindTile(x);
                tile = xTile.GetComponent<Test_Tile>();

                if ((white && tile.Occupy_White) || (black && tile.Occupy_Black))
                    conditions++;

            }
            else
                conditions++;

        if (conditions == 8)
            Stuck = true;
        else
            Stuck = false;


           

        
    }
    public void attack()
    {
        Test_Tile Tile_Script;

        int x = 9;
        if (((int)(position_y + 4.2 + 0.15f + ((position_x + 5.2f) * 8) - 13) + x) >= 0 && ((int)(position_y + 4.2 + 0.15f + ((position_x + 5.2f) * 8) - 13) + x) <= 64 && Tile_Im_On.position_.position.y <= 1.5 && Tile_Im_On.position_.position.x <= 2.5)
        {

            GameObject xTile = FindTile(x);

            Tile_Script = xTile.GetComponent<Test_Tile>();

            if (!((white && Tile_Script.Occupy_White) || (black && Tile_Script.Occupy_Black)))
            {
                if (white)
                    Tile_Script.Attacked_White = true;
                else
                    Tile_Script.Attacked_Black = true;
            }

        }
        x = -7;
        if (((int)(position_y + 4.2 + 0.15f + ((position_x + 5.2f) * 8) - 13) + x) >= 0 && ((int)(position_y + 4.2 + 0.15f + ((position_x + 5.2f) * 8) - 13) + x) <= 64 && Tile_Im_On.position_.position.y <= 1.5 && Tile_Im_On.position_.position.x >= -2.5)
        {

            GameObject xTile = FindTile(x);

            Tile_Script = xTile.GetComponent<Test_Tile>();

          
                if (white)
                    Tile_Script.Attacked_White = true;
                else
                    Tile_Script.Attacked_Black = true;
            

        }
        x = 14;
        if (((int)(position_y + 4.2 + 0.15f + ((position_x + 5.2f) * 8) - 13) + x) >= 0 && ((int)(position_y + 4.2 + 0.15f + ((position_x + 5.2f) * 8) - 13) + x) <= 64 && Tile_Im_On.position_.position.y >= -2.5 && Tile_Im_On.position_.position.x <= 1.5)
        {

            GameObject xTile = FindTile(x);

            Tile_Script = xTile.GetComponent<Test_Tile>();

            if (white)
                Tile_Script.Attacked_White = true;
            else
                Tile_Script.Attacked_Black = true;

        }
        x = 16;
        if (((int)(position_y + 4.2 + 0.15f + ((position_x + 5.2f) * 8) - 13) + x) >= 0 && ((int)(position_y + 4.2 + 0.15f + ((position_x + 5.2f) * 8) - 13) + x) <= 64 && Tile_Im_On.position_.position.y <= 2.5 && Tile_Im_On.position_.position.x <= 1.5)
        {

            GameObject xTile = FindTile(x);

            Tile_Script = xTile.GetComponent<Test_Tile>();

           
                if (white)
                    Tile_Script.Attacked_White = true;
                else
                    Tile_Script.Attacked_Black = true;
           

        }
        x = -16;
        if (((int)(position_y + 4.2 + 0.15f + ((position_x + 5.2f) * 8) - 13) + x) >= 0 && ((int)(position_y + 4.2 + 0.15f + ((position_x + 5.2f) * 8) - 13) + x) <= 64 && Tile_Im_On.position_.position.y <= 2.5 && Tile_Im_On.position_.position.x >= -1.5)
        {

            GameObject xTile = FindTile(x);

            Tile_Script = xTile.GetComponent<Test_Tile>();

            if (white)
                Tile_Script.Attacked_White = true;
            else
                Tile_Script.Attacked_Black = true;
        }
        x = -18;
        if (((int)(position_y + 4.2 + 0.15f + ((position_x + 5.2f) * 8) - 13) + x) >= 0 && ((int)(position_y + 4.2 + 0.15f + ((position_x + 5.2f) * 8) - 13) + x) <= 64 && Tile_Im_On.position_.position.y >= -2.5 && Tile_Im_On.position_.position.x >= -1.5)
        {

            GameObject xTile = FindTile(x);

            Tile_Script = xTile.GetComponent<Test_Tile>();

            if (white)
                Tile_Script.Attacked_White = true;
            else
                Tile_Script.Attacked_Black = true;

        }
        x = 5;
        if (((int)(position_y + 4.2 + 0.15f + ((position_x + 5.2f) * 8) - 13) + x) >= 0 && ((int)(position_y + 4.2 + 0.15f + ((position_x + 5.2f) * 8) - 13) + x) <= 64 && Tile_Im_On.position_.position.y >= -1.5 && Tile_Im_On.position_.position.x <= 2.5)
        {

            GameObject xTile = FindTile(x);

            Tile_Script = xTile.GetComponent<Test_Tile>();

            if (white)
                Tile_Script.Attacked_White = true;
            else
                Tile_Script.Attacked_Black = true;

        }
        x = -11;
        if (((int)(position_y + 4.2 + 0.15f + ((position_x + 5.2f) * 8) - 13) + x) >= 0 && ((int)(position_y + 4.2 + 0.15f + ((position_x + 5.2f) * 8) - 13) + x) <= 64 && Tile_Im_On.position_.position.y >= -1.5 && Tile_Im_On.position_.position.x >= -2.5)
        {

            GameObject xTile = FindTile(x);

            Tile_Script = xTile.GetComponent<Test_Tile>();

            if (white)
                Tile_Script.Attacked_White = true;
            else
                Tile_Script.Attacked_Black = true;
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



    }
}