using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10;

    private Transform target;

    // Start is called before the first frame update
    void Start(){
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();    // target is set to the players location
    }

    // Update is called once per frame
    void Update(){
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.name != "Shooter") {
            DestroyProjectile();
        }
        
    }

    //Method to destory the projectile
    void DestroyProjectile(){
        Destroy(gameObject);
    }
}
