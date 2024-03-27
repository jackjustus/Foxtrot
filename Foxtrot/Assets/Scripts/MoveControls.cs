using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveControls : MonoBehaviour
{

    // Speed of the player
    [SerializeField] public float speed = 10.0f;

    private bool isGrounded = false;


    // Init vars
    Vector3 velocity = Vector3.zero;


    // Update is called once per frame
    void Update()
    {


        Vector3 moveDirection = Vector3.zero;

        // Get the movement direction & set the move direction unit vector
        if (Input.GetKey(KeyCode.A))
        {
            moveDirection += new Vector3(-1, 0, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveDirection += new Vector3(1, 0, 0);
        }

        updateHorizontalMovement();

        updateVerticalMovement();

        // Normalize the move direction
        moveDirection.Normalize();

        // Set the velocity of the player
        velocity = moveDirection * speed * Time.deltaTime;

        // Move the player by the velocity
        transform.position += velocity;
        
    }

    void updateHorizontalMovement() {

        Vector3 moveDirection = Vector3.zero;

        // Get the movement direction & set the move direction unit vector
        if (Input.GetKey(KeyCode.A))
        {
            moveDirection += new Vector3(-1, 0, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveDirection += new Vector3(1, 0, 0);
        }
        
    }

    void updateVerticalMovement() {

        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            velocity.y = 5;
            isGrounded = false;
        }

    }
}
