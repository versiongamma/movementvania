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
    [SerializeField] private PlayerAnimationController animContr;
    public Rigidbody2D rb2D;
    public GameObject Dropper;
    public Transform RespawnPoint;

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

    public void Knockback()
    {
        GetComponent<PlayerMovement>().EndMovement();
        rb2D.AddForce(new Vector2((animContr.right ? -1 : 1) * 100, 0), ForceMode2D.Impulse);
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
        //damageSound.Play();
    }

    // If the player collides with enemy calls damage function
    private void OnCollisionEnter2D(Collision2D collision) {

        //loop for damage taken from a following enemy
        if (collision.collider.name == "FollowingEnemy"){
            TakeDamage(2);
            Knockback();
        }

        //loop for damage taken from a projectile
        if (collision.collider.name == "Projectile"){
            TakeDamage(2);
            Knockback();
        }

        if (collision.collider.name == "Jumper"){
            TakeDamage(2);
            Knockback();
        }

        //loop for damage taken from a following enemy
        if (collision.collider.name == "FollowingEnemy"){
            TakeDamage(2);
            Knockback();
        }

        //loop for damage taken from a projectile
        if (collision.collider.name == "Projectile"){
            TakeDamage(2);
            Knockback();
        }

        if (collision.collider.name == "Jumper"){
            TakeDamage(2);
            Knockback();
        }

        if (collision.collider.name == "Dropper"){
            TakeDamage(2);
            Knockback();
            Destroy(GameObject.FindWithTag("Dropper"));
        }

        if (collision.collider.name == "Patroller"){
            TakeDamage(2);
            Knockback();
        }

        if (collision.collider.name == "Spikes"){
            TakeDamage(2);
            Knockback();
        }
    }

    private void OnCollisionStay2D(Collision2D collision) {

        //loop for damage taken from a following enemy
        if (collision.collider.name == "FollowingEnemy"){
            Knockback();
        }

        //loop for damage taken from a projectile
        if (collision.collider.name == "Projectile"){
            Knockback();
        }

        if (collision.collider.name == "Jumper"){
            Knockback();
        }

        //loop for damage taken from a following enemy
        if (collision.collider.name == "FollowingEnemy"){
            Knockback();
        }

        //loop for damage taken from a projectile
        if (collision.collider.name == "Projectile"){
            Knockback();
        }

        if (collision.collider.name == "Jumper"){
            Knockback();
        }

        if (collision.collider.name == "Dropper"){
            Knockback();
            Destroy(GameObject.FindWithTag("Dropper"));
        }

        if (collision.collider.name == "Patroller"){
            Knockback();
        }

        if (collision.collider.name == "Spikes"){
            Knockback();
        }
    }
}
