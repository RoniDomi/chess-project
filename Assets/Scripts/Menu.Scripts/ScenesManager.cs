using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
  public void Gamestart()
    {
        Debug.Log("Went to the Game");
        SceneManager.LoadScene("Game");
    }

    public void Customize()
    {
        Debug.Log("Went to customize scene");
        SceneManager.LoadScene("Customize");
    }

    public void Menu()
    {
        Debug.Log("Went to menu");
        SceneManager.LoadScene("Main Menu");
    }
}
