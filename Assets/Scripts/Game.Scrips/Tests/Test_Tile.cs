using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Test_Tile : MonoBehaviour
{
    public Color _baseColor, _offsetColor, Hovercolor, _SavedColor;
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

    

    public void Start()
    {
        

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

        GameObject PawnThatsOnMe;

        PawnThatsOnMe = AllPawns[NrOfThisTile_x];

        PawnScript= PawnThatsOnMe.GetComponent<Testing_Movement>();

        PawnScript.OccupyFirstTile();


       
    }



    void OnMouseDown()
    {
        if (Called && !Occupied)
        {
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

            
        }


    }

   
    void WhitePawnMove(GameObject Pawn)
    {

        Pawn.transform.position = new Vector3((float)(position_.position.x), (float)(position_.position.y + 0.4));
        PawnScript.NrOfThisPawn_x = NrOfThisTile_x;
        PawnScript.NrOfThisPawn_y = NrOfThisTile_y;
        PawnScript.Pressed = false;
        PawnScript.FirstMove = false;
        GameObject previousTile = FindTile(1);
        xTile = previousTile.GetComponent<Test_Tile>();

        if (!PawnScript.FirstMove)
        {
            xTile.Called = false;
            xTile.Selected = true;
            xTile._renderer.color = xTile._SavedColor;
            previousTile = FindTile(-1);
            xTile = previousTile.GetComponent<Test_Tile>();
            xTile.Called = false;
            xTile.Selected = true;
            xTile._renderer.color = xTile._SavedColor;
        }

        Called = false;

        Selected = true;

        Occupied = true;

        PawnScript.Tile_Im_On.Occupied = false;


        _renderer.color = _SavedColor;
    }

    void BlackPawnMove(GameObject Pawn)
    {
        Pawn.transform.position = new Vector3((float)(position_.position.x), (float)(position_.position.y + 0.4));
        BlackPawnScript.NrOfThisPawn_x = NrOfThisTile_x;
        BlackPawnScript.NrOfThisPawn_y = NrOfThisTile_y;
        BlackPawnScript.Pressed = false;
        BlackPawnScript.FirstMove = false;
        GameObject previousTile = FindTile(-1);
        xTile = previousTile.GetComponent<Test_Tile>();

        if (!BlackPawnScript.FirstMove)
        {
            xTile.Called = false;
            xTile.Selected = true;
            xTile._renderer.color = xTile._SavedColor;
            previousTile = FindTile(1);
            xTile = previousTile.GetComponent<Test_Tile>();
            xTile.Called = false;
            xTile.Selected = true;
            xTile._renderer.color = xTile._SavedColor;
        }

        Called = false;

        Selected = true;

        BlackPawnScript.Tile_Im_On.Occupied = false;

        Occupied = true;

        

        _renderer.color = _SavedColor;
    }
    
    public GameObject FindTile(int x)
    {
        AllTiles = GameObject.FindGameObjectsWithTag("Tag_Tile");

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
        if (!Occupied)
        {
            Called = true;
            _renderer.color = Hovercolor;
        }
    }

}