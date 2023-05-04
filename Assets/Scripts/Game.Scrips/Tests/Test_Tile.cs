using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Tile : MonoBehaviour
{
    public Color _baseColor, _offsetColor, Hovercolor, _SavedColor;
    public SpriteRenderer _renderer;


    public void Start()
    {
        Debug.Log("Color Changed");

        if (((transform.position.x + 3.5f)%2 == 0 && (transform.position.y + 3.5f)%2 != 0) || ((transform.position.x + 3.5f)%2 != 0 && (transform.position.y + 3.5f)%2 == 0))
        {
            _renderer.color = _offsetColor ;
        }
        else
        {
            _renderer.color = _baseColor ;
        }
        _SavedColor = _renderer.color;
    }

        void OnMouseEnter()
        {
            _renderer.color = Hovercolor;
            Debug.Log("Enter " + gameObject.name);
        }
         void OnMouseOver()
        {
            _renderer.color = Hovercolor;
            Debug.Log("Over " + gameObject.name );
        }
    void OnMouseExit()
    {
        _renderer.color = _SavedColor;
    }
    
}