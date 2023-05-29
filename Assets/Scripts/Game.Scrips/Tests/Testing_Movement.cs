using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Testing_Movement : MonoBehaviour
{
    public bool Pressed = false;

    public bool Stuck = false;

    public int NrOfThisPawn_x;
    public int NrOfThisPawn_y;

    public Testing_Movement Pawnscript;
    public Rook_Script RookScript;

    public GameObject Logic_Manager;

    public Logic_Management_Script logic_Manager_;

    public GameObject[] AllWhitePieces;

    public Transform position_;

    public GameObject Tile_Im_On_x;

    public Test_Tile tile_to_go_to;

    public int NrOfThisPawn;

    public bool FirstMove = true;

    public GameObject Tile_To_Go_To;

    public Test_Tile Tile_Im_On;

    public bool Stuck_For_Real;

    public bool Take_Function_Called_Left = false;
    public bool Take_Function_Called_Right = false;
    public bool Taken = false;

  

    public void Start()
    {
    }

    void Awake()
    {
        logic_Manager_ = Logic_Manager.GetComponent<Logic_Management_Script>();
    }
    public void SetGame()
    {
        Tile_Im_On.NrOfPieceThatsOnMe = NrOfThisPawn;
    }


    public bool CheckIfStuckForReal()
    {
        FindTileImOn();

        int conditions = 0;
        Tile_To_Go_To = FindTile(0);


        tile_to_go_to = Tile_To_Go_To.GetComponent<Test_Tile>();

        if (tile_to_go_to.Occupy_White || tile_to_go_to.Occupy_Black)
        {
            Stuck = true;
            conditions++;
        }
        else
        {
            Stuck = false;
        }
        if (Tile_Im_On.position_.position.x < 3.5)
        {
            Tile_To_Go_To = FindTile(8);
            tile_to_go_to = Tile_To_Go_To.GetComponent<Test_Tile>();

            if (!tile_to_go_to.Occupy_Black)
                conditions++;
        }
        else
            conditions++;
        if (Tile_Im_On.position_.position.x > -3.5)
        {
            Tile_To_Go_To = FindTile(-8);
            tile_to_go_to = Tile_To_Go_To.GetComponent<Test_Tile>();

            if (!tile_to_go_to.Occupy_Black)
                conditions++;
        }
        else
            conditions++;
        if (conditions == 3)
        {
            Stuck_For_Real = true;
            return true;
        }
        else
        {
            Stuck_For_Real = false;
            return false;
        }





    }
    public void OccupyFirstTile()
    {
        FindTileImOn();
        Tile_Im_On.Occupied = true;
    }

    public void FindTileImOn()
    {
        Tile_To_Go_To = FindTile(-1);

        Tile_Im_On = Tile_To_Go_To.GetComponent<Test_Tile>();

    }

    void OnMouseDown()
    {
        FindTileImOn();

        if (Tile_Im_On.Called)
        {
            Tile_Im_On.OnMouseDown();
            Pressed = false;
            Taken = true;
        }

        Tile_To_Go_To = FindTile(0);


        tile_to_go_to = Tile_To_Go_To.GetComponent<Test_Tile>();


        if (tile_to_go_to.Occupy_White || tile_to_go_to.Occupy_Black)
        {
            Stuck = true;
 
        }

        if (!CheckIfStuckForReal() && !Taken && !logic_Manager_.White_Pressed && logic_Manager_.White_Turn)
        {


            CheckIfCanTake();

           Pressed = true;

            logic_Manager_.White_Pressed = true;

            if (!Stuck)
            {

                Tile_To_Go_To = FindTile(0);


                tile_to_go_to = Tile_To_Go_To.GetComponent<Test_Tile>();


           
                    tile_to_go_to.Selected = false;

                tile_to_go_to.NrOfPawnThatCalledThisTile = NrOfThisPawn;

                tile_to_go_to.TestFunction();


                if (FirstMove)
                {
                    Tile_To_Go_To = FindTile(1);
                    tile_to_go_to = Tile_To_Go_To.GetComponent<Test_Tile>();
                    if (!(tile_to_go_to.Occupy_White || tile_to_go_to.Occupy_Black)){
                       
                            tile_to_go_to.Selected = false;
                        
                        tile_to_go_to.NrOfPawnThatCalledThisTile = NrOfThisPawn;

                        tile_to_go_to.TestFunction(); }


                }


            }
        }
        else if(logic_Manager_.White_Pressed && Pressed)
        {


            IfNotOnlyPressed();
        }
        
    }


    public void IfNotOnlyPressed()
    {
        logic_Manager_.White_Pressed = false;
        logic_Manager_.NoHovering = false;

        Tile_Im_On.UncallTiles();
        


        Pressed = false;
        if (Tile_Im_On.position_.position.x < 3.5 && !logic_Manager_.White_Pressed)
        {
            Tile_To_Go_To = FindTile(8);
            tile_to_go_to = Tile_To_Go_To.GetComponent<Test_Tile>();
            tile_to_go_to.Selected = true;
            tile_to_go_to.Called = false;
            if (Take_Function_Called_Left)
            {

                Take_Function_Called_Left = false;
                tile_to_go_to.Occupy_Black = true;

            }

            tile_to_go_to._renderer.color = tile_to_go_to._SavedColor;
        }
        if (Tile_Im_On.position_.position.x > -3.5 && !logic_Manager_.White_Pressed)
        {
            Tile_To_Go_To = FindTile(-8);
            tile_to_go_to = Tile_To_Go_To.GetComponent<Test_Tile>();
            tile_to_go_to.Selected = true;
            tile_to_go_to.Called = false;
            if (Take_Function_Called_Right)
            {
                Take_Function_Called_Right = false;
                tile_to_go_to.Occupy_Black = true;
            }

            tile_to_go_to._renderer.color = tile_to_go_to._SavedColor;
        }
    }

    public void attack()
    {
        Test_Tile TIle_Script;

        FindTileImOn();

        if (((int)(position_.position.y + 4.2 + ((position_.position.x + 5.2f) * 8) - 13) + 8) >= 0 && ((int)(position_.position.y + 4.2 + ((position_.position.x + 5.2f) * 8) - 13) + 8) <= 64)
        {
            GameObject xTile = FindTile(8);
            TIle_Script = xTile.GetComponent<Test_Tile>();
            TIle_Script.Attacked_White = true;
        }
        if (((int)(position_.position.y + 4.2 + ((position_.position.x + 5.2f) * 8) - 13) - 6) >= 0 && ((int)(position_.position.y + 4.2 + ((position_.position.x + 5.2f) * 8) - 13) - 6) <= 64)
        {
            GameObject xTile = FindTile(-8);
            TIle_Script = xTile.GetComponent<Test_Tile>();
            TIle_Script.Attacked_White = true;
        }
    }

    public GameObject FindTile(int x)
    {

        GameObject[] Tiles_To_Be_Selected;
        Tiles_To_Be_Selected = GameObject.FindGameObjectsWithTag("Tag_Tile");
        
        return Tiles_To_Be_Selected[(int)(position_.position.y+4.2 + ((position_.position.x + 5.2f)*8)-13)+x];
    }



    void CheckIfCanTake()
    {
        

        FindTileImOn();

        if (Tile_Im_On.position_.position.x < 3.5) { 

            Tile_To_Go_To = FindTile(8);


        tile_to_go_to = Tile_To_Go_To.GetComponent<Test_Tile>();

        if (tile_to_go_to.Occupy_Black || tile_to_go_to.En_passant_Active_Black)       
            {
                tile_to_go_to.Selected = false;
         
                Take_Function_Called_Left = true;
                tile_to_go_to.Called = true;
                tile_to_go_to.Occupy_Black = false;
                tile_to_go_to.NrOfPawnThatCalledThisTile = NrOfThisPawn;

                if(!tile_to_go_to.Occupy_White)
                tile_to_go_to.TestFunction();
            }
    }

        if (Tile_Im_On.position_.position.x > -3.5) {
            Tile_To_Go_To = FindTile(-8);


            tile_to_go_to = Tile_To_Go_To.GetComponent<Test_Tile>();



            if (tile_to_go_to.Occupy_Black || tile_to_go_to.En_passant_Active_Black)
            {

              
                    tile_to_go_to.Selected = false;
               
                Take_Function_Called_Right = true;
                tile_to_go_to.Called = true;
                tile_to_go_to.Occupy_Black = false;
                tile_to_go_to.NrOfPawnThatCalledThisTile = NrOfThisPawn;

                if (!tile_to_go_to.Occupy_White)
                    tile_to_go_to.TestFunction();

            }

        }
    }
    void Update()
    {
        
    }

}
