using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeaverAttack : MonoBehaviour
{
    private int damage = 2;

    private void OnTriggerStay2D(Collider2D collider)
    {


        if (collider.GetComponent<PlayerHealth>() != null)
        {
            PlayerHealth health = collider.GetComponent<PlayerHealth>();
            health.Damage(damage);


        }
    }
}
