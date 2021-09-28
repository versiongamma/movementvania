using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovements : MonoBehaviour
{
    public float direction;
    public float speed;
    private Vector2 startPosition;
    private Rigidbody2D rb;


    private void Start(){

        //enemy movements
        rb = GetComponent<Rigidbody2D>();
        direction = -1f;
        speed = 3f;
    }

    // Update is called once per frame
    void Update(){
    }

    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.tag == "RightWall"){
            direction *= -1f;
            Debug.Log("Collision with right wall!");
        }

        if (collision.gameObject.tag == "LeftWall")
        {
            direction *= -1f;
            Debug.Log("Collision with left wall!");
        }
        if (collision.gameObject.tag == "Door")
        {
            direction *= -1f;
            Debug.Log("Collision with door 2!!");
        }
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(direction * speed, rb.velocity.y);
    }
}
