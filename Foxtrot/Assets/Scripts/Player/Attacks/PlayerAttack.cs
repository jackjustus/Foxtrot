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



    private GameObject player;
    private PlayerMovement1 bool_script;
   
   
        
    





    // Start is called before the first frame update
    void Start()
    {
        forwardAttackArea = transform.GetChild(0).gameObject;
        upAttackArea = transform.GetChild(1).gameObject; 
        backAttackArea=transform.GetChild(2).gameObject;

        player = GameObject.FindGameObjectWithTag("Player");

        bool_script = player.GetComponent<PlayerMovement1>();
    }

    // Update is called once per frame
    void Update()
    {

        
        bool facingRight = bool_script.IsFacingRight;
        bool movingRight = Input.GetAxisRaw("Horizontal") == 1;
        

        bool movingForward = (facingRight && movingRight) || (!facingRight && !movingRight);
        bool movingBackward = (facingRight && !movingRight) || (!facingRight && movingRight);
        //write an if statment that runs if the player is grounded using the getGrounded or isGrounded function from the CharacterController2D script
        
        
       

        if( ((Input.GetButtonDown("RightAttack") && facingRight) || (Input.GetButtonDown("LeftAttack") && !facingRight)) && !attacking)
        {
            ForwardAttack();
        }

        if( ((Input.GetButtonDown("RightAttack") && !facingRight) || (Input.GetButtonDown("LeftAttack") && facingRight)) && !attacking)
        {
            BackwardAttack();
        }

        if(Input.GetButtonDown("UpAttack") && !attacking)
        {
            UpAttack();
        }

        if(Input.GetButtonDown("DownAttack") && !attacking)
        {
            //DownAttack();
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
