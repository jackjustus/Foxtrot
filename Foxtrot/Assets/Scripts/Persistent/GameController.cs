using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public string currentScene {get; private set;} 


    List<AsyncOperation> scenesToLoad = new List<AsyncOperation>();

    public void standardLoadNextLevel(string sceneName)
    {
        List<Scene> openScenes = getScenes(); // Getting a list of all open scenes
        for (int i = 0; i < openScenes.Count; i++)
        {
            // If the target scene is already open, log an error and return
            if (openScenes[i].name == sceneName)
            {
                Debug.LogError("Scene already loaded");
                return;
            } else if (openScenes[i].name != "Gameplay" || openScenes[i].name != "GameController") // If the scene is not the Gameplay or GameController scene, unload it
            {
                Debug.Log("Unloading scene: " + openScenes[i].name);
                unloadScene(openScenes[i].name);
            }
        }
        // If the target scene is not open, load it
        scenesToLoad.Add(SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive));
    }

    public void loadScene(string sceneName) {
        Debug.Log("[GMCNTRL] Loading scene: " + sceneName);
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

    private List<Scene> getScenes() {
        List<Scene> openScenes = new List<Scene>();
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            Scene scene = SceneManager.GetSceneAt(i);
            openScenes.Add(scene);
        }
        return openScenes;
    }
}
