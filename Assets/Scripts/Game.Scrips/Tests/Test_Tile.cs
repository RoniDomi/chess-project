using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Test_Tile : MonoBehaviour
{
    public Color _baseColor, _offsetColor, Hovercolor, _SavedColor,_TakeColor;
    public SpriteRenderer _renderer;
    public int NrOfThisTile_x;
    public int NrOfThisTile_y;
    public bool Selected = true;
    public int NrOfPawnThatCalledThisTile;
    public bool Called = false;
    public GameObject[] AllPawns;
    public Transform position_;
    public GameObject[] AllTiles;
    public Test_Tile xTile;
    public bool Occupied = false;
    public Testing_Movement PawnScript;
    public Black_Pawn_Movement BlackPawnScript;
    public bool Occupy_White=false;
    public bool Occupy_Black=false;
    public int NrOfPieceThatsOnMe;


    public void Start()
    {
        NrOfPieceThatsOnMe = 100;

        Debug.Log("Color Changed");

        if (((transform.position.x + 3.5f) % 2 == 0 && (transform.position.y + 3.5f) % 2 != 0) || ((transform.position.x + 3.5f) % 2 != 0 && (transform.position.y + 3.5f) % 2 == 0))
        {
            _renderer.color = _offsetColor;
        }
        else
        {
            _renderer.color = _baseColor;
        }
        _SavedColor = _renderer.color;

        GameObject[] BlackPawns= GameObject.FindGameObjectsWithTag("BlackPawn");

        AllPawns = GameObject.FindGameObjectsWithTag("Pawns");

        AllPawns = AllPawns.Concat(BlackPawns).ToArray();

        NrOfThisTile_x = (int)(transform.position.x + 3.5f);

        NrOfThisTile_y = (int)(transform.position.y + 3.5f);

        if ((transform.position.y >= -3.5 && transform.position.y <= -2.5))
        {
            Occupy_White = true;
            Occupied = true;
        }
        else if ((transform.position.y <= 3.5 && transform.position.y >= 2.5))
        {
            Occupied = true;
            Occupy_Black = true;
        }
             
        AllTiles= GameObject.FindGameObjectsWithTag("Tag_Tile");

        if(position_.position.y == -2.5)
        {
            NrOfPieceThatsOnMe = NrOfThisTile_x;
        }
        else if (position_.position.y == 2.5)
        {
            NrOfPieceThatsOnMe = (int)(NrOfThisTile_x + 8);
        }
    }
    public void UncallTiles()
    {
        for (int x = 0; x < 64; x++)
        {
            Test_Tile Tiles_In_This_loop;
            Tiles_In_This_loop = AllTiles[x].GetComponent<Test_Tile>();
            if (Tiles_In_This_loop.Called)
            {
                Tiles_In_This_loop.Called = false;
                Tiles_In_This_loop.Selected = true;
                Tiles_In_This_loop._renderer.color = Tiles_In_This_loop._SavedColor;
            }
        }
    }

    void Check_Which_Pieces_Are_Stuck()
    {
        int x = 0;

        for (; x < 8; x++)
        {
            Testing_Movement whitepawns;
            whitepawns= AllPawns[x].GetComponent<Testing_Movement>();

            whitepawns.CheckIfStuckForReal();
            
        }

        for(; x < 16; x++)
        {
            Black_Pawn_Movement blackpawns;

            blackpawns= AllPawns[x].GetComponent<Black_Pawn_Movement>();

            blackpawns.CheckIfStuckForReal();
        }


        
    }

    public void DeletePiece()
    {

        AllPawns[NrOfPieceThatsOnMe].transform.position += new Vector3(0, 0, 5);
        if(NrOfPieceThatsOnMe>7)
        {

            BlackPawnScript = AllPawns[NrOfPieceThatsOnMe].GetComponent<Black_Pawn_Movement>();
            BlackPawnScript.Tile_Im_On.Occupy_Black = false;
        }else if (NrOfPieceThatsOnMe < 7)
        {

            PawnScript = AllPawns[NrOfPieceThatsOnMe].GetComponent<Testing_Movement>();
            PawnScript.Tile_Im_On.Occupy_White = false;
        }

    }
   public  void OnMouseDown()
    {
        AllTiles = GameObject.FindGameObjectsWithTag("Tag_Tile");
        if (Called && !(Occupy_Black || Occupy_White))
        {
            AllTiles = GameObject.FindGameObjectsWithTag("Tag_Tile");
            Debug.Log("Tile onmousedown called");
            GameObject Pawn = AllPawns[(int)(NrOfPawnThatCalledThisTile)];



            if(NrOfPawnThatCalledThisTile>7)
            {
                
                BlackPawnScript = Pawn.GetComponent<Black_Pawn_Movement>();
                BlackPawnMove(Pawn);
            }
            else
            {
                PawnScript= Pawn.GetComponent<Testing_Movement>();
                WhitePawnMove(Pawn);
            }
            UncallTiles();
            if(NrOfPieceThatsOnMe!=100)
           DeletePiece();
            
            
            NrOfPieceThatsOnMe = NrOfPawnThatCalledThisTile;
            
            
            
            Check_Which_Pieces_Are_Stuck();

        }


    }


    void WhitePawnMove(GameObject Pawn)
    {
        PawnScript.IfNotOnlyPressed();
        Pawn.transform.position = new Vector3((float)(position_.position.x), (float)(position_.position.y + 0.4));
        PawnScript.NrOfThisPawn_x = NrOfThisTile_x;
        PawnScript.NrOfThisPawn_y = NrOfThisTile_y;
        PawnScript.Pressed = false;
        PawnScript.FirstMove = false;
       

        Occupied = true;
        Occupy_White = true;
        PawnScript.Tile_Im_On.Occupied = false;
        PawnScript.Tile_Im_On.Occupy_White = false;
        PawnScript.Tile_Im_On.NrOfPieceThatsOnMe = 100;
        PawnScript.Take_Function_Called_Left = false;
        PawnScript.Take_Function_Called_Right = false;
        

    }

    void BlackPawnMove(GameObject Pawn)
    {
       
        BlackPawnScript.IfNotOnlyPressed();
        Pawn.transform.position = new Vector3((float)(position_.position.x), (float)(position_.position.y + 0.4));

        
        BlackPawnScript.NrOfThisPawn_x = NrOfThisTile_x;
        BlackPawnScript.NrOfThisPawn_y = NrOfThisTile_y;
        BlackPawnScript.Pressed = false;
        BlackPawnScript.FirstMove = false;
      

       BlackPawnScript.Tile_Im_On.Occupied = false;
        BlackPawnScript.Tile_Im_On.Occupy_Black = false;
        BlackPawnScript.Tile_Im_On.NrOfPieceThatsOnMe = 100;
        Occupied = true;
        Occupy_Black = true;
        BlackPawnScript.Take_Function_Called_Left = false;
        BlackPawnScript.Take_Function_Called_Right = false;
        


    }
    
    public GameObject FindTile(int x)
    {
        return AllTiles[(int)((position_.position.x + 3.5)*8+position_.position.y+3.5 - x)];
    }
    void OnMouseEnter()
    {
            _renderer.color = Hovercolor;
            Debug.Log("Enter " + gameObject.name);
    }
    void OnMouseOver()
    {

            _renderer.color = Hovercolor;
    }
    void OnMouseExit()
    {
        if (Selected)
        {
            _renderer.color = _SavedColor;
        }
    }

    public void TestFunction()
    {
        if (!Occupy_Black || !Occupy_White)
        {
            Called = true;
            _renderer.color = Hovercolor;
        }
    }

    public void RedTakeColor()
    {
        
            Called = true;
            _renderer.color = _TakeColor;
        
    }
}