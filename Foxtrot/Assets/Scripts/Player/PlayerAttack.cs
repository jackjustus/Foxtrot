using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    private GameObject forwardAttackArea = default;
    private GameObject upAttackArea = default;
    private GameObject backAttackArea = default;

    private bool attacking = false;

    private float timeToAttack = .25f;
    private float timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        forwardAttackArea = transform.GetChild(0).gameObject;
        upAttackArea = transform.GetChild(1).gameObject; 
        backAttackArea=transform.GetChild(2).gameObject;
    }

    // Update is called once per frame
    void Update()
    {

        
        
        //write an if statment that runs if the player is grounded using the getGrounded or isGrounded function from the CharacterController2D script
        if(GetComponent<CharacterController2D>().isGrounded() == false)
        {
            if( (GetComponent<CharacterController2D>().getFacingRight() == true && Input.GetAxisRaw("Horizontal") == -1) || (GetComponent<CharacterController2D>().getFacingRight()==false && Input.GetAxisRaw("Horizontal")==1))
            {
                if (Input.GetButtonDown("Attack"))
                {
                    BackwardAttack();
                }
            }
            
        }
        
        
            



        if (Input.GetAxisRaw("Vertical") !=0)
        {
            if (Input.GetButtonDown("Attack"))
            {
                UpAttack();
            }
        }
        if(Input.GetAxisRaw("Vertical") == 0)
        {
            if (Input.GetButtonDown("Attack"))
            {
                ForwardAttack();
            }
        }
        









        










        if (attacking)
        {
            timer+=Time.deltaTime;

            if(timer>=timeToAttack)
            {
                timer = 0;
                attacking = false;
                forwardAttackArea.SetActive(attacking);
                upAttackArea.SetActive(attacking);
                backAttackArea.SetActive(attacking);
                
            }
        }
    }
    private void ForwardAttack()
    {
        
        attacking = true;
        forwardAttackArea.SetActive(attacking);
    }

    private void UpAttack()
    {

        attacking = true;
        upAttackArea.SetActive(attacking);
    }

    private void BackwardAttack()
    {
        attacking = true;
        backAttackArea.SetActive(attacking);
    }

}
