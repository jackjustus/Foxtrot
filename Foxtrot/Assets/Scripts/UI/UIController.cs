using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.Playables;

public class UIController : MonoBehaviour
{

    [Header("UI Elements")]
    [SerializeField] private GameObject blackScreen;
    [SerializeField] private GameObject dialoguePrompter;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private GameObject dialogueTextBox;


    [Header("Animation Elements (Dialogue Prompter)")]
    [SerializeField] private GameObject dialoguePromptAnimationGameObj;
    private PlayableDirector dialoguePromptPlayableDirector;
    private bool dialoguePromptPlayInReverse = false;

    [Header("Animation Elements (Dialogue Text Box)")]
    [SerializeField] private GameObject dialogueTextAnimationGameObj;
    private PlayableDirector dialogueTextPlayableDirector;

    [Header("Animation Elements (Blackout Screen)")]
    [SerializeField] private PlayableDirector blackoutFadeIn;
    [SerializeField] private PlayableDirector blackoutFadeOut;

    // private bool dialogueTextPlayInReverse = false;


    void Awake()
    {
        blackScreen.SetActive(false);
        dialoguePrompter.SetActive(false);
        dialogueTextBox.SetActive(false);


        dialogueText = dialogueTextBox.GetComponentInChildren<TextMeshProUGUI>();

        dialoguePromptPlayableDirector = dialoguePromptAnimationGameObj.GetComponent<PlayableDirector>();
        dialogueTextPlayableDirector = dialoguePromptAnimationGameObj.GetComponent<PlayableDirector>();
    }

    void Update()
    {
        updateReverseAnimation(ref dialoguePromptPlayInReverse, dialoguePromptPlayableDirector);
        // updateReverseAnimation(ref dialogueTextPlayInReverse, dialogueTextPlayableDirector);
    }


    public void BlackoutScreen(bool fadeOut)
    {
        if (fadeOut)
        {
            // Stopping and rewinding the fade out animation
            blackoutFadeOut.Stop();
            blackoutFadeOut.time = 0;

            // Starting the fade in animation
            blackoutFadeIn.Play();
        }
        else
        {
            // Stopping and rewinding the fade in animation
            blackoutFadeIn.Stop();
            blackoutFadeIn.time = 0;

            // Starting the fade out animation
            blackoutFadeOut.Play();
        }
    }



    #region Dialogue
    public void ShowDialoguePrompt(bool show)
    {
        if (show)
        {
            Debug.Log("[UICTRL] Showing dialogue prompt");

            dialoguePrompter.SetActive(true);
            dialoguePromptPlayableDirector.Play();
            dialoguePromptPlayInReverse = false;
        }
        else
        {
            Debug.Log("[UICTRL] Hiding dialogue prompt");
            dialoguePromptPlayInReverse = true;
            // dialoguePrompter.SetActive(false);
        }
    }
    public void ShowDialogueBox(bool show)
    {
        if (show)
        {
            Debug.Log("[UICTRL] Showing dialogue box");
            dialogueTextBox.SetActive(true);

            dialogueTextPlayableDirector.Play();
            // dialogueTextPlayInReverse = false;
        }
        else
        {
            // dialogueTextPlayInReverse = true;
        }
    }
    public void SetDialogueText(string text)
    {
        dialogueText.text = text;
        if (dialogueText == null)
        {
            Debug.LogError("[UICTRL] Dialogue text is null!");
        }
    }
    #endregion

    private void updateReverseAnimation(ref bool playInReverse, PlayableDirector playableDirector)
    {
        // This enables the dialogue prompt animation to play in reverse
        if (playInReverse)
        {
            // Pausing the animation (it will manually be reversed)
            playableDirector.Pause();


            // Checking to see if the animation ended
            if (playableDirector.time <= 0)
            {
                playableDirector.Pause();
                playInReverse = false;
                playableDirector.time = 0;
                return;
            }

            // Reversing the animation
            playableDirector.time -= Time.deltaTime;


            // Evaluating the animation
            playableDirector.Evaluate();
        }
    }

}
