using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Tile : MonoBehaviour
{
    public Color _baseColor, _offsetColor, Hovercolor, _SavedColor;
    public SpriteRenderer _renderer;

    public bool Selected = true;

    public int NrOfPawnThatCalledThisTile;

    public bool Called = false;

    public GameObject[] AllPawns;


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
        
        AllPawns = GameObject.FindGameObjectsWithTag("Pawns");

    }

    void OnMouseDown()
    {
        if (Called)
        {
            Debug.Log("Tile onmousedown called");
            GameObject Pawn = AllPawns[(int)(NrOfPawnThatCalledThisTile)];

            Pawn.transform.position = Pawn.transform.position + (new Vector3(0, 1));
            Called = false;
            Selected = true;
            _renderer.color = _SavedColor;
        }
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
        Called = true;
        _renderer.color = Hovercolor;
    }


   }