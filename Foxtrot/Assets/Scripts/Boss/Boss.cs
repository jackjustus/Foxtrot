using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Boss : MonoBehaviour
{
    // This is the parent class used for all the bosses in the game
    // The children of this class will inherit the properties and methods of this class

    [Header("Gameplay Variables")]
    [SerializeField] public int maxHealth = 5;              // The maximum health of the boss
    public int currentHealth {get; private set;}            // The current health of the boss
    [SerializeField] public int contactDamage = 1;          // The amount of damage the boss does on contact

    [Header("Boss Information")]
    public string bossName;                                 // The name of the boss 
    public string bossDescription;                          // The description of the boss

    [Header("Events")]
    public UnityEvent onDeathEvent;                         // The event that will be called when the boss dies

    [Header("Debugging")]
    [SerializeField] private bool debugEnabled = false;

    public Boss() {
        // Constructor for the Boss class

        
    }


    void Awake() {
        // Intialize Health
        currentHealth = maxHealth;

        // Initialize Events
        if (onDeathEvent == null)
            onDeathEvent = new UnityEvent();
    }


    #region Health Methods
    public void Damage(int amount)
    {
        // Error Checking
        if (amount < 0)
            throw new System.ArgumentOutOfRangeException("Cannot have negative Damage");
        else
        {
            // Decrease Health by damage amount
            currentHealth -= amount;

            // Check if the boss is dead
            if (currentHealth <= 0)
                Die();
        }
    }

    public void Heal(int amount)
    {
        // Error Checking
        if (amount < 0)
            throw new System.ArgumentOutOfRangeException("Cannot have negative Healing");
        
        
        // Check if healing would put the boss over max health
        bool wouldBeOverMaxHealth = currentHealth + amount > maxHealth;


        // Applying the healing to the boss
        if (wouldBeOverMaxHealth)
            currentHealth = maxHealth;
        else
            currentHealth += amount;
    }


    private void Die()
    {
        // Called when the boss dies
        onDeathEvent.Invoke();

        // Message the console
        Debug.Log(bossName + " has died");
    }
    #endregion

    #region Contact Damage
    public void DealContactDamage(Collider2D collider)
    {
        // Making sure the collider is the player
        if (collider.GetComponent<PlayerHealth>() != null)
        {
            // Deal damage to the player
            PlayerHealth playerHealth = collider.GetComponent<PlayerHealth>();
            playerHealth.Damage(contactDamage);
        }
    }
    #endregion
}
