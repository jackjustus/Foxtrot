using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    // This is the parent class used for all the bosses in the game
    // The children of this class will inherit the properties and methods of this class


    // public void Damage(int amount)
    // {

    //     if (amount < 0)
    //     {
    //         throw new System.ArgumentOutOfRangeException("Cannot have negative Damage");
    //     }
    //     this.health -= amount;

    //     if (health <= 0)
    //     {
    //         Die();
    //     }


    // }
    private void Awake() {
        // if (OnDeathEvent == null)
        //     OnDeathEvent = new UnityEvent();
    }
    private void Die()
    {
        // OnDeathEvent.Invoke();


        UnityEngine.Debug.Log("I am Dead");
        Destroy(gameObject);
    }
}
