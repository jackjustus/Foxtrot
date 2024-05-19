using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NPC : MonoBehaviour
{
    // NPC Vars
    public string npcName;

    // State Vars
    public bool isDialogueAllowed { get; private set; }
    public bool isDialogueActive { get; private set; }

    // Helper Objects
    UIController UIController;

    // Events
    public UnityEvent<bool> dialoguePromptEvent;                // This event is triggered when interactionAllowed changes with the bool as a parameter
    public UnityEvent dialogueStartEvent;                       // This event is triggered when dialogueActive is set to true
    public UnityEvent dialogueEndEvent;                         // This event is triggered when dialogueActive is set to false

    // Dialogue Vars
    DialogueList dialogue;
    private int dialogueIndex = 0;
    private string currentDialogue;


    public void Start()
    {
        // Initial state
        isDialogueAllowed = false;
        isDialogueActive = false;

        // Intialize events
        InitializeEvents();

        // Initialize Dialogue
        InitializeDialogue();

        // Initilize UIController
        UIController = GameObject.FindGameObjectWithTag("UIController").GetComponent<UIController>();
    }

    public void Update()
    {
        if (isDialogueAllowed && !isDialogueActive)
        {
            // Dialogue is able to be started

            if (Input.GetButtonDown("Interact")) // If the player presses the interact button
                StartDialogue();

        }
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            UnityEngine.Debug.Log("Dialogue avalible");
            isDialogueAllowed = true;
            UIController.ShowDialoguePrompt(true);   // Show the interaction prompt
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            UnityEngine.Debug.Log("dialogue unavaliable");// Code to run when the player exits the circle collider
            isDialogueAllowed = false;
            UIController.ShowDialoguePrompt(false);   // Hide the interaction prompt
        }
    }



    private void StartDialogue()
    {

        UnityEngine.Debug.Log("Dialogue triggered");

        // Display the dialogue box & Hide the interaction prompt
        UIController.ShowDialogueBox(true);
        UIController.SetDialogueText(GetNextDialogue());

        // Update state variables
        isDialogueActive = true;

        // Trigger Event
        dialogueStartEvent.Invoke();
    }

    private void EndDialogue() {

        UnityEngine.Debug.Log("Dialogue Ended");

        // Hiding the dialogue box
        UIController.ShowDialogueBox(false);
        UIController.SetDialogueText("Dialogue Ended");

        // Update state variables
        isDialogueActive = false;

        // Trigger Event
        dialogueEndEvent.Invoke();
    }

    private string GetNextDialogue()
    {
        // Incrementing the dialogue index
        if (currentDialogue == null)
            dialogueIndex = 0;
        else
            dialogueIndex++;

        // Fetching & Returning the dialogue
        if (dialogueIndex < dialogue.GetDialogueLength()) {
            currentDialogue = dialogue.GetDialogueString(dialogueIndex);
            Debug.Log(currentDialogue);

            return currentDialogue;
        } else {
            EndDialogue();
            return "";
        }
        

    }

    


    private void InitializeDialogue()
    {
        // Fetch the dialogue list
        dialogue = GetComponentInChildren<DialogueList>();

        if (dialogue == null)
            Debug.LogError("No dialogue list found on " + gameObject.name + "'s children");
    }
    private void InitializeEvents()
    {
        dialoguePromptEvent = new UnityEvent<bool>();
        dialogueStartEvent = new UnityEvent();
        dialogueEndEvent = new UnityEvent();
    }
}
