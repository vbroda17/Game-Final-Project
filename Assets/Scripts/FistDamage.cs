using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FistDamage : MonoBehaviour
{
    public int damageAmount = 10; // The amount of damage inflicted by each hit

    // Called when the collider attached to the fist enters another collider
    private void OnTriggerEnter(Collider other)
    {
        // Debug.Log("HIT SOMETHING");
        // Determine which part of the opponent's body was hit based on its tag
        if (other.CompareTag("Head"))
        {
        Debug.Log("HIT HEAD");

            // Apply damage to the opponent's head
            BoxerHealth boxerHealth = other.GetComponentInParent<BoxerHealth>();
            boxerHealth.TakeDamage(damageAmount);

        }

        if (other.CompareTag("Body"))
        {
        Debug.Log("HIT BODY");
            // Apply damage to the opponent's body
            BoxerHealth boxerHealth = other.GetComponentInParent<BoxerHealth>();
            boxerHealth.TakeDamage(damageAmount / 2); // Example: Body takes half the damage of a headshot
        }
    }

}
