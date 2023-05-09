using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Go_To_MainMenu : MonoBehaviour
{
    public void GoMainMenu()
    { 
    Debug.Log("Went to Main Menu");
        SceneManager.LoadScene("Main Menu");
        }
}
