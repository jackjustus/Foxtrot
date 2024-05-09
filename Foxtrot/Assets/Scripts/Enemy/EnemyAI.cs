using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.U2D;

public class EnemyAI : MonoBehaviour
{


    Animator animator;
    bool animationIsGrounded = true;

    [Header("Enemy Constants")]
    [Space]

    [SerializeField] private float enemySpeed = 60f;
    [SerializeField] private float detectionRange = 200f;
    [SerializeField] private float maxJumpRange = 50f;
    [SerializeField] private float minJumpRange = 5f;
    private Transform player;
    private Rigidbody2D rb;


    [SerializeField] private float m_JumpForce = 300f;                          // Amount of force added when the player jumps.
    [Range(0, 1f)][SerializeField] private float m_MovementSmoothing = .5f;     // How much to smooth out the movement
    [SerializeField] private LayerMask m_WhatIsGround;                          // A mask determining what is ground to the character
    [SerializeField] private Transform m_GroundCheck;                           // A position marking where to check if the player is grounded.
    [SerializeField] private Transform m_CeilingCheck;                          // A position marking where to check for ceilings


    SpriteRenderer sprite;

    // Jump & ground collision stuff
    const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
    private bool m_Grounded;            // Whether or not the player is grounded.
    private bool m_FacingRight = true;  // For determining which way the player is currently facing.
    private bool m_movingRight;
    private Vector3 m_Velocity = Vector3.zero;
    bool jump = false;

    // Player Targeting
    Vector2 enemyToPlayer;
    Vector2 unitToPlayer;

    [Header("Events")]
    [Space]

    public UnityEvent OnLandEvent;

    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> { }


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();

        animator = GetComponent<Animator>();



        sprite = GetComponent<SpriteRenderer>();

    }


    private void Awake()
    {

        if (OnLandEvent == null)
            OnLandEvent = new UnityEvent();

    }


    // Update is called once per frame
    void Update()
    {


        if(rb.velocity.x >0)
        {
            m_movingRight = true;
        } 
        if(rb.velocity.x < 0)
        {
            m_movingRight = false;
        }
        

        
        animator.SetBool("isJumping", !animationIsGrounded);

        if (player != null)
        {

            Vector3 pos = transform.position;

            // Creating a unit vector from the player to the enemy
            enemyToPlayer = (pos - player.position);
            unitToPlayer = enemyToPlayer.normalized;

            float distanceToPlayer = enemyToPlayer.magnitude;


            // Move towards the player if within detection range
            if (distanceToPlayer <= detectionRange)
            {


                // Perform attack or other actions when within attack range
                if (distanceToPlayer <= maxJumpRange && distanceToPlayer >= minJumpRange)
                {
                    // Perform attack action here
                
                    jump = true;
                }

                Move(enemySpeed * Time.fixedDeltaTime, jump);


            }
            else
            {
                // Enemy is outside detection range
            }
        } else
        {
            Debug.print("BIG ERROR: ENEMY PLAYER DOESNT EXIST");
        }




    }


    private void OnTriggerStay2D(Collider2D collision)
    { 
        
        
        animationIsGrounded = true;
    }
    





    private void FixedUpdate()
    {
        

        animator.SetFloat("xVelocity", Mathf.Abs(rb.velocity.x));
        animator.SetFloat("yVelocity", rb.velocity.y);

        bool wasGrounded = m_Grounded;
        m_Grounded = false;

        // The enemy is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            // gameObject refers to the player. 
            if (colliders[i].gameObject != gameObject)
            {
                m_Grounded = true;
                if (!wasGrounded)
                    OnLandEvent.Invoke();
            }
        }



    }










    public void Move(float move, bool jump)
    {




        // Move the character by finding the target velocity
        Vector3 targetVelocity = new Vector2(move * (-unitToPlayer.x) * 10f, rb.velocity.y);
        // And then smoothing it out and applying it to the character
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

        // If the input is moving the player right and the player is facing left...
        if (enemyToPlayer.x<0 && m_movingRight && m_FacingRight)
        {
            // ... flip the player.
            Flip();
        }
        // Otherwise if the input is moving the player left and the player is facing right...
        else if (enemyToPlayer.x>0 && !m_movingRight && !m_FacingRight)
        {
            // ... flip the player.
            Flip();
        }





        // If the player should jump...
        if (m_Grounded && jump)
        {

            animationIsGrounded = false;
            // Add a vertical force to the player.
            m_Grounded = false;
            rb.AddForce(new Vector2(-unitToPlayer.x, m_JumpForce));

            this.jump = false;
        }
    }







    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        Debug.Log("swictching");

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }





}
