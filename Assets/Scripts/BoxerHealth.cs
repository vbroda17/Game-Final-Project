using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BoxerHealth : MonoBehaviour
{
    public int maxHealth = 30;
    public int currentHealth;

    // Define a delegate type for the health zero event
    public delegate void HealthZeroAction();

    // Define an event based on the delegate
    public event HealthZeroAction OnHealthZero;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        // Debug.Log("Damage Taken: " + amount); // Log the damage taken
        // Debug.Log("Health Left: " + currentHealth); // Log the damage taken
        // Check if health is zero or lower
        if (currentHealth <= 0)
        {
            Debug.Log("Health depleted");
            // Invoke the health zero event
            if (OnHealthZero != null)
            {
                OnHealthZero();
            }
        }
    }

    public void RestoreAllHealth()
    {
        currentHealth = maxHealth;
    }
}