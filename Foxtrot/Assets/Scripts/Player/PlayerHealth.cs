using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 5;
    [SerializeField] private int currentHealth = 5;

    private int invincibilityTime = 1;

    private float invincibilityTimer = 0f;

    private HealthOverlayController healthOverlayController;

    void Awake() {
        healthOverlayController = FindObjectOfType<HealthOverlayController>();
    }

    void FixedUpdate() {

        UpdateHealthOverlay();

    }

    void Update()
    {
        invincibilityTimer += Time.deltaTime;
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
