using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Events;

public class GameController : MonoBehaviour
{

    public static Scene currentScene {get; private set;} 

    List<AsyncOperation> scenesToLoad = new List<AsyncOperation>();
    
    UIController UIController;
    GameObject player;
    Event LevelLoadedEvent;

    void Awake()
    {
        UIController = FindObjectOfType<UIController>();
        player = GameObject.FindGameObjectWithTag("Player");
        currentScene = SceneManager.GetActiveScene();



        HideObjectsWithHiddenTag();
    }
    public void LoadNextLevel(string sceneToLoad)
    {
        // Blackout the screen
        UIController.BlackoutScreen(true);
        

        // Loading the new scene and unloading the old one in the background
        // After this loading is done, AfterSceneLoad() is called.
        StartCoroutine(LoadNewScenes(currentScene, sceneToLoad));


        // scenesToLoad.Add(SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Additive));
        // scenesToLoad.Add(SceneManager.UnloadSceneAsync(currentScene));
    }


    private IEnumerator LoadNewScenes(Scene currentScene, string sceneToLoad) {

        // The Application loads the Scene in the background as the blackout screen fades in.

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Additive);
        AsyncOperation asyncUnload = SceneManager.UnloadSceneAsync(currentScene);

        while (!asyncLoad.isDone || !asyncUnload.isDone)
        {
            yield return null;
        }

        Debug.Log("System has loaded the new scene and unloaded the old one.");

        AfterSceneLoad(sceneToLoad);
    }

    private void AfterSceneLoad(string loadedSceneName) {

        // The current scene is now the one that was loaded
        currentScene = SceneManager.GetSceneByName(loadedSceneName);

        // Set the new scene as the active scene
        SceneManager.SetActiveScene(currentScene);

        // Update Hidden Objects in new level
        HideObjectsWithHiddenTag();

        // Unblackout
        UIController.BlackoutScreen(false);
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
