using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public int buttonCounter {get; private set;}

    public void incrementButtonCounter() {
        buttonCounter++;
    }

    public void startGame() {


    }

    public void exitGame() {
        Debug.print("Exiting game...");
        Application.Quit();
    }
    
}
