using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    Rigidbody2D rb;
    private float movementSpeed = 12;
    private float jumpPower = 20;
    private float maxFallSpeed = 20;
    private bool grounded;
    private bool usedDoubleJump;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update() {

        // Movement Handling                          
        float targetHorizontalV = Input.GetAxis("Horizontal") * movementSpeed;
        // Digital input doesn't have any smoothing, so this lerp smooths out the horizontal acceleration based on the target velocity
        float horizontalV = Mathf.Lerp(rb.velocity.x, targetHorizontalV, Mathf.Abs(rb.velocity.x) < Mathf.Abs(targetHorizontalV) ? 0.05f : 0.2f);
        float verticalV = rb.velocity.y;

        // Jump Handling
        if(Input.GetKeyDown(KeyCode.Space) && (grounded || !usedDoubleJump)) {
            verticalV = jumpPower;
            if (!grounded && !usedDoubleJump) { usedDoubleJump = true; }
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
