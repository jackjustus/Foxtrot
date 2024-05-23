using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHurtbox : MonoBehaviour
{
    [SerializeField] private int damage = 1;

    [SerializeField] private bool isBoss = false;
    [SerializeField] private Boss boss;

    private void OnTriggerStay2D(Collider2D collider)
    {


        if (isBoss)
        {
            boss.DealContactDamage(collider);
        }
        else
        {
            if (collider.GetComponent<PlayerHealth>() != null)
            {
                PlayerHealth health = collider.GetComponent<PlayerHealth>();
                health.Damage(damage);


            }
        }
    }
}
