using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackwardAttackArea : MonoBehaviour
{
    private int damage = 5;

    private void OnTriggerEnter2D(Collider2D collider)
    {



        if (collider.GetComponent<Health>() != null)
        {
            Health health = collider.GetComponent<Health>();
            health.Damage(damage);


        }
    }
}
