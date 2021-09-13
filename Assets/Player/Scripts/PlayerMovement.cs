using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour {

    // Attached Components
    private Rigidbody2D rb;
    private PlayerEquipment equip;
    [SerializeField] private CameraMovement cam;

    [Header("Dash Attachments")]
    [SerializeField] private ParticleSystem dashParticles;

    [Header("Hook Attachments")]
    [SerializeField] private HookRenderer hookRenderer;
    [SerializeField] private HookIndicatorRenderer hookIndicatorRenderer;

    // Movement variables
    private float movementSpeed = 12;
    private float movementNormal = 0;
    private float jumpPower = 20;
    private float maxFallSpeed = 20;
    private float dashForce = 10f;
    private Vector2 swingDirection = new Vector2(.5f, .5f);
    private Vector2 dashDirection = new Vector2(1f, 0f);

    // States
    private bool grounded;
    private bool usedDoubleJump;
    private bool usedDash;
    private bool airLauncing;
    private bool keepAirLauncing;
    private bool swinging;
    private bool colliding;
    private bool sliding;

    [Header("Sound Effects")]
    //Sound effect handling
    [SerializeField] private AudioSource jumpSound;
    [SerializeField] private AudioSource dashSound;
    [SerializeField] private AudioSource footstepSound;
     private System.Random randInt;

    [Header("Input")]
     public InputController inputController;
    public Animator anim;

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
            usedDash = false;
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

        // Jump
        if (
            inputController.isJumpActive() && 
            !sliding &&
            (grounded || (!usedDoubleJump && equip.GetPowerupState(PowerUps.DoubleJump)))
        ) {      
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

        dashDirection = new Vector2(inputController.getHorizontalAxis(), inputController.getVerticalAxis());
        
        //Dash
        if (
            inputController.isDashActive() && 
            !usedDash && 
            !swinging &&
            dashDirection != Vector2.zero && 
            equip.GetPowerupState(PowerUps.Dash)
        ) {
            StartCoroutine(Dash(dashDirection));
            dashSound.Play();
        }

        // Wall Slide and Jump
        RaycastHit2D onWallLeft = Physics2D.Raycast(transform.position, -Vector2.right, .6f, LayerMask.GetMask("Geometry"));
        RaycastHit2D onWallRight = Physics2D.Raycast(transform.position, Vector2.right, .6f, LayerMask.GetMask("Geometry"));
        Debug.DrawRay(transform.position, -Vector2.right * .6f, Color.blue);
        Debug.DrawRay(transform.position, Vector2.right * .6f, Color.blue);
        if (onWallLeft.collider != null || onWallRight.collider != null) {

            if (verticalV <= 0 && Mathf.Abs(inputController.getHorizontalAxis()) > 0 && equip.GetPowerupState(PowerUps.WallJump)) {
                verticalV = Mathf.Clamp(verticalV, -maxFallSpeed / 5, 0);
                sliding = true;

                if (inputController.isJumpActive()) {
                    verticalV = jumpPower;
                    horizontalV = -inputController.getHorizontalAxis() * 15;
                    airLauncing = true;
                    StartCoroutine(KeepAirLaunch(.1f));
                }
            }
        } else sliding = false;

        // Swinging
        if (!swinging)swingDirection = movementNormal > 0 ? new Vector2(.5f, .5f) : new Vector2(-.5f, .5f); 
        RaycastHit2D hit = Physics2D.Raycast(transform.position, swingDirection, 14.14f, LayerMask.GetMask("Geometry"));
        Debug.DrawRay(transform.position, swingDirection * 20, Color.green);

        if (hit.collider != null && !grounded) {
            hookIndicatorRenderer.Display(hit.point);
            if (inputController.isSwingDown()  && equip.GetPowerupState(PowerUps.Swing)) { 
                StartCoroutine(StartSwing(hit.point, swingDirection.x > 0));
            }
        } else hookIndicatorRenderer.Remove();


        rb.velocity = new Vector3(horizontalV, Mathf.Clamp(verticalV, -maxFallSpeed, float.MaxValue), 0);

        if (SaveLoad.loaded)
        {
            Debug.Log("Moving player to loaded position!");
            if (String.Compare(SceneManager.GetActiveScene().name, SaveLoad.activeSceneName) != 0)
                SceneManager.LoadScene(SaveLoad.activeSceneName, LoadSceneMode.Single);

            Physics2D.SyncTransforms();
            rb.position = new Vector2(SaveLoad.position[0], SaveLoad.position[1]);
            rb.transform.position = new Vector2(SaveLoad.position[0], SaveLoad.position[1]);

            Camera.main.transform.position = new Vector3(SaveLoad.cameraPosition[0], SaveLoad.cameraPosition[1], SaveLoad.cameraPosition[2]);

            GameObject.Find("Main Camera").GetComponent<CameraMovement>().SetTranslation(SaveLoad.translateX, SaveLoad.translateY);
            GameObject.Find("Main Camera").GetComponent<CameraMovement>().setCameraMinMax(SaveLoad.cameraMinMax[0], SaveLoad.cameraMinMax[1], SaveLoad.cameraMinMax[2], SaveLoad.cameraMinMax[3]);

            equip.setPowerUps(SaveLoad.powerups);

            SaveLoad.clear();
            SaveLoad.loaded = false;
            
        }

        HandleAnimation();
    }

    void HandleAnimation()
    {
        anim.SetFloat("xVel", Mathf.Abs(inputController.getHorizontalAxis()));
        anim.SetFloat("yVel", rb.velocity.y);
        anim.SetBool("isDashing", inputController.isDashActive());
        anim.SetBool("isGrounded", grounded);
        anim.SetBool("hasDoubleJumped", usedDoubleJump);
    }

    IEnumerator Dash(Vector2 direction) {
        float t = Time.deltaTime;
        Vector2 startPos = transform.position;
        dashParticles.Play();
        cam.DashSmoothing();

        while (t < .1f) {
            rb.velocity = Vector2.zero;
            rb.gravityScale = 0;
            
            Vector2 nextPosition = Vector2.Lerp(startPos, startPos + (direction * 7), t * 20);
            RaycastHit2D collide = Physics2D.Raycast(transform.position, direction, Vector2.Distance(transform.position, nextPosition), LayerMask.GetMask("Geometry"));
            if (colliding) break;
            if (collide.collider != null) { 
                transform.position = collide.point;
                break; 
            }
            transform.position = nextPosition;

            t += Time.deltaTime;
            yield return 0;
        }
        
        usedDash = true;
        rb.gravityScale = 4.7f;
        dashParticles.Stop();
        yield break;
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

        hookRenderer.Attach(point);

        yield return new WaitForSeconds(.1f);

        StartCoroutine(SwingAroundPoint(point, direction));
    }

    IEnumerator SwingAroundPoint(Vector2 point, bool direction) {

        swinging = true;
        Vector3 startPos = transform.position;
        float distance = Mathf.Sqrt(2 * Mathf.Pow(Vector3.Distance(transform.position, new Vector3(point.x, point.y, 0)), 2));
        float x = startPos.x;
        Vector2 nextPoint = transform.position, velocity = new Vector2();

        while (inputController.isSwingActive() && (direction ? (x < startPos.x + distance) : (x > startPos.x - distance))) {
            rb.velocity = Vector3.zero;
            rb.gravityScale = 0;
            hookIndicatorRenderer.Remove();
            if (colliding) { break; }

            velocity = (CustomMath.CircleVectorLerp(startPos, point, x) - nextPoint).normalized;
            nextPoint = CustomMath.CircleVectorLerp(startPos, point, x);
            if (float.IsNaN(nextPoint.y)) { break; }
            Debug.DrawRay(transform.position, velocity.normalized * 20, Color.black);
            transform.position = nextPoint;

            if (direction) x += Time.deltaTime * 20;
            else x -= Time.deltaTime * 20;

            yield return 0;
        }
        
        airLauncing = true;
        swinging = false;
        usedDash = false;
        rb.velocity = velocity.normalized * 20 + new Vector2(0, 5);
        rb.gravityScale = 4.7f;
        hookRenderer.Dettach();
        yield break;
    }

    public void EndMovement() { colliding = true; }

    void OnCollisionEnter2D(Collision2D col) { colliding = true; }

    void OnCollisionExit2D(Collision2D col) { colliding = false; }

    public Vector2 getPlayerPosition() 
    {
        return this.rb.position;
    }
    public PlayerEquipment getPlayerEquipment() 
    {
        return this.equip;
    }
}
