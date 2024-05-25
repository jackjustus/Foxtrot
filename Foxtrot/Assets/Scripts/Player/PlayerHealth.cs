using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 5;
    [SerializeField] private int currentHealth = 5;
    private PlayerMovement playerMovement;
    private GameObject player;

    private Collider2D collision;

    private int invincibilityTime = 1;

    private float invincibilityTimer = 0f;

    private HealthOverlayController healthOverlayController;

    void Awake() {
        healthOverlayController = FindObjectOfType<HealthOverlayController>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerMovement = player.GetComponent<PlayerMovement>();

    }

    void FixedUpdate() {

        UpdateHealthOverlay();

    }

    void Update()
    {
        invincibilityTimer += Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision1)
    {
       if(collision1.gameObject.layer == 8 || collision1.gameObject.layer == 10)
        {
            collision = collision1;
        }
        
        
    }
        
    

    private void UpdateHealthOverlay() {
        if (healthOverlayController != null) {
            healthOverlayController.SetHealthOverlay(currentHealth, maxHealth);
        }
    }



    public int getPlayerMaxHealth() {
        return maxHealth;
    }




    public void Damage(int amount)
    {
        if(invincibilityTimer < invincibilityTime)
        {
            return;
        }


        if (collision.transform.parent != null)
        {
            if (collision.transform.parent.position.x >= transform.position.x)
            {
                playerMovement.KnockFromRight = true;
            }
            if (collision.transform.parent.position.x < transform.position.x)
            {
                playerMovement.KnockFromRight = false;
            }

        }
        else
        {
            if (collision.transform.position.x >= transform.position.x)
            {
                playerMovement.KnockFromRight = true;
            }
            if (collision.transform.position.x < transform.position.x)
            {
                playerMovement.KnockFromRight = false;
            }
        }



        



        playerMovement.KBCounter = playerMovement.KBTotalTime;

        if (amount < 0)
        {
            throw new System.ArgumentOutOfRangeException("Cannot have negative Damage");
        }
        this.currentHealth -= amount;

        if (currentHealth <= 0)
        {
            Die();
        }

        invincibilityTimer = 0f;

    }

    private void Die()
    {
        UnityEngine.Debug.Log("I am Dead");
        //Destroy(gameObject);
    }


}
