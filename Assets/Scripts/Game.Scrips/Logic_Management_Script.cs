using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Logic_Management_Script : MonoBehaviour
{
    public bool White_Pressed;
    public bool Black_Pressed;
    public bool NoHovering;
    public GameObject[] AllPieces;


    void Start()
    {
        White_Pressed=false;
        Black_Pressed=false;

    }

    void Awake()
    {
        GetPieces();
    }

    void GetPieces()
    {
        GameObject[] BlackPawns = GameObject.FindGameObjectsWithTag("BlackPawn");
        GameObject[] WhiteRooks = GameObject.FindGameObjectsWithTag("White_Rook");
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
    }


    }

