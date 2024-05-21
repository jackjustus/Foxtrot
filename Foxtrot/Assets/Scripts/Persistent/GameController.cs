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
    public UnityEvent LevelFinishedLoading;

    void Awake()
    {
        // Intialize helper objects
        UIController = FindObjectOfType<UIController>();

        // Intialize events
        LevelFinishedLoading = new UnityEvent();


        // Get the current scene for the starting position
        currentScene = SceneManager.GetActiveScene();

        // Set up the starting scene
        AfterSceneLoad(currentScene.name);
    }

    #region Level Loading
    public void LoadNextLevel(string sceneToLoad)
    {
        // Blackout the screen
        UIController.BlackoutScreen(true);
        

        // Loading the new scene and unloading the old one in the background
        // After this loading is done, AfterSceneLoad() is called.
        StartCoroutine(LoadNewScenes(currentScene, sceneToLoad));
    }

    private IEnumerator LoadNewScenes(Scene currentScene, string sceneToLoad) {

        // The Application loads the Scene in the background as the blackout screen fades in.


        // These lines start the loading & unloading operations.
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Additive);
        AsyncOperation asyncUnload = SceneManager.UnloadSceneAsync(currentScene);

        // This will continue to loop until the scenes are done loading/unloading
        while (!asyncLoad.isDone || !asyncUnload.isDone)
        {
            yield return null;
        }

        // Print the scene name & inform the user that the scene has finished loading
        Debug.Log(sceneToLoad + " has finished loading!");

        // This sets up the new scene
        AfterSceneLoad(sceneToLoad);
    }

    private void AfterSceneLoad(string loadedSceneName) {

        // The current scene is now the one that was loaded
        currentScene = SceneManager.GetSceneByName(loadedSceneName);

        // Set the new scene as the active scene
        SceneManager.SetActiveScene(currentScene);

        // Update Hidden Objects in new level
        HideObjectsWithHiddenTag();

        // Trigger the levelFinishedLoading Event
        LevelFinishedLoading.Invoke();

        // Unblackout
        UIController.BlackoutScreen(false);
    }
    #endregion

    #region Misc
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
    #endregion