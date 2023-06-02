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
    public bool White_Wins;
    public bool Black_Wins;
    public bool StaleMate;
    public int stalemate_white=0;
    public int stalemate_black=0;

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
           

            if (whitepawns.Tile_Im_On.Checked_Black && !whitepawns.Taken)
                whitedouble++;
            

        }
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

        for (; x < 24; x++)
        {
            Bishop_Script bishops;
            bishops = AllPieces[x].GetComponent<Bishop_Script>();

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

        for (; x < 26; x++)
        {
            Queen_Script queens;
            queens = AllPieces[x].GetComponent<Queen_Script>();


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

        for (; x < 30; x++)
        {
            Knight_Script knights;
            knights = AllPieces[x].GetComponent<Knight_Script>();

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
        stalemate_black = 0;
        stalemate_white = 0;

        for (; x < 8; x++)
        {
            Testing_Movement whitepawns;
            whitepawns= AllPieces[x].GetComponent<Testing_Movement>();

            if (!whitepawns.Taken)
            {
                whitepawns.attack();
                
            }
            if(whitepawns.Taken || whitepawns.Stuck_For_Real)
                {
                stalemate_white++;
                }

        }

        for (; x < 16; x++)
        {
            Black_Pawn_Movement blackpawns;

            blackpawns = AllPieces[x].GetComponent<Black_Pawn_Movement>();


            if (!blackpawns.Taken) { 
                blackpawns.attack();
            }

            if (blackpawns.Taken || blackpawns.Stuck_For_Real)
            {
                stalemate_black++;
            }
        }

        for(; x<20; x++)
        {
            Rook_Script rooks;
            rooks = AllPieces[x].GetComponent<Rook_Script>();

            if (!rooks.Taken)
            {
                rooks.attack();

            }
            if(rooks.white)
            {
                if ((rooks.Taken || rooks.Stuck))
                    stalemate_white++;
            }    
            else
            {
                if ((rooks.Taken || rooks.Stuck))
                    stalemate_black++;

            }

        }
        for(;x<24; x++)
        {
            Bishop_Script bishops;
            bishops = AllPieces[x].GetComponent<Bishop_Script>();

            if (!bishops.Taken)
            {
                bishops.attack();
             
            }
                if (bishops.white)
            {
                if ((bishops.Taken || bishops.Stuck))
                    stalemate_white++;
            }
            else
            {
                if ((bishops.Taken || bishops.Stuck))
                    stalemate_black++;

            }
        }
        for(;x<26;x++)
        {
            Queen_Script queens;
            queens = AllPieces[x].GetComponent<Queen_Script>();

            if (!queens.Taken)
            {
                queens.attack();

            }
            if (queens.white)
            {
                if ((queens.Taken || queens.Stuck))
                    stalemate_white++;
            }
            else
            {
                if ((queens.Taken || queens.Stuck))
                    stalemate_black++;

            }
        }
        for (; x < 30; x++)
        {
            Knight_Script knights;
            knights = AllPieces[x].GetComponent<Knight_Script>();

            if (!knights.Taken)
            {
                knights.attack();

            }
            if (knights.white)
            {
                if ((knights.Taken || knights.Stuck))
                    stalemate_white++;
            }
            else
            {
                if ((knights.Taken || knights.Stuck))
                    stalemate_black++;

            }
        }
        for(; x < 32; x++)
        {
            king = AllPieces[x].GetComponent<King_Script>();
            if (king.white)
            {
                
                if (king.Tile_Im_On.Attacked_Black_Secondary)
                    king.PinPiece();
            }
            else
            {
                
                if (king.Tile_Im_On.Attacked_White_Secondary)
                    king.PinPiece();
            }
            king.attack();


            if (king.white)
            {
                if ((king.Stuck))
                    stalemate_white++;
            }
            else
            {
                if ((king.Stuck))
                    stalemate_black++;

            }

        }
        
          if (White_Turn)
         {
            king = AllPieces[31].GetComponent<King_Script>();
                king.FindTileImOn();
            if (king.Tile_Im_On.Attacked_White)
               {
                check_black = true;
            
            }

            if (stalemate_black == 16 && check_black)
            {
                White_Wins = true;
            }
            else if (stalemate_black == 16)
            {
                StaleMate = true;
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
            if (stalemate_white == 16 && check_white)
            {
                Black_Wins = true;
            }
            else if (stalemate_white == 16)
            {
                StaleMate = true;
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



    }

    void Update()
    {
        int x = 0;
        stalemate_black = 0;
        stalemate_white = 0;

        King_Script king;

        for (; x < 8; x++)
        {
            Testing_Movement whitepawns;
            whitepawns = AllPieces[x].GetComponent<Testing_Movement>();

            if (!whitepawns.Taken)
            {
                whitepawns.attack();
                whitepawns.CheckIfStuckForReal();
            }
            if (whitepawns.Taken || whitepawns.Stuck_For_Real)
            {
                stalemate_white++;
            }

        }

        for (; x < 16; x++)
        {
            Black_Pawn_Movement blackpawns;

            blackpawns = AllPieces[x].GetComponent<Black_Pawn_Movement>();


            if (!blackpawns.Taken)
            {
                blackpawns.attack();
                blackpawns.CheckIfStuckForReal();
            }

            if (blackpawns.Taken || blackpawns.Stuck_For_Real)
            {
                stalemate_black++;
            }
        }

        for (; x < 20; x++)
        {
            Rook_Script rooks;
            rooks = AllPieces[x].GetComponent<Rook_Script>();

            if (!rooks.Taken)
            {
                rooks.attack();
                rooks.CheckIfStuck();
            }
            if (rooks.white)
            {
                if ((rooks.Taken || rooks.Stuck))
                    stalemate_white++;
            }
            else
            {
                if ((rooks.Taken || rooks.Stuck))
                    stalemate_black++;

            }

        }
        for (; x < 24; x++)
        {
            Bishop_Script bishops;
            bishops = AllPieces[x].GetComponent<Bishop_Script>();

            if (!bishops.Taken)
            {
                bishops.attack();
                bishops.CheckIfStuck();
            }
            if (bishops.white)
            {
                if ((bishops.Taken || bishops.Stuck))
                    stalemate_white++;
            }
            else
            {
                if ((bishops.Taken || bishops.Stuck))
                    stalemate_black++;

            }
        }
        for (; x < 26; x++)
        {
            Queen_Script queens;
            queens = AllPieces[x].GetComponent<Queen_Script>();

            if (!queens.Taken)
            {
                queens.attack();
                queens.CheckIfStuck();
            }
            if (queens.white)
            {
                if ((queens.Taken || queens.Stuck))
                    stalemate_white++;
            }
            else
            {
                if ((queens.Taken || queens.Stuck))
                    stalemate_black++;

            }
        }
        for (; x < 30; x++)
        {
            Knight_Script knights;
            knights = AllPieces[x].GetComponent<Knight_Script>();

            if (!knights.Taken)
            {
                knights.attack();
                knights.CheckIfStuck();

            }
            if (knights.white)
            {
                if ((knights.Taken || knights.Stuck))
                    stalemate_white++;
            }
            else
            {
                if ((knights.Taken || knights.Stuck))
                    stalemate_black++;

            }
        }
        for (; x < 32; x++)
        {
            king = AllPieces[x].GetComponent<King_Script>();
            king.CheckIfStuck();
            if (king.white)
            {

                if (king.Tile_Im_On.Attacked_Black_Secondary)
                    king.PinPiece();
            }
            else
            {

                if (king.Tile_Im_On.Attacked_White_Secondary)
                    king.PinPiece();
            }
            king.attack();


            if (king.white)
            {
                if ((king.Stuck))
                    stalemate_white++;
            }
            else
            {
                if ((king.Stuck))
                    stalemate_black++;

            }

        }

        if (Black_Turn)
        {
    
            if (stalemate_black == 16 && check_black)
            {
                White_Wins = true;
            }
            else if (stalemate_black == 16)
            {
                StaleMate = true;
            }

        }
        else
        {

            if (stalemate_white == 16 && check_white)
            {
                Black_Wins = true;
            }
            else if (stalemate_white == 16)
            {
                StaleMate = true;
            }
           
        }
        

    }

}

