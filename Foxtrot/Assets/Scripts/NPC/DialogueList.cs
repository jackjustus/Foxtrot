using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueList : MonoBehaviour
{
    public string dialogueName;
    public string dialoguePurpose;
    public string[] dialogue;

    public string GetDialogueString(int index)
    {
        return dialogue[index];
    }

    public int GetDialogueLength()
    {
        return dialogue.Length;
    }
}
