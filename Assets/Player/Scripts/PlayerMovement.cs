using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    // Attached Components
    private Rigidbody2D rb;
    private PlayerEquipment equip;

    // Movement variables
    private float movementSpeed = 12;
    private float movementNormal = 0;
    private float jumpPower = 20;
    private float maxFallSpeed = 20;
    public float dashForce = 10f;
    private float currentDashTime;
    private float dashDirection;
    private Vector2 swingDirection = new Vector2(.5f, .5f);

    // States
    private bool grounded;
    private bool usedDoubleJump;
    private bool airLauncing;
    private bool swinging;
    private bool isDashing;
    private bool colliding;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        equip = GetComponent<PlayerEquipment>();
    }
    void Update() {
        // Grounding
        RaycastHit2D groundHitPos = Physics2D.Raycast(transform.position + new Vector3(.4f,0,0), -Vector2.up, 1, LayerMask.GetMask("Geometry"));
        RaycastHit2D groundHitNeg = Physics2D.Raycast(transform.position + new Vector3(-.4f,0,0), -Vector2.up, 1, LayerMask.GetMask("Geometry"));
        Debug.DrawRay(transform.position+ new Vector3(.4f,0,0), -Vector2.up, Color.red);
        Debug.DrawRay(transform.position+ new Vector3(-.4f,0,0), -Vector2.up, Color.red);

        if (groundHitPos.collider != null || groundHitNeg.collider != null) {
            grounded = true;
            usedDoubleJump = false;
            airLauncing = false;
        } else {
            grounded = false;
        }

        float horizontalV = rb.velocity.x;
        float verticalV = rb.velocity.y;

        float targetHorizontalV = Input.GetAxis("Horizontal") * movementSpeed;
        
        if (!airLauncing) {
            // Digital input doesn't have any smoothing, so this lerp smooths out the horizontal acceleration based on the target velocity
            horizontalV = Mathf.Lerp(rb.velocity.x, targetHorizontalV, Mathf.Abs(rb.velocity.x) < Mathf.Abs(targetHorizontalV) ? 15f * Time.deltaTime : 30f * Time.deltaTime);
        } else {
            horizontalV = Mathf.Lerp(horizontalV, 0, Time.deltaTime);
            if (Input.GetAxis("Horizontal") != 0) airLauncing = false;
        }

        // Jump Handling
        if(Input.GetKeyDown(KeyCode.Space) && (grounded || (!usedDoubleJump && equip.GetPowerupState(PowerUps.DoubleJump)))) {
            verticalV = jumpPower;
            if (!grounded && !usedDoubleJump) { usedDoubleJump = true; }
        }

        if (horizontalV != 0) movementNormal = Mathf.Clamp(horizontalV, -1, 1);
        
        //code for dashing
        if (Input.GetKeyDown(KeyCode.Z) && horizontalV != 0 && equip.GetPowerupState(PowerUps.Dash)){
            isDashing = true;
            rb.velocity = Vector2.zero;
            dashDirection = (int)horizontalV;

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

        // Wall Slide and Jump
        RaycastHit2D onWallLeft = Physics2D.Raycast(transform.position, -Vector2.right, .6f, LayerMask.GetMask("Geometry"));
        RaycastHit2D onWallRight = Physics2D.Raycast(transform.position, Vector2.right, .6f, LayerMask.GetMask("Geometry"));
        Debug.DrawRay(transform.position, -Vector2.right * .6f, Color.blue);
        Debug.DrawRay(transform.position, Vector2.right * .6f, Color.blue);
        if (onWallLeft.collider != null || onWallRight.collider != null) {

            if (verticalV <= 0 && Mathf.Abs(Input.GetAxis("Horizontal")) > 0) {
                verticalV = Mathf.Clamp(verticalV, -maxFallSpeed / 5, 0);

                if (Input.GetKeyDown(KeyCode.Space)) {
                    verticalV = jumpPower;
                }
            }
        }

        // Swinging
        if (!swinging)swingDirection = movementNormal > 0 ? new Vector2(.5f, .5f) : new Vector2(-.5f, .5f); 
        RaycastHit2D hit = Physics2D.Raycast(transform.position, swingDirection, 14.14f, LayerMask.GetMask("Geometry"));
        Debug.DrawRay(transform.position, swingDirection * 20, Color.green);

        if (hit.collider != null && Input.GetKeyDown(KeyCode.LeftShift) && !grounded) {
            
            StartCoroutine(StartSwing(hit.point, swingDirection.x > 0));
        }


        rb.velocity = new Vector3(horizontalV, Mathf.Clamp(verticalV, -maxFallSpeed, float.MaxValue), 0);
    }

    IEnumerator StartSwing(Vector2 point, bool direction) {
        rb.velocity = Vector3.zero;
        rb.gravityScale = 0;

        yield return new WaitForSeconds(.1f);

        StartCoroutine(SwingAroundPoint(point, direction));
    }

    IEnumerator SwingAroundPoint(Vector2 point, bool direction) {

        swinging = true;
        Vector3 startPos = transform.position;
        float distance = Mathf.Sqrt(2 * Mathf.Pow(Vector3.Distance(transform.position, new Vector3(point.x, point.y, 0)), 2));
        float x = startPos.x;
        Vector2 nextPoint = transform.position, velocity = new Vector2();

        Debug.Log("Swinging");

        while (!Input.GetKeyUp(KeyCode.LeftShift) && (direction ? (x < startPos.x + distance) : (x > startPos.x - distance))) {
            rb.velocity = Vector3.zero;
            rb.gravityScale = 0;
            if (colliding) { break; }

            velocity = (AwfulVectorCircleLerp(startPos, point, x) - nextPoint).normalized;
            nextPoint = AwfulVectorCircleLerp(startPos, point, x);
            Debug.DrawRay(transform.position, velocity.normalized * 20, Color.black);
            transform.position = nextPoint;

            if (direction) x += Time.deltaTime * 20;
            else x -= Time.deltaTime * 20;

            yield return 0;
        }
        
        airLauncing = true;
        swinging = false;
        rb.velocity = velocity.normalized * 20 + new Vector2(0, 5);
        rb.gravityScale = 4.7f;
        yield break;
    }

    Vector2 AwfulVectorCircleLerp(Vector2 position, Vector2 target, float x) {
        return new Vector2(x, target.y - Mathf.Sqrt((target.y*target.y) - (2 * position.y * target.y) + (position.x * position.x) + (position.y * position.y) + (2 * x * target.x) - (2 * position.x * target.x) - x * x));
    }

    void OnCollisionEnter2D(Collision2D col) { colliding = true; }

    void OnCollisionExit2D(Collision2D col) { colliding = false; }
}
