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
}
