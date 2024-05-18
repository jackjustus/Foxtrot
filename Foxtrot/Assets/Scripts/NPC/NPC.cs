using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NPC : MonoBehaviour
{
    // NPC Vars
    public string npcName;
    public string npcDialogue;

    // State Vars
    public bool isInteractionAllowed { get; private set;}
    public bool isDialogueActive { get; private set;}      

    // Helper Objects
    UIController UIController;       

    // Events
    public UnityEvent<bool> dialoguePromptEvent;                      // This event is triggered when interactionAllowed changes with the bool as a parameter
    public UnityEvent dialogueStartEvent;                       // This event is triggered when dialogueActive is set to true
    public UnityEvent dialogueEndEvent;                         // This event is triggered when dialogueActive is set to false



    public void Start()
    {
        // Initial state
        isInteractionAllowed = false;
        isDialogueActive = false;

        // Intialize events
        dialoguePromptEvent = new UnityEvent<bool>();
        dialogueStartEvent = new UnityEvent();
        dialogueEndEvent = new UnityEvent();

        // Initilize UIController
        UIController = GameObject.FindGameObjectWithTag("UIController").GetComponent<UIController>();
    }

    public void FixedUpdate()
    {
        if (isInteractionAllowed && !isDialogueActive)
        {
            // Dialogue is avaliable to be started

            if (Input.GetButtonDown("Interact")) // If the player presses the interact button
            {
                // Start Dialogue
                UnityEngine.Debug.Log("Dialogue triggered");
                isDialogueActive = true;
                dialogueStartEvent.Invoke();
            }
        }
    }   


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            UnityEngine.Debug.Log("Dialogue avalible");
            isInteractionAllowed = true;
            dialoguePromptEvent.Invoke(true);   // Show the interaction prompt
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            UnityEngine.Debug.Log("dialogue unavaliable");// Code to run when the player exits the circle collider
            isInteractionAllowed = false;
            dialoguePromptEvent.Invoke(false);   // Show the interaction prompt
        }
    }
}
