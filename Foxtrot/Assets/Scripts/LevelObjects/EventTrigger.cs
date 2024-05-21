using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class EventTrigger : MonoBehaviour
{
    public UnityEvent playerEntersTrigger;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerEntersTrigger.Invoke();
        
    }
}
