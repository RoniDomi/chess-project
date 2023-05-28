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
    public GameObject[] AllPieces;


    void Start()
    {
        White_Pressed=false;
        Black_Pressed=false;
        White_Turn = true;

    }

    void Awake()
    {
       
    }


    public void TurnChange()
    {
        //if (White_Turn)
        //{
          //  White_Turn = false;
            //Black_Turn = true;
        //}
        //else
        //{
          //  White_Turn = true;
            //Black_Turn = false;
        //}

        int x = 0;

        for (; x < 8; x++)
        {
            Testing_Movement whitepawns;
            whitepawns= AllPieces[x].GetComponent<Testing_Movement>();

            if (!whitepawns.Taken && !whitepawns.Stuck_For_Real)
            {
                whitepawns.attack();
            }

        }

        for(; x < 16; x++)
        {
            Black_Pawn_Movement blackpawns;

            blackpawns= AllPieces[x].GetComponent<Black_Pawn_Movement>();

      
            if(!blackpawns.Taken && !blackpawns.Stuck_For_Real)
            blackpawns.attack();
        }

        for(; x<20; x++)
        {
            Rook_Script rooks;
            rooks = AllPieces[x].GetComponent<Rook_Script>();
            
            if(!rooks.Taken && !rooks.Stuck)
            rooks.attack();
        }
        for(;x<24; x++)
        {
            Bishop_Script bishops;
            bishops = AllPieces[x].GetComponent<Bishop_Script>();

            if(!bishops.Taken && !bishops.Stuck)
            bishops.attack();
        }
        for(;x<26;x++)
        {
            Queen_Script queens;
            queens = AllPieces[x].GetComponent<Queen_Script>();
   
            if(!queens.Taken && !queens.Stuck)
                queens.attack();
        }
        for (; x < 30; x++)
        {
            Knight_Script knights;
            knights = AllPieces[x].GetComponent<Knight_Script>();
     
            if (!knights.Taken && !knights.Stuck)
                knights.attack();
        }



    }


    }

