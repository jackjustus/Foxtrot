using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    private GameObject forwardAttackArea = default;

    private bool attacking = false;

    private float timeToAttack = .25f;
    private float timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        forwardAttackArea = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        

        if (Input.GetButtonDown("Attack"))
        {
            Attack();
        }

        if (attacking)
        {
            timer+=Time.deltaTime;

            if(timer>=timeToAttack)
            {
                timer = 0;
                attacking = false;
                forwardAttackArea.SetActive(attacking);
            }
        }
    }
    private void Attack()
    {
        
        attacking = true;
        forwardAttackArea.SetActive(attacking);
    }
}
