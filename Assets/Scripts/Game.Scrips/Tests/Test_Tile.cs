using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Test_Tile : MonoBehaviour
{
    public Color _baseColor, _offsetColor, Hovercolor, _SavedColor,_TakeColor,_DarkCalledColor,_LightCalledColor;
    public bool NoHovering = false;
    public SpriteRenderer _renderer;
    public int NrOfThisTile_x;
    public int NrOfThisTile_y;
    public bool Selected = true;
    public int NrOfPawnThatCalledThisTile;
    public bool Called = false;
    public GameObject[] AllPieces;
    public Transform position_;
    public GameObject[] AllTiles;
    public Test_Tile xTile;
    public bool Occupied = false;
    public Testing_Movement PawnScript;
    public Black_Pawn_Movement BlackPawnScript;
    public Bishop_Script BishopScript;
    public Queen_Script QueenScript;
    public bool Occupy_White=false;
    public bool Occupy_Black=false;
    public int NrOfPieceThatsOnMe;
    public bool En_passant_Active_White = false;
    public bool En_passant_Active_Black = false;
    public bool Vertical_Edge_Up = false;
    public bool Vertical_Edge_Down = false;
    public bool Horizontal_Edge_Left = false;
    public bool Horizontal_Edge_Right = false;
    public Rook_Script RookScript;
    public GameObject Logic;
    public Logic_Management_Script logic_Manager_;

    bool WhitePiece(Test_Tile tile)
    {
        if (tile.NrOfPieceThatsOnMe < 8 || tile.NrOfPieceThatsOnMe == 16 || tile.NrOfPieceThatsOnMe == 17 || tile.NrOfPieceThatsOnMe == 20 || tile.NrOfPieceThatsOnMe == 21 || tile.NrOfPieceThatsOnMe == 24)
            return true;
        return false;
    }
    bool BlackPiece(Test_Tile tile)
    {
        {
            if ((tile.NrOfPieceThatsOnMe > 8 && tile.NrOfPieceThatsOnMe < 16) || tile.NrOfPieceThatsOnMe == 18 || tile.NrOfPieceThatsOnMe == 19 || tile.NrOfPieceThatsOnMe == 22 || tile.NrOfPieceThatsOnMe == 23 || tile.NrOfPieceThatsOnMe == 25)
                return true;
            return false;
        }
    }

    public void Start()
    {
        Logic= GameObject.FindGameObjectWithTag("Logic_Manager");

        logic_Manager_ = Logic.GetComponent<Logic_Management_Script>();

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
        GameObject[] WhiteRooks= GameObject.FindGameObjectsWithTag("White_Rook");
        GameObject[] BlackRooks = GameObject.FindGameObjectsWithTag("Black_Rook");
        GameObject[] WhiteBishops = GameObject.FindGameObjectsWithTag("White_Bishop");
        GameObject[] BlackBishops = GameObject.FindGameObjectsWithTag("Black_Bishop");
        GameObject[] WhiteQueen = GameObject.FindGameObjectsWithTag("White_Queen");
        GameObject[] BlackQueen = GameObject.FindGameObjectsWithTag("Black_Queen");

        AllPieces = GameObject.FindGameObjectsWithTag("Pawns");

        AllPieces = AllPieces.Concat(BlackPawns).ToArray();
        AllPieces = AllPieces.Concat(WhiteRooks).ToArray();
        AllPieces = AllPieces.Concat(BlackRooks).ToArray();
        AllPieces = AllPieces.Concat(WhiteBishops).ToArray();
        AllPieces = AllPieces.Concat(BlackBishops).ToArray();
        AllPieces = AllPieces.Concat(WhiteQueen).ToArray();
        AllPieces = AllPieces.Concat(BlackQueen).ToArray();

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

        if (position_.position.x == -3.5)
            Horizontal_Edge_Left = true;
        if (position_.position.x == 3.5)
            Horizontal_Edge_Right = true;

        if (position_.position.y == 3.5)
            Vertical_Edge_Up = true;
        if (position_.position.y == -3.5)
            Vertical_Edge_Down = true;

        if (Vertical_Edge_Up )
        {
            if (Horizontal_Edge_Right)
            {
                NrOfPieceThatsOnMe = 19;
            }
            else if (Horizontal_Edge_Left)
                NrOfPieceThatsOnMe = 18;
        }else if (Vertical_Edge_Down)
        {
            if (Horizontal_Edge_Right)
            {
                NrOfPieceThatsOnMe = 17;
            }
            else if (Horizontal_Edge_Left)
                NrOfPieceThatsOnMe = 16;
        }
        

    }
    public void UncallTiles()
    {
        logic_Manager_.NoHovering = false;
        Test_Tile Tiles_In_This_loop;
        for (int x = 0; x < 64; x++)
        {
            
            Tiles_In_This_loop = AllTiles[x].GetComponent<Test_Tile>();
            if (Tiles_In_This_loop.Called)
            {
                Tiles_In_This_loop.Called = false;
                Tiles_In_This_loop.Selected = true;
                Tiles_In_This_loop._renderer.color = Tiles_In_This_loop._SavedColor;

               if (WhitePiece(Tiles_In_This_loop))
                    {
                     if(!(WhitePiece(Tiles_In_This_loop)))
                       {
                       
                           Tiles_In_This_loop.Occupy_White = true; 

                        }
                        
                    }
                else if (BlackPiece(Tiles_In_This_loop) && !En_passant_Active_Black)
                    {
                       
                            Tiles_In_This_loop.Occupy_Black = true;
                        
                    }
                

                Tiles_In_This_loop.NrOfPawnThatCalledThisTile = 100;


               
            }
        }
    }

    public void Undo_En_Passant()
    {
        Test_Tile Tiles_In_This_loop;
        for (int x = 0; x < 64; x++)
        {
            Tiles_In_This_loop = AllTiles[x].GetComponent<Test_Tile>();
            if (Tiles_In_This_loop.En_passant_Active_White || Tiles_In_This_loop.En_passant_Active_Black)
            {
                Tiles_In_This_loop.En_passant_Active_White = false;
                Tiles_In_This_loop.En_passant_Active_Black = false;
                Tiles_In_This_loop.Occupied = false;
                Tiles_In_This_loop.Occupy_Black = false;
                Tiles_In_This_loop.Occupy_White = false;
                Tiles_In_This_loop.NrOfPieceThatsOnMe = 100;
            }

        }
    }

    void Check_Which_Pieces_Are_Stuck()
    {
        int x = 0;

        for (; x < 8; x++)
        {
            Testing_Movement whitepawns;
            whitepawns= AllPieces[x].GetComponent<Testing_Movement>();

            whitepawns.CheckIfStuckForReal();
            
        }

        for(; x < 16; x++)
        {
            Black_Pawn_Movement blackpawns;

            blackpawns= AllPieces[x].GetComponent<Black_Pawn_Movement>();

            blackpawns.CheckIfStuckForReal();
        }

        for(; x<20; x++)
        {
            Rook_Script rooks;
            rooks = AllPieces[x].GetComponent<Rook_Script>();
            rooks.CheckIfStuck();
        }
        for(;x<24; x++)
        {
            Bishop_Script bishops;
            bishops = AllPieces[x].GetComponent<Bishop_Script>();
            bishops.CheckIfStuck();
        }
        for(;x<26;x++)
        {
            Queen_Script queens;
            queens = AllPieces[x].GetComponent<Queen_Script>();
            queens.CheckIfStuck();
        }


        
    }

    public void DeletePiece()
    {
        FindObjectOfType<AudioManager>().Play("PieceTake");
        AllPieces[NrOfPieceThatsOnMe].transform.position += new Vector3(0, 0, 5);
        if(NrOfPieceThatsOnMe>=8 && NrOfPieceThatsOnMe<16)
        {

            BlackPawnScript = AllPieces[NrOfPieceThatsOnMe].GetComponent<Black_Pawn_Movement>();
            BlackPawnScript.Tile_Im_On.Occupy_Black = false;
            BlackPawnScript.Tile_Im_On.En_passant_Active_Black = false;
            BlackPawnScript.Tile_Im_On.NrOfPieceThatsOnMe = 100;
        }
        else if (NrOfPieceThatsOnMe < 8 )
        {

            PawnScript = AllPieces[NrOfPieceThatsOnMe].GetComponent<Testing_Movement>();
            PawnScript.Tile_Im_On.Occupy_White = false;
            PawnScript.Tile_Im_On.En_passant_Active_White = false;
            PawnScript.Tile_Im_On.NrOfPieceThatsOnMe = 100;
        }
        else if(NrOfPieceThatsOnMe >=16 && NrOfPieceThatsOnMe <20 )
        {
            RookScript=AllPieces[NrOfPieceThatsOnMe].GetComponent<Rook_Script>();
            if(RookScript.white)
                RookScript.Tile_Im_On.Occupy_White = false;
            else
                RookScript.Tile_Im_On.Occupy_Black = false;
            RookScript.Tile_Im_On.NrOfPieceThatsOnMe = 100;
        }
        else if(NrOfPieceThatsOnMe >= 20 && NrOfPieceThatsOnMe < 24)
        {
            BishopScript = AllPieces[NrOfPieceThatsOnMe].GetComponent<Bishop_Script>();
            if (BishopScript.white)
                BishopScript.Tile_Im_On.Occupy_White = false;
            else
                BishopScript.Tile_Im_On.Occupy_Black = false;
            BishopScript.Tile_Im_On.NrOfPieceThatsOnMe = 100;
        }
        else if(NrOfPieceThatsOnMe >= 24 && NrOfPieceThatsOnMe < 26)
            {
            QueenScript = AllPieces[NrOfPieceThatsOnMe].GetComponent<Queen_Script>();
            if (QueenScript.white)
                QueenScript.Tile_Im_On.Occupy_White = false;
            else
                QueenScript.Tile_Im_On.Occupy_Black = false;
            QueenScript.Tile_Im_On.NrOfPieceThatsOnMe = 100;
        }

    }
   public  void OnMouseDown()
    {
     
        AllTiles = GameObject.FindGameObjectsWithTag("Tag_Tile");


        if (Called && !(Occupy_Black || Occupy_White))
        {
            bool v=false;
            bool m = false;

            if(En_passant_Active_Black || En_passant_Active_White)
            {
                v = true;
            }
        int saved_number = NrOfPieceThatsOnMe;
            Undo_En_Passant();

        NrOfPieceThatsOnMe = saved_number;

            AllTiles = GameObject.FindGameObjectsWithTag("Tag_Tile");
            Debug.Log("Tile onmousedown called");
            GameObject Piece = AllPieces[(int)(NrOfPawnThatCalledThisTile)];



            if(NrOfPawnThatCalledThisTile>=8 && NrOfPawnThatCalledThisTile < 16)
                {
                
                BlackPawnScript = Piece.GetComponent<Black_Pawn_Movement>();
                BlackPawnMove(Piece);
                if (NrOfPieceThatsOnMe != 100)
                {
                    DeletePiece();
                    m = true;
                }
                
            }
            else if (NrOfPawnThatCalledThisTile < 8)
            {
                PawnScript= Piece.GetComponent<Testing_Movement>();
                WhitePawnMove(Piece);
                if (NrOfPieceThatsOnMe != 100)
                {
                    DeletePiece();
                    m = true;
                }
          
                
            }
            else if(NrOfPawnThatCalledThisTile >=16 && NrOfPawnThatCalledThisTile < 20 )
            {
                RookScript= Piece.GetComponent<Rook_Script>();
                RookMove(Piece);
            }
            else if(NrOfPawnThatCalledThisTile >= 20 && NrOfPawnThatCalledThisTile < 24)
            {
                BishopScript = Piece.GetComponent<Bishop_Script>();
                BishopMove(Piece);
            }
            else if(NrOfPawnThatCalledThisTile >= 24 && NrOfPawnThatCalledThisTile < 26)
            {
                QueenScript= Piece.GetComponent<Queen_Script>();
                QueenMove(Piece);
            }

            if (!v && NrOfPieceThatsOnMe != 100)
                    DeletePiece();
             else if(!m)
               FindObjectOfType<AudioManager>().Play("PieceMove");




            NrOfPieceThatsOnMe = NrOfPawnThatCalledThisTile;

            UncallTiles();
            
            
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
        Occupy_Black = false;
        PawnScript.Tile_Im_On.Occupied = false;
        PawnScript.Tile_Im_On.Occupy_White = false;
        PawnScript.Tile_Im_On.NrOfPieceThatsOnMe = 100;
        PawnScript.Take_Function_Called_Left = false;
        PawnScript.Take_Function_Called_Right = false;
        NrOfPawnThatCalledThisTile = PawnScript.NrOfThisPawn;

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
        Occupy_White = false;
        BlackPawnScript.Take_Function_Called_Left = false;
        BlackPawnScript.Take_Function_Called_Right = false;
        NrOfPawnThatCalledThisTile = BlackPawnScript.NrOfThisPawn;



    }
    void RookMove(GameObject Piece)
    {
        Piece.transform.position = new Vector3((float)(position_.position.x ), (float)(position_.position.y +0.3),0);
        RookScript.position_x = position_.position.x;
        RookScript.position_y = position_.position.y + 0.3;
        RookScript.Pressed = false;
        RookScript.Tile_Im_On.Occupied = false;
        RookScript.Tile_Im_On.NrOfPieceThatsOnMe = 100;
        Occupied = true;
        if(RookScript.black)
        {
            Occupy_Black = true;
            Occupy_White = false;
            logic_Manager_.Black_Pressed = false;
            RookScript.Tile_Im_On.Occupy_Black = false;
        }
        else
        {
            Occupy_White = true;
            Occupy_Black = false;
            logic_Manager_.White_Pressed = false;
            RookScript.Tile_Im_On.Occupy_White = false;
        }
        NrOfPawnThatCalledThisTile = RookScript.NrOfThisPiece;

    }
    void BishopMove(GameObject Piece)
    {
        Piece.transform.position = new Vector3((float)(position_.position.x), (float)(position_.position.y+ 0.255), 0);
        BishopScript.position_x = position_.position.x;
        BishopScript.position_y = position_.position.y + 0.3;
        BishopScript.Pressed = false;
        BishopScript.Tile_Im_On.Occupied = false;
        BishopScript.Tile_Im_On.NrOfPieceThatsOnMe = 100;
        Occupied = true;
        if (BishopScript.black)
        {
            Occupy_Black = true;
            Occupy_White = false;
            logic_Manager_.Black_Pressed = false;
            BishopScript.Tile_Im_On.Occupy_Black = false;
        }
        else
        {
            Occupy_White = true;
            Occupy_Black = false;
            logic_Manager_.White_Pressed = false;
            BishopScript.Tile_Im_On.Occupy_White = false;
        }
        NrOfPawnThatCalledThisTile = BishopScript.NrOfThisPiece;
    }
    
    void QueenMove(GameObject Piece)
    {
        Piece.transform.position = new Vector3((float)(position_.position.x), (float)(position_.position.y + 0.2), 0);
        QueenScript.position_x = position_.position.x;
        QueenScript.position_y = position_.position.y + 0.4;
        QueenScript.Pressed = false;
        QueenScript.Tile_Im_On.Occupied = false;
        QueenScript.Tile_Im_On.NrOfPieceThatsOnMe = 100;
        Occupied = true;
        if (QueenScript.black)
        {
            Occupy_Black = true;
            Occupy_White = false;
            logic_Manager_.Black_Pressed = false;
            QueenScript.Tile_Im_On.Occupy_Black = false;
        }
        else
        {
            Occupy_White = true;
            Occupy_Black = false;
            logic_Manager_.White_Pressed = false;
            QueenScript.Tile_Im_On.Occupy_White = false;
        }
        NrOfPawnThatCalledThisTile = QueenScript.NrOfThisPiece;
    }

    public GameObject FindTile(int x)
    {
        return AllTiles[(int)((position_.position.x + 3.5)*8+position_.position.y+3.5 - x)];
    }
    void OnMouseEnter()
    {
        if(!logic_Manager_.NoHovering)
            _renderer.color = Hovercolor;
            
    }
    void OnMouseOver()
    {
        if(!logic_Manager_.NoHovering)
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
            if (_renderer.color == _offsetColor)
                _renderer.color = _DarkCalledColor;
            else
                _renderer.color = _LightCalledColor;
            logic_Manager_.NoHovering = true;
        }
    }


}