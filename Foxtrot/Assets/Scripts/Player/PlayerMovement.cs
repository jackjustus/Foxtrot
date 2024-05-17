using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;

    public float characterSpeed = 40f;

    float horizontalMove = 0f;

    

    bool jump = false;


   


    // Runs once per frame
    void Update()
    {

        horizontalMove = Input.GetAxisRaw("Horizontal") * characterSpeed;

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }

    }

    // Runs a fixed number of times per second
    void FixedUpdate()
    {
        // Move our character
        // controller.Move(speed, crouch, jump)
        controller.Move(horizontalMove * Time.fixedDeltaTime, jump);

        jump = false;
        

    }


}