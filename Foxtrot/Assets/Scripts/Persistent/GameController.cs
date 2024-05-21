using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Events;

public class GameController : MonoBehaviour
{

    public static string currentScene {get; private set;} 

    List<AsyncOperation> scenesToLoad = new List<AsyncOperation>();
    
    UIController UIController;
    GameObject player;
    Event LevelLoadedEvent;

    void Awake()
    {
        UIController = FindObjectOfType<UIController>();
        player = GameObject.FindGameObjectWithTag("Player");
        currentScene = SceneManager.GetActiveScene().name;



        HideObjectsWithHiddenTag();
    }
    public void LoadNextLevel(string sceneName)
    {
        // Blackout the screen
        UIController.BlackoutScreen(true);
        

        // If the target scene is not open, load it
        LoadSceneAdditive(sceneName);
        UnloadScene(currentScene);

        
        // scenesToLoad.Add(SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive));
        // scenesToLoad.Add(SceneManager.UnloadSceneAsync(sceneName));

        currentScene = sceneName;

        // Update Hidden Objects in new level
        HideObjectsWithHiddenTag();

        // Unblackout
        UIController.BlackoutScreen(false);
    }

    public void loadSceneAdditive(string sceneName) {
        Debug.Log("[GMCNTRL] Loading scene: " + sceneName);
        scenesToLoad.Add(SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive));
    }




    private void LoadSceneAdditive(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
    }

    private void UnloadScene(string sceneName)
    {
        SceneManager.UnloadSceneAsync(sceneName);
    }


    public void exitGame()
    {
        Debug.print("Exiting game...");
        Application.Quit();
    }





    public void HideObjectsWithHiddenTag() {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Hidden");
        foreach(GameObject obj in objects) {
            obj.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
