using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class ExitTrigger : MonoBehaviour
{
    public UnityEvent<string> playerExitEvent;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player has entered the exit trigger");
            playerExitEvent.Invoke("");
        }
    }
}
