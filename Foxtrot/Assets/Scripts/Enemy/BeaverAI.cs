using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.U2D;

public class BeaverAI : MonoBehaviour
{


    Animator animator;


    [Header("Enemy Constants")]
    [Space]

    private GameObject attackArea = default;

    [SerializeField] private float enemySpeed = 20f;
    [SerializeField] private float detectionRange = 200f;
    [SerializeField] private float maxAttackRange = 2f;
    [SerializeField] private float minAttackRange = 0f;
    private Transform player;
    private Rigidbody2D rb;


    [SerializeField] private float m_JumpForce = 100f;                          // Amount of force added when the player jumps.
    [Range(0, 1f)][SerializeField] private float m_MovementSmoothing = 0f;     // How much to smooth out the movement
    [SerializeField] private LayerMask m_WhatIsGround;                          // A mask determining what is ground to the character
    [SerializeField] private Transform m_GroundCheck;                           // A position marking where to check if the player is grounded.
    [SerializeField] private Transform m_CeilingCheck;                          // A position marking where to check for ceilings



    [SerializeField] private float attackCooldown;
    //[SerializeField] private float range;
    //[SerializeField] private int damage;

    private float timer = 0f;
    private float timeToAttack = 1.5f;

    private float cooldownTimer = Mathf.Infinity;


    SpriteRenderer sprite;

    // Jump & ground collision stuff
    const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
    private bool m_Grounded;            // Whether or not the player is grounded.
    private bool m_FacingRight = true;  // For determining which way the player is currently facing.
    private bool m_movingRight;
    private Vector3 m_Velocity = Vector3.zero;
    bool jump = false;


    private bool attacking = false;

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

        attackArea = transform.GetChild(0).gameObject;

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

        timer+= Time.deltaTime;

        if (cooldownTimer >= 1.5f)
        {
            attacking = false;
        }

        if (attacking)
        {
            //set the rigidbody velocity to 0
            rb.velocity = new Vector2(0, rb.velocity.y);
        }


        if (rb.velocity.x > 0)
        {
            m_movingRight = true;
        }
        if (rb.velocity.x < 0)
        {
            m_movingRight = false;
        }

        cooldownTimer += Time.deltaTime;


        if (attacking)
        {
            
            timer += Time.deltaTime;

            if (timer >= timeToAttack)
            {
                timer = 0;
                attacking = false;
                attackArea.SetActive(attacking);
                

            }
        }


        //animator.SetBool("isJumping", !animationIsGrounded);

        if (player != null)
        {

            Vector3 pos = transform.position;

            // Creating a unit vector from the player to the enemy
            enemyToPlayer = (pos - player.position);
            unitToPlayer = enemyToPlayer.normalized;

            float distanceToPlayer = enemyToPlayer.magnitude;


            // Move towards the player if within detection range
            if (distanceToPlayer <= detectionRange && !attacking)
            {


                // Perform attack or other actions when within attack range
                if (distanceToPlayer <= maxAttackRange && distanceToPlayer >= minAttackRange && cooldownTimer >= attackCooldown)
                {
                    // Perform attack action here
                    attacking = true;
                    cooldownTimer = 0;
                    animator.SetBool("isAttacking", true);
                    StartCoroutine(MeleeAttack());


                }
                else
                {
                    animator.SetBool("isAttacking", false);
                }

                Move(enemySpeed * Time.fixedDeltaTime, jump);


            }
            else
            {
                // Enemy is outside detection range
            }
        }
        else
        {
            Debug.print("BIG ERROR: ENEMY PLAYER DOESNT EXIST");
        }




    }

    private IEnumerator MeleeAttack()
    {

        Debug.Log("Flipped");
        cooldownTimer = 0;
        attacking = true;

        Flip();


        yield return new WaitForSeconds(0.5f);


        
        attackArea.SetActive(attacking);

    }










    private void FixedUpdate()
    {


        animator.SetFloat("xVelocity", Mathf.Abs(rb.velocity.x));
        //animator.SetFloat("yVelocity", rb.velocity.y);

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

        if(!attacking)
        {
            if (enemyToPlayer.x < 0 && m_movingRight && m_FacingRight)
            {
                // ... flip the player.
                Flip();
            }
            // Otherwise if the input is moving the player left and the player is facing right...
            else if (enemyToPlayer.x > 0 && !m_movingRight && !m_FacingRight)
            {
                // ... flip the player.
                Flip();
            }
        }
        





        // If the player should jump...
        if (m_Grounded && jump)
        {


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


        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
        
    }





}
