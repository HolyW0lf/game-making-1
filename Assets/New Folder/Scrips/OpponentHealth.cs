using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            // Handle defeat logic here
            Debug.Log("Opponent defeated!");
        }
    }
}
