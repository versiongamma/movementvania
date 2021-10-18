using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// Handles the controlling of the player character, and any accosiated functions that
// result from moving the player, such as sound and animation
public class PlayerMovement : MonoBehaviour {

    /** ATTACHED COMPONENTS **/
    private Rigidbody2D rb;
    private PlayerEquipment equip;
    [SerializeField] private CameraMovement cam;

    [Header("Dash Attachments")]
    [SerializeField] private ParticleSystem dashParticles;

    [Header("Hook Attachments")]
    [SerializeField] private HookRenderer hookRenderer;
    [SerializeField] private HookIndicatorRenderer hookIndicatorRenderer;

    /** MOVEMENT VARIABLES **/
    // These values effect the movement of the player, editing them will
    // affect how they feel to control. Also has some initilisation of
    // directionality variables used in certain movement abilities
    private float movementSpeed = 12;
    private float movementNormal = 0;
    private float jumpPower = 20;
    private float maxFallSpeed = 20;
    private float dashForce = 10f;
    private Vector2 swingDirection = new Vector2(.5f, .5f);
    private Vector2 dashDirection = new Vector2(1f, 0f);

    //** STATES **//
    // States can be used to determine what the player is currently doing,
    // i.e. if they are on the ground, if they are currently swinging, 
    // as functionality might change based on the player's current state
    [Header("States")]
    public bool grounded;
    public bool usedDoubleJump;
    public bool usedDash;
    public bool airLauncing;
    public bool keepAirLauncing;
    public bool swinging;
    public bool colliding;
    public bool sliding;

    [Header("Sound Effects")]
    //Sound effect handling
    [SerializeField] private AudioSource jumpSound;
    [SerializeField] private AudioSource dashSound;
    [SerializeField] private AudioSource footstepSound;
    [SerializeField] private AudioSource grappleShotSound;
    [SerializeField] private AudioSource grappleHookSound;
    private System.Random randInt;

    [Header("Input")]
    public InputController inputController;

    [Header("Animation")]
    [SerializeField] private GameObject sprite;
    private PlayerAnimationController anim;

    public long startTime;
    public int lastMinuteSaved = 0;
    public ArrayList rooms;
    public ArrayList minimapExploredList;
    static public Dictionary<string, string[]> minimapExplored;
    public PauseMenu pm;
    public bool showPauseMenu = false;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        equip = GetComponent<PlayerEquipment>();
        inputController = GetComponent<InputController>();
        anim = sprite.GetComponent<PlayerAnimationController>();
        randInt = new System.Random();
        startTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
        rooms = new ArrayList();
        minimapExploredList = new ArrayList();
        int i = 1;
        while (true) {
            GameObject temp = GameObject.Find("Room" + (i < 10 ? ("0" + i.ToString()) : i.ToString()));
            if (!temp)
                break;
            rooms.Add(temp);
            i++;
        }
        pm = GameObject.FindObjectOfType(typeof(PauseMenu)) as PauseMenu;
        bool check = false;
        if (pm.getPaused()) {
            check = true;
        }
        try {
            if (System.IO.File.Exists(Application.persistentDataPath + "/TempSaveData.bin") && check) 
            {
                SaveLoad.loadData("TempSaveData");
                // Ensure we delete the TempSaveData.bin file after loading
                File.Delete(Application.persistentDataPath + "/TempSaveData.bin");
                showPauseMenu = true;
            }
        } catch (Exception e) {
            // Ignore the exception
        }
        StartCoroutine("FadeFromBlack");
    }

    void Update() {

        if (showPauseMenu) 
        {
            pm.setExternalPause(true);
            showPauseMenu = false;
        }

        if (System.IO.File.Exists(Application.persistentDataPath + "/" + SceneManager.GetActiveScene().name + ".bin")) 
        {

            new PlayerData().loadPlayerDataLevelChange(SceneManager.GetActiveScene().name);
            
            equip.setPowerUps(SaveLoad.powerups);

            for (int i = 0; i < SaveLoad.minimapExplored.Keys.Count(); i++) 
            {
                if (String.Equals(SaveLoad.minimapExplored.ElementAt(i).Key, SceneManager.GetActiveScene().name)) 
                {
                    for (int k = 0; k <SaveLoad.minimapExplored.ElementAt(i).Value.Count(); k++) {
                        GameObject tempRoom = (GameObject) GameObject.Find(SaveLoad.minimapExplored.ElementAt(i).Value[k]);
                        if (tempRoom) {
                            Color newColour = tempRoom.GetComponent<SpriteRenderer>().color;
                            newColour.a = 255;
                            tempRoom.GetComponent<SpriteRenderer>().color = newColour;

                            newColour = tempRoom.GetComponentsInChildren<SpriteRenderer>()[1].color;
                            newColour.a = 255;
                            tempRoom.GetComponentsInChildren<SpriteRenderer>()[1].color = newColour;
                        }
                    }
                }
            }
            minimapExplored = SaveLoad.minimapExplored;
            SaveLoad.clear();
            SaveLoad.loaded = false;

            File.Delete(Application.persistentDataPath + "/" + SceneManager.GetActiveScene().name + ".bin");
        }

        // Minimap fog of war checks \\
        for (int i = 0; i < rooms.Count; i++) {
            GameObject temp = (GameObject) rooms[i];
            GameObject tempRoom = (GameObject) GameObject.Find("MinimapUnexplored" + temp.name);
            GameObject tempCeiling = GameObject.Find(temp.name + "/Room Bound/Ceiling");
            GameObject tempFloor = GameObject.Find(temp.name + "/Room Bound/Floor");
            GameObject tempLeft = GameObject.Find(temp.name + "/Room Bound/Left Wall");
            GameObject tempRight = GameObject.Find(temp.name + "/Room Bound/Right Wall");
            if (tempRoom && 
                checkPlayerRoom(transform.position, tempCeiling.transform.position, tempFloor.transform.position, tempLeft.transform.position, tempRight.transform.position)) 
            { 
                Color newColour = tempRoom.GetComponent<SpriteRenderer>().color;
                newColour.a = 255;
                tempRoom.GetComponent<SpriteRenderer>().color = newColour;

                newColour = tempRoom.GetComponentsInChildren<SpriteRenderer>()[1].color;
                newColour.a = 255;
                tempRoom.GetComponentsInChildren<SpriteRenderer>()[1].color = newColour;
                if (!minimapExploredList.Contains(tempRoom.name))
                    minimapExploredList.Add(tempRoom.name);
            }
        }

        // Grounding \\
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

        anim.UpdateGroundedState(grounded);

        // Basic Movement \\

        float horizontalV = rb.velocity.x;
        float verticalV = rb.velocity.y;

        float targetHorizontalV = inputController.getHorizontalAxis() * movementSpeed;
        
        if (!airLauncing) {
            // Digital input doesn't have any smoothing, so this lerp smooths out the horizontal acceleration based on the target velocity
            horizontalV = Mathf.Lerp(rb.velocity.x, targetHorizontalV, Mathf.Abs(rb.velocity.x) < Mathf.Abs(targetHorizontalV) ? 15f * Time.deltaTime : 30f * Time.deltaTime);
        } else {
            horizontalV = Mathf.Lerp(horizontalV, 0, Time.deltaTime);
            // Cancel the air launching state if the player inputs movement, this lets them move straight out of an ability
            // that sends them into the air launching state, so they don't have to wait for the natural decceleration
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

        // Jump \\
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
                anim.PlayDoubleJump();
                usedDoubleJump = true;
                jumpSound.pitch = 1.3f;
            } else { // Jump animation is played here as otherwise the jump animation trigger is set when the player double jumps as well,
                     // and is not cleared until they hit the ground
                anim.PlayJump();
            }
        }

        // Movement normal is the normalised movement vector, which can be used to get the direction that the player is travelling in.
        if (horizontalV != 0) movementNormal = Mathf.Clamp(horizontalV, -1, 1);

        dashDirection = new Vector2(inputController.getHorizontalAxis(), inputController.getVerticalAxis());
        
        //Dash \\
        if (
            inputController.isDashActive() && 
            !usedDash && 
            !swinging &&
            dashDirection != Vector2.zero && 
            equip.GetPowerupState(PowerUps.Dash)
        ) {
            StartCoroutine(Dash(dashDirection));
        }

        // Wall Slide and Jump \\
        RaycastHit2D onWallLeft = Physics2D.Raycast(transform.position, -Vector2.right, .6f, LayerMask.GetMask("Geometry"));
        RaycastHit2D onWallRight = Physics2D.Raycast(transform.position, Vector2.right, .6f, LayerMask.GetMask("Geometry"));
        Debug.DrawRay(transform.position, -Vector2.right * .6f, Color.blue);
        Debug.DrawRay(transform.position, Vector2.right * .6f, Color.blue);
        if ((onWallLeft.collider != null || onWallRight.collider != null) && !grounded) {

            if (verticalV < 0 && Mathf.Abs(inputController.getHorizontalAxis()) > 0 && equip.GetPowerupState(PowerUps.WallJump)) {
                verticalV = Mathf.Clamp(verticalV, -maxFallSpeed / 5, 0);
                sliding = true;

                if (inputController.isJumpActive()) {
                    verticalV = jumpPower;
                    horizontalV = -inputController.getHorizontalAxis() * 15;
                    airLauncing = true;
                    StartCoroutine(KeepAirLaunch(.1f));

                    anim.PlayDoubleJump(); // NOTE: If we add another animation for the wall jump, it will be played from here
                }
            }
        } else sliding = false;

        anim.UpdateSliding(sliding);

        // Swinging \\
        if (!swinging)swingDirection = movementNormal > 0 ? new Vector2(.5f, .5f) : new Vector2(-.5f, .5f); // Swing direction is a vector rotated 45 degrees upwards from the current movement normal
        RaycastHit2D hit = Physics2D.Raycast(transform.position, swingDirection, 14.14f, LayerMask.GetMask("Geometry"));
        Debug.DrawRay(transform.position, swingDirection * 20, Color.green);

        if (hit.collider != null && !grounded && !sliding) {
            hookIndicatorRenderer.Display(hit.point);
            if (inputController.isSwingDown()  && equip.GetPowerupState(PowerUps.Swing)) { 
                StartCoroutine(StartSwing(hit.point, swingDirection.x > 0));
            }
        } else hookIndicatorRenderer.Remove();

        verticalV = Mathf.Clamp(verticalV, -maxFallSpeed, float.MaxValue); // Clamp the vertical velocity, otherwise it will increase forever, and makes the player fall way too fast
        rb.velocity = new Vector3(horizontalV, verticalV, 0);
        anim.UpdateVelocity(inputController.getHorizontalAxis(), verticalV);
        // Save Loading \\
        if (SaveLoad.loaded)
        {
            Debug.Log("Moving player to loaded position!");
            if (String.Compare(SceneManager.GetActiveScene().name, SaveLoad.activeSceneName) != 0) {
                GameObject fadeToBlackBox = GameObject.Find("Level_Transition");
                SpriteRenderer re = fadeToBlackBox.GetComponent<SpriteRenderer>();
                Color newColorFinal = re.color;
                newColorFinal.a = 1;
                newColorFinal.r = 0;
                newColorFinal.g = 0;
                newColorFinal.b = 0;
                re.color = newColorFinal;
                Canvas.ForceUpdateCanvases();
                SceneManager.LoadScene(SaveLoad.activeSceneName, LoadSceneMode.Single);
            }

            Physics2D.SyncTransforms();
            rb.position = new Vector2(SaveLoad.position[0], SaveLoad.position[1]);
            rb.transform.position = new Vector2(SaveLoad.position[0], SaveLoad.position[1]);

            Camera.main.transform.position = new Vector3(SaveLoad.cameraPosition[0], SaveLoad.cameraPosition[1], SaveLoad.cameraPosition[2]);

            GameObject.Find("Main Camera").GetComponent<CameraMovement>().SetTranslation(SaveLoad.translateX, SaveLoad.translateY);
            GameObject.Find("Main Camera").GetComponent<CameraMovement>().setCameraMinMax(SaveLoad.cameraMinMax[0], SaveLoad.cameraMinMax[1], SaveLoad.cameraMinMax[2], SaveLoad.cameraMinMax[3]);

            equip.setPowerUps(SaveLoad.powerups);

            for (int i = 0; i < SaveLoad.minimapExplored.Keys.Count(); i++) 
            {
                if (String.Equals(SaveLoad.minimapExplored.ElementAt(i).Key, SceneManager.GetActiveScene().name)) 
                {
                    for (int k = 0; k <SaveLoad.minimapExplored.ElementAt(i).Value.Count(); k++) {
                        GameObject tempRoom = (GameObject) GameObject.Find(SaveLoad.minimapExplored.ElementAt(i).Value[k]);
                        if (tempRoom) {
                            Color newColour = tempRoom.GetComponent<SpriteRenderer>().color;
                            newColour.a = 255;
                            tempRoom.GetComponent<SpriteRenderer>().color = newColour;

                            newColour = tempRoom.GetComponentsInChildren<SpriteRenderer>()[1].color;
                            newColour.a = 255;
                            tempRoom.GetComponentsInChildren<SpriteRenderer>()[1].color = newColour;
                        }
                    }
                }
            }
            minimapExplored = SaveLoad.minimapExplored;
            SaveLoad.clear();
            SaveLoad.loaded = false;
            pm.setPaused(false);
            pm.ResumeGame();
            
        }
        // Autosave checks
        long currentTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
        int minuteSinceStart = (int) Math.Floor((double)((currentTime - startTime) / 60000));
        // Game will auto-save once every 5 minutes (can be changed)
        if (minuteSinceStart % 5 == 0 && 
            /* Ensure we don't save as soon as the game loads */
            minuteSinceStart > 0 && 
            /* Ensure that we only save once per 5 minutes, not every frame at the 5 minute mark */
            minuteSinceStart != lastMinuteSaved) 
        {
            lastMinuteSaved = minuteSinceStart;
            // Call save function
            new PlayerData().popluateData("AutoSaveData");
        }
    }

    public Boolean checkPlayerRoom(Vector3 playerPos, Vector3 roomCeiling, Vector3 roomFloor, Vector3 roomLeft, Vector3 roomRight) {
        if ((playerPos.x <= roomRight.x && playerPos.x >= roomLeft.x) && (playerPos.y <= roomCeiling.y && playerPos.y >= roomFloor.y)) {
            return true;
        }
        return false;
    }

    // Coroutine that takes a given [direction] and perfoms the dash action in that direction
    IEnumerator Dash(Vector2 direction) {
        if (colliding) yield break;

        float t = Time.deltaTime;
        Vector2 startPos = transform.position;
        dashParticles.Play();
        dashSound.Play();
        cam.DashSmoothing();

        while (t < .1f) {
            rb.velocity = Vector2.zero;
            rb.gravityScale = 0;
            
            Vector2 nextPosition = Vector2.Lerp(startPos, startPos + (direction * 7), t * 20);
            RaycastHit2D collide = Physics2D.Raycast(transform.position, direction, Vector2.Distance(transform.position, nextPosition), LayerMask.GetMask("Geometry"));
            if (colliding) {
                dashSound.Stop(); 
                break;
            }
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

    // Maintains the air launching state for a given [time] period
    IEnumerator KeepAirLaunch(float time) {
        keepAirLauncing = true;
        yield return new WaitForSeconds(time);
        keepAirLauncing = false;
        airLauncing = false;
    }

    // Starts a swing with acceleration deadening and playing animations. Full swinging coroutine is started on completion 
    IEnumerator StartSwing(Vector2 point, bool direction) {
        if (colliding) yield break;    

        rb.velocity = Vector3.zero;
        rb.gravityScale = 0;

        hookRenderer.Attach(point);
        anim.PlayStartSwing(point);
        grappleShotSound.Play();

        yield return new WaitForSeconds(.1f);

        StartCoroutine(SwingAroundPoint(point, direction));
    }

    // Takes a [point] and a swing [direction] and will force the player through an arc that connects there position to the point.
    IEnumerator SwingAroundPoint(Vector2 point, bool direction) {

        //sfx
        grappleHookSound.Play();
        if (grappleShotSound.isPlaying) { grappleShotSound.Stop(); }

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
        anim.PlayStopSwing();
        yield break;
    }

    IEnumerator FadeFromBlack() 
    {
        GameObject fadeToBlackBox = GameObject.Find("Level_Transition");
        if (fadeToBlackBox) {
            fadeToBlackBox.SetActive(true);
            for (float alphaValue = 1.2f; alphaValue >= 0f; alphaValue = alphaValue - 0.1f) 
            {
                SpriteRenderer r = fadeToBlackBox.GetComponent<SpriteRenderer>();
                Color newColor = r.color;
                newColor.a = alphaValue;
                newColor.r = 0;
                newColor.g = 0;
                newColor.b = 0;
                r.color = newColor;
                yield return new WaitForSeconds(.05f);
            }
            SpriteRenderer re = fadeToBlackBox.GetComponent<SpriteRenderer>();
            Color newColorFinal = re.color;
            newColorFinal.a = 0;
            newColorFinal.r = 0;
            newColorFinal.g = 0;
            newColorFinal.b = 0;
            re.color = newColorFinal;
        }
    }

    // Forces the player to the collision state, interuptting any currently occuring movement abilities
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
    public long getStartTime() 
    {
        return this.startTime;
    }
    public ArrayList getExploredRooms() 
    {
        return this.minimapExploredList;
    }
    public Dictionary<string, string[]> getExploredRoomsWithSceneName() 
    { 
        if (minimapExplored != null)
            return minimapExplored;
        var retDict= new Dictionary<string, string[]>();
        for (int i = 0; i < getExploredRooms().Count; i++) 
        {
            ArrayList currentArray;
            if (retDict.ContainsKey(SceneManager.GetActiveScene().name))
                currentArray = new ArrayList(retDict[SceneManager.GetActiveScene().name]);
            else
                currentArray = new ArrayList();
            currentArray.Add((string) getExploredRooms().ToArray()[i]);
            string[] tempArray = ((IEnumerable)currentArray).Cast<object>()
                                 .Select(currentArray => currentArray.ToString())
                                 .ToArray();
            retDict.Remove(SceneManager.GetActiveScene().name);
            retDict.Add(SceneManager.GetActiveScene().name, tempArray);
        }
        return retDict;
    }
}
