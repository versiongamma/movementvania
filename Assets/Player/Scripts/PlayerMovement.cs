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
    public float startDashTime = 0.5f;
    private float currentDashTime;
    private float dashDirection;
    private bool isDashing;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update() {
        float targetHorizontalV = Input.GetAxis("Horizontal") * movementSpeed;
        // Digital input doesn't have any smoothing, so this lerp smooths out the horizontal acceleration based on the target velocity
        float horizontalV = Mathf.Lerp(rb.velocity.x, targetHorizontalV, Mathf.Abs(rb.velocity.x) < Mathf.Abs(targetHorizontalV) ? 0.05f : 0.2f);
        float verticalV = rb.velocity.y;

        // Jump Handling
        if(Input.GetKeyDown(KeyCode.Space) && (grounded || !usedDoubleJump)) {
            verticalV = jumpPower;
            if (!grounded && !usedDoubleJump) { usedDoubleJump = true; }
        }
        
        //code for dashing
        if (Input.GetKeyDown(KeyCode.Z) && horizontalV != 0){
            isDashing = true;
            currentDashTime = startDashTime;
            rb.velocity = Vector2.zero;
            dashDirection = (int)horizontalV;

            if (isDashing)
            {
                rb.velocity = transform.right * dashDirection * dashForce;
                currentDashTime -= Time.deltaTime;

                if (currentDashTime <= 0)
                {
                    isDashing = false;
                }
            }
        }

        rb.velocity = new Vector3(horizontalV, Mathf.Clamp(verticalV, -maxFallSpeed, float.MaxValue), 0);
    }

    // Grounding Triggers
    private void OnTriggerStay2D(Collider2D other) {
        grounded = true;
        usedDoubleJump = false;
    }

    private void OnTriggerExit2D(Collider2D other) {
        grounded = false;        
    }
}
