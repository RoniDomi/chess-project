using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Test_Tile : MonoBehaviour
{
    public Color _baseColor, _offsetColor, Hovercolor, _SavedColor,_TakeColorLight,_TakeColorDark,_DarkCalledColor,_LightCalledColor;
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
    public Knight_Script KnightScript;
    public King_Script KingScript;
    public bool Attacked_White = false;
    public bool Attacked_Black = false;
    public bool Attacked_White_Secondary = false;
    public bool Attacked_Black_Secondary = false;
    public bool Occupy_White=false;
    public bool Occupy_Black=false;
    public bool king_white =false;
    public bool king_black = false;
    public int NrOfPieceThatsOnMe;
    public bool En_passant_Active_White = false;
    public bool En_passant_Active_Black = false;
    public bool Vertical_Edge_Up = false;
    public bool Vertical_Edge_Down = false;
    public bool Horizontal_Edge_Left = false;
    public bool Horizontal_Edge_Right = false;
    public bool Castle_Queen_Side;
    public bool Castle_King_Side;
    public Rook_Script RookScript;
    public GameObject Logic;
    public Logic_Management_Script logic_Manager_;
    public bool PiecePinned;
    public bool pinnedTile_White;
    public bool pinnedTile_Black;
    public bool pinnedTile_White_Bishop;
    public bool pinnedTile_Black_Bishop;
    public bool canbepinned_white;
    public bool canbepinned_black;
    public bool canbepinned_white_Bishop;
    public bool canbepinned_black_Bishop;
    public bool Checked_White;
    public bool Checked_Black;
    public bool canbetaken_white;
    public bool canbetaken_black;

    bool WhitePiece(Test_Tile tile)
    {
        if (tile.NrOfPieceThatsOnMe < 8 || tile.NrOfPieceThatsOnMe == 16 || tile.NrOfPieceThatsOnMe == 17 || tile.NrOfPieceThatsOnMe == 20 || tile.NrOfPieceThatsOnMe == 21 || tile.NrOfPieceThatsOnMe == 24 || tile.NrOfPieceThatsOnMe == 26 || tile.NrOfPieceThatsOnMe == 27 || tile.NrOfPieceThatsOnMe == 30)
            return true;
        return false;
    }
    bool BlackPiece(Test_Tile tile)
    {
        {
            if ((tile.NrOfPieceThatsOnMe >= 8 && tile.NrOfPieceThatsOnMe < 16) || tile.NrOfPieceThatsOnMe == 18 || tile.NrOfPieceThatsOnMe == 19 || tile.NrOfPieceThatsOnMe == 22 || tile.NrOfPieceThatsOnMe == 23 || tile.NrOfPieceThatsOnMe == 25 || tile.NrOfPieceThatsOnMe == 28 || tile.NrOfPieceThatsOnMe == 29 || tile.NrOfPieceThatsOnMe == 31)
                return true;
            return false;
        }
    }

    public void Start()
    {
        Logic= GameObject.FindGameObjectWithTag("Logic_Manager");

        logic_Manager_ = Logic.GetComponent<Logic_Management_Script>();

        NrOfPieceThatsOnMe = 100;


        if (((transform.position.x + 3.5f) % 2 == 0 && (transform.position.y + 3.5f) % 2 != 0) || ((transform.position.x + 3.5f) % 2 != 0 && (transform.position.y + 3.5f) % 2 == 0))
        {
            _renderer.color = _offsetColor;
        }
        else
        {
            _renderer.color = _baseColor;
        }
        _SavedColor = _renderer.color;

        GameObject piece;

        for(int i=0;i<32;i++)
        {
            piece = logic_Manager_.AllPieces[i];
            AllPieces[i] = piece;
        }

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
    public void undoattacks()
    {
        Test_Tile Tiles_In_This_loop;
        for (int x = 0; x < 64; x++)
        {
            Tiles_In_This_loop = AllTiles[x].GetComponent<Test_Tile>();
            Tiles_In_This_loop.Attacked_White = false;
            Tiles_In_This_loop.Attacked_Black = false;
            Tiles_In_This_loop.Attacked_White_Secondary = false;
            Tiles_In_This_loop.Attacked_Black_Secondary = false;
            Tiles_In_This_loop.pinnedTile_White = false;
            Tiles_In_This_loop.pinnedTile_Black = false;
            Tiles_In_This_loop.pinnedTile_White_Bishop = false;
            Tiles_In_This_loop.pinnedTile_Black_Bishop = false;
            Tiles_In_This_loop.canbepinned_white = false;
            Tiles_In_This_loop.canbepinned_black = false;
            Tiles_In_This_loop.canbepinned_white_Bishop = false;
            Tiles_In_This_loop.canbepinned_black_Bishop = false;
            Tiles_In_This_loop.Checked_White = false;
            Tiles_In_This_loop.Checked_Black = false;
            Tiles_In_This_loop.canbetaken_black = false;
            Tiles_In_This_loop.canbetaken_white = false;
            Tiles_In_This_loop.king_white = false;
            Tiles_In_This_loop.king_black = false;
            Tiles_In_This_loop.NrOfPawnThatCalledThisTile = 100;
            if (Tiles_In_This_loop.NrOfPieceThatsOnMe == 100)
            {
                Tiles_In_This_loop.Occupy_Black = false;
                Tiles_In_This_loop.Occupy_White = false;
            }
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
                Tiles_In_This_loop.Selected = true;
                Tiles_In_This_loop._renderer.color = Tiles_In_This_loop._SavedColor;
                Tiles_In_This_loop.Castle_King_Side = false;
                Tiles_In_This_loop.Castle_Queen_Side = false;

               if (WhitePiece(Tiles_In_This_loop) && !En_passant_Active_White)
                    {
                       
                       
                           Tiles_In_This_loop.Occupy_White = true; 

                        
                        
                    }
                else if (BlackPiece(Tiles_In_This_loop) && !En_passant_Active_Black)
                    {
                       
                            Tiles_In_This_loop.Occupy_Black = true;
                        
                    }
                



               
            }
            Tiles_In_This_loop.Called = false;
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
            
            if (!whitepawns.Taken)
            {
                whitepawns.CheckIfStuckForReal();
                whitepawns.SetGame();
            }

        }

        for(; x < 16; x++)
        {
            Black_Pawn_Movement blackpawns;

            blackpawns= AllPieces[x].GetComponent<Black_Pawn_Movement>();


            if (!blackpawns.Taken)
            {
                blackpawns.CheckIfStuckForReal();
                blackpawns.SetGame();
            }
        }

        for(; x<20; x++)
        {
            Rook_Script rooks;
            rooks = AllPieces[x].GetComponent<Rook_Script>();

            if (!rooks.Taken)
            {
                rooks.CheckIfStuck();
                rooks.SetGame();
            }
        }
        for(;x<24; x++)
        {
            Bishop_Script bishops;
            bishops = AllPieces[x].GetComponent<Bishop_Script>();

            if (!bishops.Taken)
            {
                bishops.CheckIfStuck();
                bishops.SetGame();
            }
        }
        for(;x<26;x++)
        {
            Queen_Script queens;
            queens = AllPieces[x].GetComponent<Queen_Script>();

            if (!queens.Taken)
            {
                queens.CheckIfStuck();
                queens.SetGame();
            }
        }
        for (; x < 30; x++)
        {
            Knight_Script knights;
            knights = AllPieces[x].GetComponent<Knight_Script>();

            if (!knights.Taken)
            {
                knights.CheckIfStuck();
                knights.SetGame();
            }
        }
        for (; x < 32; x++)
        {
            King_Script kings;
            kings = AllPieces[x].GetComponent<King_Script>();
            kings.CheckIfStuck();

        }




    }

    public void DeletePiece()
    {
        FindObjectOfType<AudioManager>().Play("PieceTake");
        AllPieces[NrOfPieceThatsOnMe].transform.position += new Vector3(0, 0, 5);
        if (NrOfPieceThatsOnMe >= 8 && NrOfPieceThatsOnMe < 16)
        {

            BlackPawnScript = AllPieces[NrOfPieceThatsOnMe].GetComponent<Black_Pawn_Movement>();
            BlackPawnScript.Tile_Im_On.Occupy_Black = false;
            BlackPawnScript.Tile_Im_On.En_passant_Active_Black = false;
            BlackPawnScript.Taken = true;
            BlackPawnScript.Tile_Im_On.NrOfPieceThatsOnMe = 100;
        }
        else if (NrOfPieceThatsOnMe < 8)
        {

            PawnScript = AllPieces[NrOfPieceThatsOnMe].GetComponent<Testing_Movement>();
            PawnScript.Tile_Im_On.Occupy_White = false;
            PawnScript.Tile_Im_On.En_passant_Active_White = false;
            PawnScript.Taken = true;
            PawnScript.Tile_Im_On.NrOfPieceThatsOnMe = 100;
        }
        else if (NrOfPieceThatsOnMe >= 16 && NrOfPieceThatsOnMe < 20)
        {
            RookScript = AllPieces[NrOfPieceThatsOnMe].GetComponent<Rook_Script>();
            if (RookScript.white)
                RookScript.Tile_Im_On.Occupy_White = false;
            else
                RookScript.Tile_Im_On.Occupy_Black = false;
            RookScript.Taken = true;
            RookScript.Tile_Im_On.NrOfPieceThatsOnMe = 100;
        }
        else if (NrOfPieceThatsOnMe >= 20 && NrOfPieceThatsOnMe < 24)
        {
            BishopScript = AllPieces[NrOfPieceThatsOnMe].GetComponent<Bishop_Script>();
            if (BishopScript.white)
                BishopScript.Tile_Im_On.Occupy_White = false;
            else
                BishopScript.Tile_Im_On.Occupy_Black = false;
            BishopScript.Taken = true;
            BishopScript.Tile_Im_On.NrOfPieceThatsOnMe = 100;
        }
        else if (NrOfPieceThatsOnMe >= 24 && NrOfPieceThatsOnMe < 26)
        {
            QueenScript = AllPieces[NrOfPieceThatsOnMe].GetComponent<Queen_Script>();
            if (QueenScript.white)
                QueenScript.Tile_Im_On.Occupy_White = false;
            else
                QueenScript.Tile_Im_On.Occupy_Black = false;
            QueenScript.Taken = true;
            QueenScript.Tile_Im_On.NrOfPieceThatsOnMe = 100;
        }
        else if (NrOfPieceThatsOnMe >= 26 && NrOfPieceThatsOnMe < 30)
        {
            KnightScript = AllPieces[NrOfPieceThatsOnMe].GetComponent<Knight_Script>();
            if (KnightScript.white)
                KnightScript.Tile_Im_On.Occupy_White = false;
            else
                KnightScript.Tile_Im_On.Occupy_Black = false;
            KnightScript.Taken = true;
            KnightScript.Tile_Im_On.NrOfPieceThatsOnMe = 100;
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
            else if(NrOfPawnThatCalledThisTile >= 26 && NrOfPawnThatCalledThisTile < 30)
            {
                KnightScript= Piece.GetComponent<Knight_Script>();
                KnightMove(Piece);
            }
            else if(NrOfPawnThatCalledThisTile >= 30 && NrOfPawnThatCalledThisTile < 32)
            {
                KingScript = Piece.GetComponent<King_Script>();
                KingMove(Piece);
            }

            if (!v && NrOfPieceThatsOnMe != 100)
                    DeletePiece();
             else if(!m)
               FindObjectOfType<AudioManager>().Play("PieceMove");




            

            

            logic_Manager_.check_white = false;
            logic_Manager_.check_black = false;

           

            Check_Which_Pieces_Are_Stuck();

            UncallTiles();

            NrOfPieceThatsOnMe = NrOfPawnThatCalledThisTile;
            

            undoattacks();

            logic_Manager_.TurnChange();
            logic_Manager_.Check_For_Double_Check();

            
        }


    }


    void WhitePawnMove(GameObject Pawn)
    {
        PawnScript.IfNotOnlyPressed();
        Pawn.transform.position = new Vector3((float)(position_.position.x), (float)(position_.position.y + 0.4));
        PawnScript.NrOfThisPawn_x = NrOfThisTile_x;
        PawnScript.NrOfThisPawn_y = NrOfThisTile_y;
        PawnScript.Pressed = false;
       
        if(PawnScript.FirstMove)
        {
            GameObject previoustile = PawnScript.FindTile(-2);
            Test_Tile tile = previoustile.GetComponent<Test_Tile>();
            tile.En_passant_Active_White = true;
            tile.NrOfPieceThatsOnMe = PawnScript.NrOfThisPawn;
        }
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

        if (BlackPawnScript.FirstMove)
        {
            GameObject previoustile = BlackPawnScript.FindTile(0);
            Test_Tile tile = previoustile.GetComponent<Test_Tile>();
            tile.En_passant_Active_Black = true;
            tile.NrOfPieceThatsOnMe = BlackPawnScript.NrOfThisPawn;
        }
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
        RookScript= Piece.GetComponent<Rook_Script>(); ;
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
        if(RookScript.Castling)
        {
            RookScript.Castling = false;
        }

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

    void KnightMove(GameObject Piece)
    {
        Piece.transform.position = new Vector3((float)(position_.position.x), (float)(position_.position.y + 0.3), 0);
        KnightScript.position_x = position_.position.x;
        KnightScript.position_y = position_.position.y + 0.3;
        KnightScript.Pressed = false;
        KnightScript.Tile_Im_On.Occupied = false;
        KnightScript.Tile_Im_On.NrOfPieceThatsOnMe = 100;
        Occupied = true;
        if (KnightScript.black)
        {
            Occupy_Black = true;
            Occupy_White = false;
            logic_Manager_.Black_Pressed = false;
            KnightScript.Tile_Im_On.Occupy_Black = false;
        }
        else
        {
            Occupy_White = true;
            Occupy_Black = false;
            logic_Manager_.White_Pressed = false;
            KnightScript.Tile_Im_On.Occupy_White = false;
        }
        NrOfPawnThatCalledThisTile = KnightScript.NrOfThisPiece;
    }

    void KingMove(GameObject Piece)
    {
        Piece.transform.position = new Vector3((float)(position_.position.x), (float)(position_.position.y + 0.15), 0);
        KingScript.position_x = position_.position.x;
        KingScript.position_y = position_.position.y + 0.3;
        KingScript.Pressed = false;
        KingScript.Tile_Im_On.Occupied = false;
        KingScript.Tile_Im_On.NrOfPieceThatsOnMe = 100;
        Occupied = true;
        if (KingScript.white)
            king_white = true;
        else
            king_black = true;
        if (KingScript.black)
        {
            Occupy_Black = true;
            Occupy_White = false;
            logic_Manager_.Black_Pressed = false;
            KingScript.Tile_Im_On.Occupy_Black = false;
            
        }
        else
        {
            Occupy_White = true;
            Occupy_Black = false;
            logic_Manager_.White_Pressed = false;
            KingScript.Tile_Im_On.Occupy_White = false;
        }
        if (KingScript.white)
            KingScript.Tile_Im_On.king_white = false;
        else
            KingScript.Tile_Im_On.king_black = false;
     
        NrOfPawnThatCalledThisTile = KingScript.NrOfThisPiece;
        if(Castle_King_Side)
        {
            Test_Tile tile;
            tile=KingScript.FindTile(-9).GetComponent<Test_Tile>();
            tile.RookMove(KingScript.Rooks[0]);
            Debug.Log("castle");
        }
        if (Castle_Queen_Side)
        {
            Test_Tile tile;
            tile = KingScript.FindTile(7).GetComponent<Test_Tile>();
            tile.RookMove(KingScript.Rooks[1]);
            Debug.Log("castle");
        }
        KingScript.Castling = false;
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

    void Update()
    {
        if((Checked_Black || Checked_White) && !logic_Manager_.NoHovering)
       {
         if(_renderer.color==_offsetColor)
       {
         _renderer.color = _TakeColorDark;
       }else if (_renderer.color==_baseColor)
       {
         _renderer.color = _TakeColorLight;
       }
               
       }
       else if((_renderer.color == _TakeColorDark || _renderer.color == _TakeColorLight) && !logic_Manager_.NoHovering)
       {
       _renderer.color = _SavedColor;
       }
    }

}