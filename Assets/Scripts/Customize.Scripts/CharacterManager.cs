using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterManager : MonoBehaviour
{
    public CharacterDatabase characterDB;

    public SpriteRenderer pieceSprite;

    private int selectedOption = 0;

    void Start()
    {
        if(!PlayerPrefs.HasKey("selectedOption"))
        {
            selectedOption = 0;
        }

        else
        {
            Load();
        }

        updatePiece(selectedOption);
    }

    public void nextOption()
    {
        selectedOption++;

        if(selectedOption >= characterDB.CharacterCount)
        {
            selectedOption = 0;
        }

        updatePiece(selectedOption);
        Save();
    }

    public void backOption()
    {
        selectedOption--;

        if(selectedOption < 0)
        {
            selectedOption = characterDB.CharacterCount - 1;
        }

        updatePiece(selectedOption);
        Save();
    }

    private void updatePiece(int selectedOption)
    {
        Character character = characterDB.GetCharacter(selectedOption);
        pieceSprite.sprite = character.characterSprtie;
    }

    private void Load()
    {
        selectedOption = PlayerPrefs.GetInt("selectedOption");
        // setting the value of the selected option to the value stored in the keyname 
    }

    private void Save()
    {
        PlayerPrefs.SetInt("selectedOption", selectedOption);
        // stores selected option variable into the selected option key name
    }
}
  
