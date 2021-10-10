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
    public Rigidbody2D rb2D;

    //Damage sfx
    [SerializeField] private AudioSource damageSound;

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

    public void Knockback(Collision2D obj)
    {
        Debug.Log(obj.transform.position.y);
        Debug.Log(this.transform.position.y);

        Vector2 direction = (obj.transform.position - this.transform.position).normalized;
        rb2D.AddForce(-direction * 50, ForceMode2D.Impulse);
    }

    public int getPlayerHealth() 
    {
        return this.currentHealth;
    }

    public void setPlayerHealth(int health) 
    {
        this.currentHealth = health;
    }

    // Damages player and adjusts healthbar to suit
    public void TakeDamage(int damage) {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        damageSound.Play();
    }

    // If the player collides with enemy calls damage function
    private void OnCollisionEnter2D(Collision2D collision) {
        //loop for damage taken from a basic enemy
        if(collision.collider.name == "Enemy") {
            TakeDamage(5);
            Knockback(collision);
        }

        //loop for damage taken from a following enemy
        if (collision.collider.name == "FollowingEnemy")
        {
            TakeDamage(10);
        }

        //loop for damage taken from a projectile
        if (collision.collider.name == "Projectile")
        {
            TakeDamage(10);
        }

        if (collision.collider.name == "Jumper")
        {
            TakeDamage(10);
        }
    }
}
