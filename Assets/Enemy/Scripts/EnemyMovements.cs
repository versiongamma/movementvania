using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovements : MonoBehaviour
{
    public float direction;
    public float speed;
    private Vector2 startPosition;
    private Rigidbody2D rb;

    private bool grounded;
    private bool usedDoubleJump;
    private bool usedDash;
    private bool airLauncing;
    private bool keepAirLauncing;
    private bool swinging;
    private bool colliding;
    private bool sliding;
    private float maxFallSpeed = 20;

    [SerializeField] private GameObject sprite;
    private EnemyAnimationController anim;

    private void Start(){
        //Enemy movements
        rb = GetComponent<Rigidbody2D>();
        direction = 1f;
        speed = 3f;

        anim = sprite.GetComponent<EnemyAnimationController>();
    }

    // Update is called once per frame
    void Update(){

        // GROUNDING
        RaycastHit2D groundHitPos = Physics2D.Raycast(transform.position + new Vector3(.4f,0,0), -Vector2.up, 1, LayerMask.GetMask("Geometry"));
        RaycastHit2D groundHitNeg = Physics2D.Raycast(transform.position + new Vector3(-.4f,0,0), -Vector2.up, 1, LayerMask.GetMask("Geometry"));
        Debug.DrawRay(transform.position+ new Vector3(.4f,0,0), -Vector2.up, Color.red);
        Debug.DrawRay(transform.position+ new Vector3(-.4f,0,0), -Vector2.up, Color.red);

        if (groundHitPos.collider != null || groundHitNeg.collider != null) {
            grounded = true;
            usedDoubleJump = false;
            usedDash = false;
            airLauncing = false;

        } else {
            grounded = true;
        }
        
        anim.UpdateGroundedState(grounded);

        Debug.Log("Rigib Body: " + rb);

        float horizontalV = rb.velocity.x;
        float verticalV = rb.velocity.y;

        Debug.Log("horizontal: " + horizontalV);
        Debug.Log("vertical: " + verticalV);

        anim.UpdateVelocity(horizontalV, verticalV);
    }

    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.layer == 8){
            direction *= -1f;
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
