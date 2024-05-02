using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public string currentScene {get; private set;} 

    List<AsyncOperation> scenesToLoad = new List<AsyncOperation>();

    void Awake() {
        DontDestroyOnLoad(this.gameObject);
    }

    public void standardLoadNextLevel(string sceneName)
    {
        // If the target scene is not open, load it
        scenesToLoad.Add(SceneManager.LoadSceneAsync(sceneName));
    }

    public void loadScene(string sceneName) {
        Debug.Log("[GMCNTRL] Loading scene: " + sceneName);
        scenesToLoad.Add(SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive));
    }

    public void unloadScene(string sceneName)
    {
        // AsyncOperation sceneToUnload = SceneManager.UnloadSceneAsync(sceneName);
        // scenesToLoad.Remove(sceneToUnload);
        SceneManager.UnloadSceneAsync(sceneName);
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
