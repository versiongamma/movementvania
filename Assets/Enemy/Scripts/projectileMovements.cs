using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectileMovements : MonoBehaviour
{
    public float speed = 0.5f;

    private Transform player;
    private Vector2 target;
    private GameObject projectile;

    // Start is called before the first frame update
    void Start(){
        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = new Vector2(player.position.x, player.position.y);
    }

    // Update is called once per frame
    void Update(){
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if(transform.position.x == target.x && transform.position.y == target.y)
        {
            Destroy(projectile);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(projectile);
        }
    }
}
