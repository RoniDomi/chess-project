using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;
    public GameObject warningMenu;
    public GameObject warningExit;
    public Text blacktime;
    public Text whitetime;
    public GameObject logic;
    public GameObject timepanel;
    public TIme_Control_Script time;


    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        timepanel.SetActive(true);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    public void Pause()
    {
        time = logic.GetComponent<TIme_Control_Script>();
        pauseMenuUI.SetActive(true);
        timepanel.SetActive(false);
        blacktime.text = time.Black_Time.text;
        whitetime.text = time.White_Time.text;

        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void LoadMenuWarning()

    {
        warningMenu.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
    public void LoadExitWarning()

    {
        warningExit.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void UnloadMenuWarning()

    {
        warningMenu.SetActive(false);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
    public void UnloadExitWarning()

    {
        warningExit.SetActive(false);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void LoadMenu()

    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main Menu");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting");
        Application.Quit();
    }
}