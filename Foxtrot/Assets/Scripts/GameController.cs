using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    // public int buttonCounter {get; private set;}

    public string currentScene {get; private set;} 


    List<AsyncOperation> scenesToLoad = new List<AsyncOperation>();


    public void loadScene(string sceneName) {
        scenesToLoad.Add(SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive));
    }

    public void unloadScene(string sceneName)
    {
        AsyncOperation sceneToUnload = SceneManager.UnloadSceneAsync(sceneName);
        scenesToLoad.Remove(sceneToUnload);
    }

    public void exitGame()
    {
        Debug.print("Exiting game...");
        Application.Quit();
    }
}
