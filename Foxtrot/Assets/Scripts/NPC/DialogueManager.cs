using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    // This script will handle displaying the text, advancing the dialogue, and managing any choices the player can make.
    
    UIController UIController;

    public string currentDialogue { get; private set; }

    public void Start()
    {
        UIController = GameObject.FindGameObjectWithTag("UIController").GetComponent<UIController>();
    }

    public void StartDialogue() {
        Debug.Log("Starting dialogue");

        // Displaying the UI Elements
        UIController.ShowDialogueBox(true);
        UIController.ShowDialoguePrompt(false);

        // Displaying the first peice of dialogue
    }

    // Sending the dialogue to the UIController

}
