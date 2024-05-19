using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject blackScreen;
    [SerializeField] private GameObject dialoguePrompter;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private GameObject dialogueTextBox;

    void Awake() {
        blackScreen.SetActive(false);
        dialoguePrompter.SetActive(false);
        dialogueTextBox.SetActive(false);


        dialogueText = dialogueTextBox.GetComponentInChildren<TextMeshProUGUI>();
    }

    public void BlackoutScreen(bool fadeOut) {
        if (fadeOut) {
            Debug.Log("[UICTRL] Fading out screen");
            blackScreen.SetActive(true);
    } else
            blackScreen.SetActive(false);
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
            dialogueTextBox.SetActive(true);
        } else {
            dialogueTextBox.SetActive(false);
        }
    }
    public void SetDialogueText(string text) {
        dialogueText.text = text;
        if (dialogueText == null) {
            Debug.LogError("[UICTRL] Dialogue text is null!");
        }
    }

}
