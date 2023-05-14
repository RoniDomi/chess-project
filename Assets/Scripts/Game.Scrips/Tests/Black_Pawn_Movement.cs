using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Black_Pawn_Movement : MonoBehaviour
{
    public bool Pressed = false;

    public bool Stuck = false;

    public int NrOfThisPawn_x;
    public int NrOfThisPawn_y;

    public Black_Pawn_Movement Pawnscript;

    public GameObject[] AllPawns;

    public Transform position_;

    public Test_Tile tile_to_go_to;

    public int NrOfThisPawn;

    public bool FirstMove = true;

    public GameObject Tile_To_Go_To;

    public GameObject Tile_Im_On_x;

    public Test_Tile Tile_Im_On;

    public bool Stuck_For_Real;
   
    public bool Take_Function_Called_Left = false;
    public bool Take_Function_Called_Right = false;
    public bool Taken = false;

    public void Start()
    {
        NrOfThisPawn = (int)(position_.position.x + 4.2 +8);

        Tile_To_Go_To = FindTile(-2);


        tile_to_go_to = Tile_To_Go_To.GetComponent<Test_Tile>();

        NrOfThisPawn_x = tile_to_go_to.NrOfThisTile_x;
        NrOfThisPawn_y = tile_to_go_to.NrOfThisTile_y;

 

    }

    public bool CheckIfStuckForReal()
    {
        FindTileImOn();

        int conditions = 0;
        Tile_To_Go_To = FindTile(-2);


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
            Tile_To_Go_To = FindTile(6);
            tile_to_go_to = Tile_To_Go_To.GetComponent<Test_Tile>();

            if (!tile_to_go_to.Occupy_White)
                conditions++;
        }
        else
            conditions++;
        if (Tile_Im_On.position_.position.x > -3.5)
        {
            Tile_To_Go_To = FindTile(-10);
            tile_to_go_to = Tile_To_Go_To.GetComponent<Test_Tile>();

            if (!tile_to_go_to.Occupy_White)
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

        Tile_To_Go_To = FindTile(-2);


        tile_to_go_to = Tile_To_Go_To.GetComponent<Test_Tile>();


        if (tile_to_go_to.Occupy_White || tile_to_go_to.Occupy_Black)
        {
            Stuck = true;

        }

        

        if (OnlyOnePressed() && !CheckIfStuckForReal() && !Taken)
        {
            CheckIfCanTake();
            Pressed = true;

            if (!Stuck)
            {

                Tile_To_Go_To = FindTile(-2);


                tile_to_go_to = Tile_To_Go_To.GetComponent<Test_Tile>();


               
                    tile_to_go_to.Selected = false;
             
                tile_to_go_to.NrOfPawnThatCalledThisTile = NrOfThisPawn;

                tile_to_go_to.TestFunction();


                if (FirstMove)
                {
                    Tile_To_Go_To = FindTile(-3);
                    tile_to_go_to = Tile_To_Go_To.GetComponent<Test_Tile>();
                    if (!(tile_to_go_to.Occupy_White || tile_to_go_to.Occupy_Black)){
                      
                            tile_to_go_to.Selected = false;

                        tile_to_go_to.NrOfPawnThatCalledThisTile = NrOfThisPawn;

                        tile_to_go_to.TestFunction(); }


                }


            }
        }
        else
        {
            IfNotOnlyPressed();
        }
    }

    public void IfNotOnlyPressed()
    {
        if (!Stuck)
        {
            Tile_To_Go_To = FindTile(-2);
            tile_to_go_to = Tile_To_Go_To.GetComponent<Test_Tile>();
            tile_to_go_to.Selected = true;
            tile_to_go_to.Called = false;
            tile_to_go_to._renderer.color = tile_to_go_to._SavedColor;
            Tile_To_Go_To = FindTile(-3);
            tile_to_go_to = Tile_To_Go_To.GetComponent<Test_Tile>();
            tile_to_go_to.Selected = true;
            tile_to_go_to.Called = false;
            tile_to_go_to._renderer.color = tile_to_go_to._SavedColor;
        }

        Pressed = false;
        if (Tile_Im_On.position_.position.x < 3.5 && OnlyOnePressed())
        {
            Tile_To_Go_To = FindTile(6);
            tile_to_go_to = Tile_To_Go_To.GetComponent<Test_Tile>();
            tile_to_go_to.Selected = true;
            tile_to_go_to.Called = false;
            if (Take_Function_Called_Left)
            {

                Take_Function_Called_Left = false;
                tile_to_go_to.Occupy_White = true;
            }
            tile_to_go_to._renderer.color = tile_to_go_to._SavedColor;
        }
        if (Tile_Im_On.position_.position.x > -3.5 && OnlyOnePressed())
        {
            Tile_To_Go_To = FindTile(-10);
            tile_to_go_to = Tile_To_Go_To.GetComponent<Test_Tile>();
            tile_to_go_to.Selected = true;
            tile_to_go_to.Called = false;
            if (Take_Function_Called_Right)
            {
                Take_Function_Called_Right = false;
                tile_to_go_to.Occupy_White = true;
            }

            tile_to_go_to._renderer.color = tile_to_go_to._SavedColor;
        }

    }

    public GameObject FindTile(int x)
    {

        GameObject[] Tiles_To_Be_Selected;
        Tiles_To_Be_Selected = GameObject.FindGameObjectsWithTag("Tag_Tile");

        return Tiles_To_Be_Selected[(int)(position_.position.y + 4.2 + ((position_.position.x + 5.2f) * 8) - 13) + x];
    }

    public bool OnlyOnePressed()
    {

        AllPawns = GameObject.FindGameObjectsWithTag("BlackPawn");

        for (int x = 0; x < 8; x++)
        {
            GameObject xPawn = AllPawns[x];
            Pawnscript = xPawn.GetComponent<Black_Pawn_Movement>();
            if (Pawnscript.Pressed)
            {
                return false;
            }
        }

        return true;
    }

    void CheckIfCanTake()
    {
        
        FindTileImOn();

        if (Tile_Im_On.position_.position.x < 3.5)
        {

            Tile_To_Go_To = FindTile(6);


            tile_to_go_to = Tile_To_Go_To.GetComponent<Test_Tile>();

            if (tile_to_go_to.Occupy_White)
            {

              
                    tile_to_go_to.Selected = false;
              
                Take_Function_Called_Left = true;
                tile_to_go_to.Called = true;
                tile_to_go_to.Occupy_White = false;
                tile_to_go_to.NrOfPawnThatCalledThisTile = NrOfThisPawn;

                if (!tile_to_go_to.Occupy_Black)
                    tile_to_go_to.TestFunction();
            }
        }

        if (Tile_Im_On.position_.position.x > -3.5)
        {
            Tile_To_Go_To = FindTile(-10);


            tile_to_go_to = Tile_To_Go_To.GetComponent<Test_Tile>();



            if (tile_to_go_to.Occupy_White)
            {

              
                    tile_to_go_to.Selected = false;
              
                Take_Function_Called_Right = true;
                tile_to_go_to.Called = true;
                tile_to_go_to.Occupy_White = false;
                tile_to_go_to.NrOfPawnThatCalledThisTile = NrOfThisPawn;

                if (!tile_to_go_to.Occupy_Black)
                    tile_to_go_to.TestFunction();

            }

        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}

