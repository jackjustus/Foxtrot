using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject blackScreen;
    [SerializeField] private GameObject dialoguePrompter;
    [SerializeField] private GameObject dialogueText;

    void Awake() {
        blackScreen.SetActive(false);
        dialoguePrompter.SetActive(false);
    }

    public void BlackoutScreen(bool fadeOut) {
        if (fadeOut) {
            Debug.Log("[UICTRL] Fading out screen");
            blackScreen.SetActive(true);
    } else
            blackScreen.SetActive(false);
    }

    public void StartDialogue() {
        Debug.Log("Starting dialogue");

        // Displaying the UI Elements
        ShowDialogueBox(true);
        ShowDialoguePrompt(false);
    }

    public void ShowDialoguePrompt(bool show) {
        if (show) {
            Debug.Log("[UICTRL] Showing dialogue prompt");
            dialoguePrompter.SetActive(true);
        } else {
            dialoguePrompter.SetActive(false);
        }
    }
    public void ShowDialogueBox(bool show) {
        if (show) {
            Debug.Log("[UICTRL] Showing dialogue box");
            dialogueText.SetActive(true);
        } else {
            dialogueText.SetActive(false);
        }
    }
    public void SetDialogueText(string text) {
        dialogueText.GetComponent<TextMesh>().text = text;
    }

}
