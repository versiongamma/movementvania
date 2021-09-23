using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public HealthBar healthBar;
    public GameObject Player;

    // Sets the users health to its max on start, same for health bar
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Checks for if user has died
    void Update()
    {
        if (currentHealth <= 0)
        {
            Destroy(Player);
            SceneManager.LoadScene("GameOverMenu");
        }
    }

    public int getPlayerHealth() 
    {
        return this.currentHealth;
    }

    // Damages player and adjusts healthbar to suit
    public void TakeDamage(int damage) {
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);

       
    }

    // If the player collides with enemy calls damage function
    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.collider.name == "Enemy") {
            TakeDamage(5);
        }

        if (collision.collider.name == "FollowingEnemy")
        {
            TakeDamage(10);
        }
    }
}
