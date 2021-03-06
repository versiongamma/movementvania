using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumper : MonoBehaviour
{
    public float direction;
    public float speed;
    public float jumpHeight = 20f;
    private bool isGrounded = true;
    public bool inPlace = true;
    private Vector2 startPosition;
    private Rigidbody2D rb;


    private void Start(){
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isGrounded == true){
            rb.AddForce(new Vector2(2, jumpHeight), ForceMode2D.Impulse);
            isGrounded = false;
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8){
            if (inPlace) { direction *= -1f; }
            else if (collision.gameObject.name == "Left Wall" || collision.gameObject.name == "Right Wall") {
                direction *= -1f;
            }
            isGrounded = true;
        }

        if (collision.gameObject.tag == "Door"){
            direction *= -1f;
        }
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(direction * speed, rb.velocity.y);
    }
}
