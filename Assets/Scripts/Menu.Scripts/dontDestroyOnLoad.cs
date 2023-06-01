using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class dontDestroyOnLoad : MonoBehaviour
{
    private void Awake()
    {
        GameObject[] musicObj = GameObject.FindGameObjectsWithTag("GameMusic");
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        if (musicObj.Length > 1)
        {
            Destroy(this.gameObject);
            // Destroy the game object if there's more than one music object
        }
        DontDestroyOnLoad(this.gameObject);
        // Otherwise keep music object playing when loading scene

        if (sceneName == "Game")
        {
            Destroy(this.gameObject);
        }
    }
}
