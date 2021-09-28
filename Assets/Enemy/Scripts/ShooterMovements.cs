using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterMovements : MonoBehaviour
{
    private Vector2 startPosition;
    private float timeBetweenProjectiles;
    public float startTimeProjectiles;
    public GameObject projectile;


    private void Start()
    {
        //startPosition is set equal to transform position to track the enemy
        startPosition = transform.position;
        timeBetweenProjectiles = startTimeProjectiles;
    }

    // Update is called once per frame
    void Update()
    {
        //for projectiles
        if (timeBetweenProjectiles <= 0)
        {
            Instantiate(projectile, transform.position, Quaternion.identity); //initiates the projectile to spawn
            timeBetweenProjectiles = startTimeProjectiles;
        }
        else
        {
            timeBetweenProjectiles -= Time.deltaTime;
        }
    }
}
