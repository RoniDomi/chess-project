using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_Won_or_Draw : MonoBehaviour
{
    public GameObject Logic;
    public Logic_Management_Script logic_manager;


    public GameObject whitewins;
    public GameObject blackwins;
    public GameObject stalemate;
    public GameObject time;

    // Start is called before the first frame update
    void Awake()
    {
        logic_manager = Logic.GetComponent<Logic_Management_Script>();
        whitewins.SetActive(false);
        blackwins.SetActive(false);
        stalemate.SetActive(false);
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("Game");
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    // Update is called once per frame
    void Update()
    {
        if (logic_manager.White_Wins)
        {
            whitewins.SetActive(true);
            time.SetActive(false);
        }
        if (logic_manager.Black_Wins)
        {
            blackwins.SetActive(true);
            time.SetActive(false);
        }
        if (logic_manager.StaleMate)
        {
            stalemate.SetActive(true);
            time.SetActive(false);
        }


    }
}
