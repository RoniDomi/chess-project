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
}
