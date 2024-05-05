using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
    // This script is a modified version of the CharacterController2D script from the Unity 2D Platformer tutorial
    // It controlls the player's movement, jumping, and ground check by interacting with the Rigidbody2D component
    // It also triggers events when the player lands


    [Header("Jumping")]     // Jump 
    [Space]


    [SerializeField] private float jumpForce = 18f;                             // The force applied when the player jumps
    [SerializeField] private float fallSpeed = 4f;                              // The downward force applied when the player lets go of the jump button
    [SerializeField] private float coyoteBuffer = 0.1f;                         // Length of time where the player can jump after falling off a platform
    private bool isJumping = false;
    private float coyoteTimer;



    [Header("Movement")]    // Movement 
    [Space]


    [Range(0, .3f)][SerializeField] private float m_MovementSmoothing = .05f;   // How much to smooth out the movement
    [SerializeField] private bool AirControlEnabled = false;                    // Whether or not a player can steer while jumping;
    private bool isFacingRight = true;                                          // For determining which way the player is currently facing.
    private bool isAcceleratingRight = false;
    private bool isSpeedingUpX = false;


    [Header("Ground Check")]    // Ground Check
    [Space]


    [SerializeField] private LayerMask whatIsGround;                             // A mask determining what layers are ground to the character
    [SerializeField] private Vector2 boxSize;                                    // The width & height of the GC box 
    [SerializeField] private float castDistance;                                 // The distance the GC box is cast from the center of the player
    public bool isGrounded;                                                      // Whether or not the player is grounded.
    private bool wasGrounded;                                                    // Whether or not the player was grounded in the previous frame.


    [Header("Events")]          // Events
    [Space]


    public UnityEvent OnLandEvent;                                              // Event triggered when the player lands after falling


    // General Init Variables - These are initialized in the Awake function

    private Rigidbody2D Rigidbody2D;
    private Vector3 velocity = Vector3.zero;


    private void Awake()
    {
        // Initialize the Rigidbody2D component
        Rigidbody2D = GetComponent<Rigidbody2D>();

        // Initialize the OnLandEvent
        if (OnLandEvent == null)
            OnLandEvent = new UnityEvent();
    }


    private void FixedUpdate()
    {
        // Updating Grounded Status
        wasGrounded = isGrounded;
        UpdateGroundedStatus();

        UpdateCoyoteTimer();

        // After the player has let go of the jump button, increase downwards force by fallSpeed
        if (!Input.GetButton("Jump") && !isJumping && Rigidbody2D.velocity.y > 0)
            Rigidbody2D.velocity = new Vector2(Rigidbody2D.velocity.x, Rigidbody2D.velocity.y - fallSpeed);

    }


    private void Update()
    {
        // Updating movement booleans
        isAcceleratingRight = Input.GetAxisRaw("Horizontal") == 1;
        isSpeedingUpX = (isFacingRight && isAcceleratingRight) || (!isFacingRight && !isAcceleratingRight);




        // The player is no longer jumping if the jump button is released or the player is grounded
        if (Input.GetButtonUp("Jump") || isGrounded)
            isJumping = false;

    }


    public void Move(float move, bool jump)
    {
        // This method is called by PlayerMovement.cs

        // The target velocity is the player's current velocity with the new movement input
        Vector3 targetVelocity = new Vector2(move * 10f, Rigidbody2D.velocity.y);


        // The player is able to jump if coyoteTimer is greater than 0
        if (jump && coyoteTimer > 0)
        {
            isJumping = true;
            Rigidbody2D.velocity = new Vector2(Rigidbody2D.velocity.x, jumpForce);

            // Uncomment to see the coyote timer in the console
            if (coyoteTimer >= 0 && coyoteTimer < coyoteBuffer)
            {
                Debug.Log("Coyote Time: " + coyoteTimer);
            }
            
        }

        // Smoothing out the target velocity and applying it to the player
        Rigidbody2D.velocity = Vector3.SmoothDamp(Rigidbody2D.velocity, targetVelocity, ref velocity, m_MovementSmoothing);


        // The player can only move if grounded or airControl is turned on
        // The second part of the or logic is very iffy
        if (isGrounded || AirControlEnabled)
        {

            
            


            // If the input is moving the player right and the player is facing left...
            if (move > 0 && !isFacingRight)
            {
                // ... flip the player.
                Flip();
            }
            // Otherwise if the input is moving the player left and the player is facing right...
            else if (move < 0 && isFacingRight)
            {
                // ... flip the player.
                Flip();
            }
        }
        
        
    }


    private void UpdateGroundedStatus()
    {
        // The player is grounded if a linecast to the groundcheck position hits anything on the ground layer.
        // See OnDrawGizmosSelected() for the BoxCast visual representation
        if (Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, castDistance, whatIsGround))
            isGrounded = true;
        else
            isGrounded = false;
    }


    private void UpdateCoyoteTimer()
    {
        // The Coyote Timer is set to the coyoteBuffer when the player is grounded
        // When the player is not grounded, the Coyote Timer is decremented by Time.fixedDeltaTime
        // The player can jump as long as the Coyote Timer is greater than 0

        if (isGrounded)
            // If the player is grounded, update the coyote timer
            coyoteTimer = coyoteBuffer;
        else if (Rigidbody2D.velocity.y > 0)
            // If the player is not grounded and is moving upwards, set the coyote timer to 0
            coyoteTimer = 0;
        else
            // If the player is not grounded and is moving downwards, decrement the coyote timer
            coyoteTimer -= Time.fixedDeltaTime;
    }


    private void Flip()
    {

        // Switch the way the player is labelled as facing.
        isFacingRight = !isFacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }


    public bool getFacingRight()
    {
        return isFacingRight;
    }


    private void OnDrawGizmosSelected()
    {
        // This visualizes the ground check rectangle in the editor when the object is selected
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position - transform.up * castDistance, boxSize);
    }


}
