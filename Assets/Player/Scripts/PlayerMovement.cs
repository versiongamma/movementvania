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
    private bool keepAirLauncing;
    private bool swinging;
    private bool isDashing;
    private bool colliding;

    //Sound effect handling
    [SerializeField] private AudioSource jumpSound;
    [SerializeField] private AudioSource dashSound;
    [SerializeField] private AudioSource footstepSound;
     private System.Random randInt;


     public InputController inputController;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        equip = GetComponent<PlayerEquipment>();
        inputController = GetComponent<InputController>();
        randInt = new System.Random();
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

        float targetHorizontalV = inputController.getHorizontalAxis() * movementSpeed;
        
        if (!airLauncing) {
            // Digital input doesn't have any smoothing, so this lerp smooths out the horizontal acceleration based on the target velocity
            horizontalV = Mathf.Lerp(rb.velocity.x, targetHorizontalV, Mathf.Abs(rb.velocity.x) < Mathf.Abs(targetHorizontalV) ? 15f * Time.deltaTime : 30f * Time.deltaTime);
        } else {
            horizontalV = Mathf.Lerp(horizontalV, 0, Time.deltaTime);
            if (inputController.getHorizontalAxis() != 0 && !keepAirLauncing) airLauncing = false;
        }

        // Handle footstep sound
        if (grounded == true && (inputController.getHorizontalAxis() > 0 || inputController.getHorizontalAxis() < 0))
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
        if (inputController.getHorizontalAxis()== 0) { footstepSound.Stop(); }

        // Jump Handling
        if(inputController.isJumpActive() && (grounded || (!usedDoubleJump && equip.GetPowerupState(PowerUps.DoubleJump)))) {
            verticalV = jumpPower;
            jumpSound.pitch = 1f;
            jumpSound.Play();
            if (!grounded && !usedDoubleJump)
            {
                usedDoubleJump = true;
                jumpSound.pitch = 1.3f;
            }
        }

        if (horizontalV != 0) movementNormal = Mathf.Clamp(horizontalV, -1, 1);
        
        //code for dashing
        if (inputController.isDashActive() && horizontalV != 0 && equip.GetPowerupState(PowerUps.Dash)){
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

        // Wall Slide and Jump
        RaycastHit2D onWallLeft = Physics2D.Raycast(transform.position, -Vector2.right, .6f, LayerMask.GetMask("Geometry"));
        RaycastHit2D onWallRight = Physics2D.Raycast(transform.position, Vector2.right, .6f, LayerMask.GetMask("Geometry"));
        Debug.DrawRay(transform.position, -Vector2.right * .6f, Color.blue);
        Debug.DrawRay(transform.position, Vector2.right * .6f, Color.blue);
        if (onWallLeft.collider != null || onWallRight.collider != null) {

            if (verticalV <= 0 && Mathf.Abs(inputController.getHorizontalAxis()) > 0) {
                verticalV = Mathf.Clamp(verticalV, -maxFallSpeed / 5, 0);

                if (inputController.isJumpActive()) {
                    verticalV = jumpPower;
                    horizontalV = -inputController.getHorizontalAxis() * 15;
                    airLauncing = true;
                    StartCoroutine(KeepAirLaunch(.1f));
                }
            }
        }

        // Swinging
        if (!swinging)swingDirection = movementNormal > 0 ? new Vector2(.5f, .5f) : new Vector2(-.5f, .5f); 
        RaycastHit2D hit = Physics2D.Raycast(transform.position, swingDirection, 14.14f, LayerMask.GetMask("Geometry"));
        Debug.DrawRay(transform.position, swingDirection * 20, Color.green);

        if (hit.collider != null && inputController.isSwingDown() && !grounded) {
            
            StartCoroutine(StartSwing(hit.point, swingDirection.x > 0));
        }


        rb.velocity = new Vector3(horizontalV, Mathf.Clamp(verticalV, -maxFallSpeed, float.MaxValue), 0);
    }

    public void EndSwing() {
        colliding = true;
    }

    IEnumerator KeepAirLaunch(float time) {
        keepAirLauncing = true;
        yield return new WaitForSeconds(time);
        keepAirLauncing = false;
        airLauncing = false;
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

        while (inputController.isSwingActive() && (direction ? (x < startPos.x + distance) : (x > startPos.x - distance))) {
            rb.velocity = Vector3.zero;
            rb.gravityScale = 0;
            if (colliding) { break; }

            velocity = (CustomMath.CircleVectorLerp(startPos, point, x) - nextPoint).normalized;
            nextPoint = CustomMath.CircleVectorLerp(startPos, point, x);
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

    void OnCollisionEnter2D(Collision2D col) { colliding = true; }

    void OnCollisionExit2D(Collision2D col) { colliding = false; }
}
