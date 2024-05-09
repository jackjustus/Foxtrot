using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 5;
    [SerializeField] private int currentHealth = 5;

    private HealthOverlayController healthOverlayController;

    void Awake() {
        healthOverlayController = FindObjectOfType<HealthOverlayController>();
    }

    void FixedUpdate() {

        UpdateHealthOverlay();

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

        if (amount < 0)
        {
            throw new System.ArgumentOutOfRangeException("Cannot have negative Damage");
        }
        this.currentHealth -= amount;

        if (currentHealth <= 0)
        {
            Die();
        }


    }

    private void Die()
    {
        UnityEngine.Debug.Log("I am Dead");
        //Destroy(gameObject);
    }


}
