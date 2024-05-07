using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public string currentScene {get; private set;} 

    List<AsyncOperation> scenesToLoad = new List<AsyncOperation>();
    
    UIController UIController;
    GameObject player;

    void Awake()
    {
        UIController = FindObjectOfType<UIController>();
        player = GameObject.FindGameObjectWithTag("Player");
    }
    public void LoadNextLevel(string sceneName)
    {
        // Blackout the screen
        UIController.BlackoutScreen(true);
        

        // If the target scene is not open, load it
        scenesToLoad.Add(SceneManager.LoadSceneAsync(sceneName));
        currentScene = sceneName;

        // Unblackout
        UIController.BlackoutScreen(false);
    }

    public void loadSceneAdditive(string sceneName) {
        Debug.Log("[GMCNTRL] Loading scene: " + sceneName);
        scenesToLoad.Add(SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive));
    }

    public void exitGame()
    {
        Debug.print("Exiting game...");
        Application.Quit();
    }
}
