using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Transform startPosition;
    private GameObject player;
    private GameController gameController;
    private string sceneName;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        sceneName = gameObject.scene.name;
        MovePlayerToStartPosition();

        gameController.HideObjectsWithHiddenTag();
    }

    public void LoadLevel(string levelName)
    {
        Debug.Log("[LVLMNGR] Loading next level: " + levelName);
        gameController.LoadNextLevel(levelName); // Load the next level
    }


    private void MovePlayerToStartPosition()
    {
        if (startPosition != null)
        {
            // Move the player to the target position
            player.transform.position = startPosition.position;
        }
        else
        {
            Debug.LogError(sceneName + " start position is not assigned!");
        }
    }

}

