using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
    
    [SerializeField] private float jumpForce = 18f; // The force applied when the player jumps
    [SerializeField] private float fallSpeed = 4f; // The maximum time the player can hold the jump button to jump higher


    private bool isJumping = false;


    [Range(0, .3f)][SerializeField] private float m_MovementSmoothing = .05f;   // How much to smooth out the movement
    [SerializeField] private bool m_AirControl = false;                         // Whether or not a player can steer while jumping;

    [Header("Ground Check")]
    [Space]
    [SerializeField] private LayerMask m_WhatIsGround;                          // A mask determining what is ground to the character

    [SerializeField] public Vector2 boxSize;
    [SerializeField] public float castDistance;

    private bool m_Grounded;                                                    // Whether or not the player is grounded.
    private Rigidbody2D m_Rigidbody2D;
    private bool m_FacingRight = true;                                          // For determining which way the player is currently facing.
    private Vector3 m_Velocity = Vector3.zero;


    //provides a small window of time where the player can jump after falling off a platform
    private float coyoteTime = 0.1f;
    private float coyoteTimer; 

    bool movingRight = false;
    bool movingForward = false;



    
   
    


    [Header("Events")]
    [Space]

    public UnityEvent OnLandEvent;

    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> { }

    public BoolEvent OnCrouchEvent;

    private void Awake()
    {

        m_Rigidbody2D = GetComponent<Rigidbody2D>();

        if (OnLandEvent == null)
            OnLandEvent = new UnityEvent();

        if (OnCrouchEvent == null)
            OnCrouchEvent = new BoolEvent();
    }

    private void FixedUpdate()
    {
        bool wasGrounded = m_Grounded;
        m_Grounded = false;



        if (isGrounded())
        {
            coyoteTimer = coyoteTime;
        }
        else if(m_Rigidbody2D.velocity.y >0)
        {
            coyoteTimer = 0;
        }
        else
        {
            coyoteTimer -= Time.fixedDeltaTime;
        }



        if (!Input.GetButton("Jump") && !isJumping && m_Rigidbody2D.velocity.y>0)
        {
            
            m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, m_Rigidbody2D.velocity.y - fallSpeed);
            
            
        }



    }

    private void Update()
    {

        movingRight = Input.GetAxisRaw("Horizontal") == 1;
        movingForward = (m_FacingRight && movingRight) || (!m_FacingRight && !movingRight);


        if (Input.GetButtonDown("Jump") && (isGrounded() || coyoteTimer > 0))
        {
            isJumping = true;
            m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, jumpForce);
            if (coyoteTimer >= 0 && coyoteTimer < coyoteTime)
            {
                UnityEngine.Debug.Log("Coyote Time: " + coyoteTimer);
            }
        }

        if (Input.GetButtonUp("Jump"))
        {
            isJumping = false;
        }


        
            






    }



    public void Move(float move, bool jump)
    {

        //only control the player if grounded or airControl is turned on
        if (m_Grounded || m_AirControl)
        {
            Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);

            // Move the character by finding the target velocity
            //if(!isGrounded() && !movingForward)
            {
                 //targetVelocity = new Vector2(move * 5f, m_Rigidbody2D.velocity.y);
            }
            
            



            // And then smoothing it out and applying it to the character
            //m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);


            if (isGrounded() || movingForward)
            {
                m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

                
                // If the input is moving the player right and the player is facing left...
                if (move > 0 && !m_FacingRight)
                {
                    // ... flip the player.
                    Flip();
                }
                // Otherwise if the input is moving the player left and the player is facing right...
                else if (move < 0 && m_FacingRight)
                {
                    // ... flip the player.
                    Flip();
                }
            }
            else if(!isGrounded() && !movingForward)
            {
                m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing+ 0.1f);
            }
            
            


        }
        // If the player should jump...
        
    }

    public bool isGrounded()
    {
        if (Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, castDistance, m_WhatIsGround))
        {
            return true;
        }
        else
        {
            return false;
        }

    }


    private void Flip()
    {
        
        
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
        
        
    }

    // This visualizes the ground check rectangle in the editor
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position - transform.up * castDistance, boxSize);
    }

    //write a get function to get the direction the player is facing
    public bool getFacingRight()
    {
        return m_FacingRight;
    }

    

    

    
}
