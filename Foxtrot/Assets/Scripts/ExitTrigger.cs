using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitTrigger : MonoBehaviour
{
    
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Player has reached the exit");
        }
    }   

    // void Update()
    // {
    //     Debug.Log("ExitTrigger is running");
    // }

}
