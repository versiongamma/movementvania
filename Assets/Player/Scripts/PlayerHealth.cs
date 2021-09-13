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

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

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

    public void TakeDamage(int damage) {
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);

       
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.collider.name == "Enemy") {
            TakeDamage(5);
        }
    }
}
