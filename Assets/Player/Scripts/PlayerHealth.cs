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
        //if the current health of the player is less than or equal to 0
        //then the player is destroyed and the game over menu is called
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

    //Used to detect collision between the player and the enemy
    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.collider.name == "Enemy") {
            //int 5 passed through into the take damage method
            //5 damage is taken from the players health
            TakeDamage(5);
        }
    }
}
