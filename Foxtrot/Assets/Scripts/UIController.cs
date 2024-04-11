using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI buttonCounterText;

    GameController gameController;

    private void Start()
    {
        // Get the master gameController
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        
    }

    public void updateScore() {
        buttonCounterText.text = gameController.buttonCounter.ToString();
    }
}
