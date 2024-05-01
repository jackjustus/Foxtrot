using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject menu;
    public GameObject loadingInterface;
    public Image loadingProgressBar;

    List<AsyncOperation> scenesToLoad = new List<AsyncOperation>();

    public void StartGame()
    {
        HideMenu();
        ShowLoadingScreen();
        
        scenesToLoad.Add(SceneManager.LoadSceneAsync("Gameplay"));
        scenesToLoad.Add(SceneManager.LoadSceneAsync("Stage0", LoadSceneMode.Additive));

        // Coroutine is used to load the scenes in the background
        StartCoroutine(LoadingScreen());
    }

    public void HideMenu() {
        menu.SetActive(false);
    }

    public void ShowLoadingScreen() {
        loadingInterface.SetActive(true);
    }


    // This coroutine method, LoadingScreen, is responsible for loading multiple scenes asynchronously in Unity.
    // It iterates over a list of scenes to load, each represented by an AsyncOperation object.
    // For each scene, it waits until the scene is fully loaded, during which it calculates and updates a progress bar.
    // The progress bar's fill amount is determined by the average progress of all scenes being loaded.
    // This method allows for the game to remain responsive as scenes load in the background.
    IEnumerator LoadingScreen() {
        float totalProgress = 0;
        for (int i = 0; i < scenesToLoad.Count; ++i) {
            while (!scenesToLoad[i].isDone) {
                totalProgress += scenesToLoad[i].progress;
                loadingProgressBar.fillAmount = totalProgress / scenesToLoad.Count;
                yield return null;
            }
        }
    }

}
