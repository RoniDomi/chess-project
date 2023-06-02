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
    public int whitedouble = 0;
    public int blackdouble = 0;

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
        whitedouble = 0;
        blackdouble = 0;

        for (; x < 8; x++)
        {
            Testing_Movement whitepawns;
            whitepawns = AllPieces[x].GetComponent<Testing_Movement>();
            whitepawns.FindTileImOn();

            if (whitepawns.Tile_Im_On.Checked_Black && !whitepawns.Taken)
                whitedouble++;
            

        }
        Debug.Log(whitedouble);
        for (; x < 16; x++)
        {
            Black_Pawn_Movement blackpawns;

            blackpawns = AllPieces[x].GetComponent<Black_Pawn_Movement>();
            blackpawns.FindTileImOn();
            if (blackpawns.Tile_Im_On.Checked_White && !blackpawns.Taken)
                blackdouble++;

        }

        for (; x < 20; x++)
        {
            Rook_Script rooks;
            rooks = AllPieces[x].GetComponent<Rook_Script>();
            rooks.FindTileImOn();

            if (rooks.white)
            {
                if (rooks.Tile_Im_On.Checked_Black && rooks.Taken)
                    whitedouble++;
            }
            else
            {
                if (rooks.Tile_Im_On.Checked_White && !rooks.Taken)
                    blackdouble++;
            }

           

        }
         Debug.Log(whitedouble);
        for (; x < 24; x++)
        {
            Bishop_Script bishops;
            bishops = AllPieces[x].GetComponent<Bishop_Script>();
            bishops.FindTileImOn();

            if (bishops.white)
            {
                if (bishops.Tile_Im_On.Checked_Black && !bishops.Taken) 
                    whitedouble++;
            }
            else
            {
                if (bishops.Tile_Im_On.Checked_White && !bishops.Taken)
                    blackdouble++;
            }

            
        }
         Debug.Log(whitedouble);
        for (; x < 26; x++)
        {
            Queen_Script queens;
            queens = AllPieces[x].GetComponent<Queen_Script>();
            queens.FindTileImOn();

            if (queens.white)
            {
                if (queens.Tile_Im_On.Checked_Black && !queens.Taken)
                    whitedouble++;
            }
            else
            {
                if (queens.Tile_Im_On.Checked_White && !queens.Taken)
                    blackdouble++;
            }
            
        }
         Debug.Log(whitedouble);
        for (; x < 30; x++)
        {
            Knight_Script knights;
            knights = AllPieces[x].GetComponent<Knight_Script>();
            knights.FindTileImOn();

            if (knights.white)
            {
                if (knights.Tile_Im_On.Checked_Black && !knights.Taken)
                    whitedouble++;
            }
            else
            {
                if (knights.Tile_Im_On.Checked_White && !knights.Taken)
                    blackdouble++;
            }
           
        }
       Debug.Log(whitedouble);
        if(whitedouble == 2)
        {
            double_check_black = true;
        }
        else
            double_check_black = false;
        if (blackdouble == 2)
        {
            double_check_white = true;
        }
        else
            double_check_white = false;

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
               }
            king = AllPieces[30].GetComponent<King_Script>();
            king.FindTileImOn();
            if (king.Tile_Im_On.Attacked_Black)
            {
                check_white = true;
                
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
                }
            king = AllPieces[31].GetComponent<King_Script>();
            king.FindTileImOn();
            if (king.Tile_Im_On.Attacked_White)
            {
                check_black = true;
                
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

