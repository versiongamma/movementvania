using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public HealthBar healthBar;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    void Update()
    {
 
    }

    public int getPlayerHealth() 
    {
        return this.currentHealth;
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.name == "Enemy")
        {
            TakeDamage(5);
        }
    }
}
