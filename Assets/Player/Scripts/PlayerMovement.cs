using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    Rigidbody2D rb;
    private float movementSpeed = 12;
    private float jumpPower = 20;
    private float maxFallSpeed = 20;
    private bool grounded;
    private bool usedDoubleJump;

    public float dashForce = 10f;
    private float currentDashTime;
    private float dashDirection;
    private bool isDashing;

    //Sound effect handling
    [SerializeField] private AudioSource jumpSound;
    [SerializeField] private AudioSource dashSound;
    [SerializeField] private AudioSource footstepSound;
    private System.Random randInt;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        randInt = new System.Random();
    }
    void Update() {
        // Grounding
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, 1, LayerMask.GetMask("Geometry"));
        Debug.DrawRay(transform.position, -Vector2.up, Color.red);

        if (hit.collider != null) {
            grounded = true;
            usedDoubleJump = false;
        } else {
            grounded = false;
        }


        float targetHorizontalV = Input.GetAxis("Horizontal") * movementSpeed;
        // Digital input doesn't have any smoothing, so this lerp smooths out the horizontal acceleration based on the target velocity
        float horizontalV = Mathf.Lerp(rb.velocity.x, targetHorizontalV, Mathf.Abs(rb.velocity.x) < Mathf.Abs(targetHorizontalV) ? 0.05f : 0.2f);
        float verticalV = rb.velocity.y;

        // Handle footstep sound
        if (grounded == true && (Input.GetAxis("Horizontal") > 0 || Input.GetAxis ("Horizontal") < 0))
        {
            // Change footstep pitch for variety
            switch(randInt.Next(3))
            {
                case (0):
                    footstepSound.pitch = 0.6f;
                    break;
                case (1):
                    footstepSound.pitch = 0.8f;
                    break;
                case (2):
                    footstepSound.pitch = 0.7f;
                    break;
            }
            if (!footstepSound.isPlaying) { footstepSound.Play(); }
        }
        if (Input.GetAxis("Horizontal") == 0) { footstepSound.Stop(); }

        // Jump Handling
        if(Input.GetKeyDown(KeyCode.Space) && (grounded || !usedDoubleJump)) {
            verticalV = jumpPower;
            jumpSound.pitch = 1f;
            jumpSound.Play();
            if (!grounded && !usedDoubleJump)
            {
                usedDoubleJump = true;
                jumpSound.pitch = 1.3f;
            }
        }
        
        //code for dashing
        if (Input.GetKeyDown(KeyCode.Z) && horizontalV != 0){
            isDashing = true;
            rb.velocity = Vector2.zero;
            dashDirection = (int)horizontalV;

            dashSound.Play();

            if (isDashing)
            {
                horizontalV = dashDirection * dashForce;
                currentDashTime -= Time.deltaTime;

                if (currentDashTime <= 0)
                {
                    isDashing = false;
                }
            }
        }

        rb.velocity = new Vector3(horizontalV, Mathf.Clamp(verticalV, -maxFallSpeed, float.MaxValue), 0);
    }
}
