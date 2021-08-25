using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    Rigidbody2D rb;
    [SerializeField] private float movementSpeed = 8;
    [SerializeField] private float jumpPower = 10;

    public float dashForce = 10f;
    public float startDashTime = 0.5f;
    private float currentDashTime;
    private float dashDirection;
    private bool isDashing;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update(){
        float targetHorizontalV = Input.GetAxis("Horizontal") * movementSpeed;
                                  // Digital input doesn't have any smoothing, so this lerp smooths out  
                                  // the horizontal acceleration based on the target velocity
        float horizontalV = Mathf.Lerp(rb.velocity.x, targetHorizontalV, Mathf.Abs(rb.velocity.x) < Mathf.Abs(targetHorizontalV) ? 0.05f : 0.2f);
        float verticalV = rb.velocity.y;

        // Needs grounding later
        if(Input.GetKeyDown(KeyCode.Space)) {
            verticalV += jumpPower;
        }


        rb.velocity = new Vector3(horizontalV, verticalV, 0);

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
        
        rb.velocity = new Vector3(horizontalV, verticalV, 0);
    }
}
