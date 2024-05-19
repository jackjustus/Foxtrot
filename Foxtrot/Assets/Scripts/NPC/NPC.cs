using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
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
    [SerializeField] private int dialogueIndex = 0;
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

        // Dialogue Logic
        switch (isDialogueActive)
        {
            case true:
                // Dialogue is active
                if (Input.GetButtonDown("Interact"))
                {                  // If the player presses the interact button
                    Debug.Log("Next Dialogue Requested");

                    currentDialogue = GetNextDialogue();                // Get the next peice of dialogue

                    if (currentDialogue == "##END DIALOGUE")            // Check if the dialogue has ended
                        EndDialogue();
                    else
                        UIController.SetDialogueText(currentDialogue);  // Set the dialogue text
                }
                break;
            case false:
                // Dialogue is not active
                if (isDialogueAllowed && Input.GetButtonDown("Interact"))

                    StartDialogue();
                else if (isDialogueAllowed)
                {
                }

                break;
        }
    }



    #region Dialogue
    private void StartDialogue()
    {
        // This sets the UIController
        UnityEngine.Debug.Log("Dialogue Started w/ Index " + dialogueIndex);

        // Gets the first line of dialogue
        GetNextDialogue();
        UIController.SetDialogueText(currentDialogue);

        // Display the dialogue box & Hide the interaction prompt
        UIController.ShowDialogueBox(true);

        // Update state variables
        isDialogueActive = true;

        // Trigger Event
        dialogueStartEvent.Invoke();
    }

    private void EndDialogue()
    {

        UnityEngine.Debug.Log("Dialogue Ended");

        // Hiding the dialogue box
        UIController.ShowDialogueBox(false);
        UIController.SetDialogueText("Dialogue Ended");

        // Reset Index
        dialogueIndex = 0;

        // Update state variables
        isDialogueActive = false;

        // Trigger Event
        dialogueEndEvent.Invoke();
    }

    private string GetNextDialogue()
    {
        // This gets the next entry from the dialogue list




        // Fetching & Returning the dialogue
        if (dialogueIndex < dialogue.GetDialogueLength())
            currentDialogue = dialogue.GetDialogue(dialogueIndex);
        else
            currentDialogue =  "##END DIALOGUE";
        

        // Incrementing the dialogue index
        if (currentDialogue == null)
            dialogueIndex = 0;
        else
            dialogueIndex++;

        
        return currentDialogue;

    }
    #endregion



    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isDialogueAllowed = true;
            UIController.ShowDialoguePrompt(true);   // Show the interaction prompt
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isDialogueAllowed = false;
            UIController.ShowDialoguePrompt(false);   // Hide the interaction prompt
        }

        // End the dialogue if it is active
        if (isDialogueActive)
            EndDialogue();
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
