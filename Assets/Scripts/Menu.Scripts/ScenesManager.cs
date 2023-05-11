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
    public void Menu()
    {
        Debug.Log("Went to the Menu scene");
        SceneManager.LoadScene("Main Menu");
    }
    public void loadCustomize()
    {
        Debug.Log("Went to the Customize scene");
        SceneManager.LoadScene("Customize");
    }
}
