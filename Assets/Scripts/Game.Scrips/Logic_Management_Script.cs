using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Logic_Management_Script : MonoBehaviour
{
    public bool White_Pressed;
    public bool Black_Pressed;
    public bool NoHovering;
    public bool White_Turn;
    public bool Black_Turn;
    public bool check_white;
    public bool check_black;
    public bool double_check_white;
    public bool double_check_black;


    public GameObject[] AllPieces;


    void Start()
    {
        White_Pressed=false;
        Black_Pressed=false;
        White_Turn = true;

    }

    public void Check_For_Double_Check()
    {
        int x = 0;
        int whitedouble = 0;
        int blackdouble = 0;

        for (; x < 8; x++)
        {
            Testing_Movement whitepawns;
            whitepawns = AllPieces[x].GetComponent<Testing_Movement>();
            whitepawns.FindTileImOn();
            if (whitepawns.Tile_Im_On.Checked_Black)
                whitedouble++;


        }

        for (; x < 16; x++)
        {
            Black_Pawn_Movement blackpawns;

            blackpawns = AllPieces[x].GetComponent<Black_Pawn_Movement>();
            blackpawns.FindTileImOn();
            if (blackpawns.Tile_Im_On.Checked_White)
                blackdouble++;


        }

        for (; x < 20; x++)
        {
            Rook_Script rooks;
            rooks = AllPieces[x].GetComponent<Rook_Script>();
            rooks.FindTileImOn();

            if (rooks.white)
            {
                if (rooks.Tile_Im_On.Checked_Black)
                    whitedouble++;
            }
            else
            {
                if (rooks.Tile_Im_On.Checked_White)
                    blackdouble++;
            }

        }
        for (; x < 24; x++)
        {
            Bishop_Script bishops;
            bishops = AllPieces[x].GetComponent<Bishop_Script>();
            bishops.FindTileImOn();

            if (bishops.white)
            {
                if (bishops.Tile_Im_On.Checked_Black)
                    whitedouble++;
            }
            else
            {
                if (bishops.Tile_Im_On.Checked_White)
                    blackdouble++;
            }
        }
        for (; x < 26; x++)
        {
            Queen_Script queens;
            queens = AllPieces[x].GetComponent<Queen_Script>();
            queens.FindTileImOn();

            if (queens.white)
            {
                if (queens.Tile_Im_On.Checked_Black)
                    whitedouble++;
            }
            else
            {
                if (queens.Tile_Im_On.Checked_White)
                    blackdouble++;
            }
        }
        for (; x < 30; x++)
        {
            Knight_Script knights;
            knights = AllPieces[x].GetComponent<Knight_Script>();
            knights.FindTileImOn();

            if (knights.white)
            {
                if (knights.Tile_Im_On.Checked_Black)
                    whitedouble++;
            }
            else
            {
                if (knights.Tile_Im_On.Checked_White)
                    blackdouble++;
            }

        }
        if(whitedouble>=2)
        {
            double_check_black = true;
        }
       if(blackdouble>=2)
        {
            double_check_white = true;
        }

    }


    public void TurnChange()
    {
        King_Script king;
        
        int x = 0;

        for (; x < 8; x++)
        {
            Testing_Movement whitepawns;
            whitepawns= AllPieces[x].GetComponent<Testing_Movement>();

            if (!whitepawns.Taken)
            {
                whitepawns.attack();
            }

        }

        for(; x < 16; x++)
        {
            Black_Pawn_Movement blackpawns;

            blackpawns= AllPieces[x].GetComponent<Black_Pawn_Movement>();

      
            if(!blackpawns.Taken)
            blackpawns.attack();
        }

        for(; x<20; x++)
        {
            Rook_Script rooks;
            rooks = AllPieces[x].GetComponent<Rook_Script>();
            
            if(!rooks.Taken)
            rooks.attack();
        }
        for(;x<24; x++)
        {
            Bishop_Script bishops;
            bishops = AllPieces[x].GetComponent<Bishop_Script>();

            if(!bishops.Taken)
            bishops.attack();
        }
        for(;x<26;x++)
        {
            Queen_Script queens;
            queens = AllPieces[x].GetComponent<Queen_Script>();
   
            if(!queens.Taken)
                queens.attack();
        }
        for (; x < 30; x++)
        {
            Knight_Script knights;
            knights = AllPieces[x].GetComponent<Knight_Script>();
     
            if (!knights.Taken)
                knights.attack();
        }
        for(; x < 32; x++)
        {
            king = AllPieces[x].GetComponent<King_Script>();
            if (king.white)
            {
                king.FindTileImOn();
                if (king.Tile_Im_On.Attacked_Black_Secondary)
                    king.PinPiece();
            }
            else
            {
                king.FindTileImOn();
                if (king.Tile_Im_On.Attacked_White_Secondary)
                    king.PinPiece();
            }
            king.attack();

        }
        
          if (White_Turn)
         {
            king = AllPieces[31].GetComponent<King_Script>();
                king.FindTileImOn();
            if (king.Tile_Im_On.Attacked_White) {
                check_black = true;
                Debug.Log("check black king"); }
            king = AllPieces[30].GetComponent<King_Script>();
            king.FindTileImOn();
            if (king.Tile_Im_On.Attacked_Black)
            {
                check_white = true;
                Debug.Log("check white king");
            }
            White_Turn = false;
          Black_Turn = true;
        }
        else
        {
            king = AllPieces[30].GetComponent<King_Script>();
                king.FindTileImOn();
            if (king.Tile_Im_On.Attacked_Black) {
                check_white = true;
                Debug.Log("check white king"); }
            king = AllPieces[31].GetComponent<King_Script>();
            king.FindTileImOn();
            if (king.Tile_Im_On.Attacked_White)
            {
                check_black = true;
                Debug.Log("check black king");
            }
            White_Turn = true;
          Black_Turn = false;
        }
        if (check_white)
        {
            king = AllPieces[30].GetComponent<King_Script>();
            king.FindCheckedTiles();
        }
        else if(check_black)
        {
            king = AllPieces[31].GetComponent<King_Script>();
            king.FindCheckedTiles();
        }


        White_Turn = true;
        Black_Turn = true;
    }


    }

