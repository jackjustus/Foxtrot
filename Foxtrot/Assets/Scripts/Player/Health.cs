using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{

    [SerializeField] private int health = 5;

    private int MAX_HEALTH = 5;

    public UnityEvent OnDeathEvent;



    // Update is called once per frame

    void Awake()
    {
        if (OnDeathEvent == null)
            OnDeathEvent = new UnityEvent();
    }
    private void Update()
    {




        if (Input.GetKeyDown(KeyCode.H))
        {
            Heal(1);
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            Damage(1);
        }

    }

    public void Damage(int amount)
    {

        if (amount < 0)
        {
            throw new System.ArgumentOutOfRangeException("Cannot have negative Damage");
        }
        this.health -= amount;

        if (health <= 0)
        {
            Die();
        }


    }

    public void Heal(int amount)
    {

        if (amount < 0)
        {
            throw new System.ArgumentOutOfRangeException("Cannot have negative Healing");
        }

        bool wouldBeOverMaxHealth = health + amount > MAX_HEALTH;


        if (wouldBeOverMaxHealth)
        {
            this.health = MAX_HEALTH;
        }
        else
        {
            this.health += amount;
        }

    }

    private void Die()
    {
        OnDeathEvent.Invoke();


        UnityEngine.Debug.Log("I am Dead");
        Destroy(gameObject);
    }
}
