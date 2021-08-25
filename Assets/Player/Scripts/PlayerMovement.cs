using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    Rigidbody2D rb;
    [SerializeField] private float movmentSpeed = 6;
    [SerializeField] private float jumpPower = 10;

    public float dashForce = 10f;
    public float startDashTime = 0.5f;
    private float currentDashTime;
    private float dashDirection;
    private bool isDashing;

    // Start is called before the first frame update
    void Start(){
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update(){
        //horizontal and vertical movements
        float horizontalV = Input.GetAxis("Horizontal") * movmentSpeed;
        float verticalV = rb.velocity.y;

        //basic movements if/else loop (incl double jumps and ground checks)
        if (Input.GetKeyDown(KeyCode.Space)){
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
        //code for dashing
    }
}
