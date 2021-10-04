using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Handles all camera movements
public class CameraMovement : MonoBehaviour {

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject startingBounds;
    private float cameraSmootingAmount = 20f, roomTransitionSmoothingAmount = 10f, dashSmoothinAmount = 5f, newX, newY;
    private bool translateX, translateY; // Booleans that allow the camera to be locked on certain axes. Makes the camera not jitter when the bounds aren't quite tight enough
    private float minX = float.MinValue, maxX = float.MaxValue, minY = float.MinValue, maxY = float.MaxValue; // The bounds of the camera, used to constrain it to a specific room

    void Start() {
        startingBounds.GetComponent<CameraBoundsHandler>().SetCameraBounds(this.GetComponent<Camera>());
    }
    
    void Update() {
        if (translateX) { newX = player.transform.position.x; }
        if (translateY) { newY = player.transform.position.y + 3; }

        // Terniaries make sure that the max clamp is greater than the min clamp, otherwise it freaks out.
        // This should only ever happen in single screen rooms, when the bounds are smaller than the camera's width or height
        newX = Mathf.Clamp(newX, minX, maxX > minX ? maxX : minX);
        newY = Mathf.Clamp(newY, minY, maxY > minY ? maxY : minY);

        transform.position = Vector3.Slerp(transform.position, new Vector3(newX, newY, -10), cameraSmootingAmount * Time.deltaTime);
    }

    // Transitions the camera to new bounds
    public void Transition(float minX, float minY, float maxX, float maxY) {
        float smoothing = cameraSmootingAmount; // Store the camera smoothing for later

        // Increase the camera smoothing for the room transition, this makes the camera transitioning
        // from one room to another more fluid looking, rather than having it snap to each room
        cameraSmootingAmount = roomTransitionSmoothingAmount; 

        this.minX = minX;
        this.minY = minY;
        this.maxX = maxX;
        this.maxY = maxY;

        // Set the smoothing to be reset after .3 seconds
        StartCoroutine(EndTransition(smoothing, .3f));
    }

    // Smooths out the camera for a dash
    public void DashSmoothing() {
        float smoothing = cameraSmootingAmount; 
        cameraSmootingAmount = dashSmoothinAmount; 

        StartCoroutine(EndTransition(smoothing, .3f));
    }

    // Sets what axes the camera can move on
    public void SetTranslation(bool x, bool y) {
        translateX = x;
        translateY = y;
    }

    // Resets the camera smoothing back to the default after a specified amount of time
    private IEnumerator EndTransition(float smoothing, float delta) {
        yield return new WaitForSeconds(delta);
        cameraSmootingAmount = smoothing;
    }

    public void setCameraMinMax(float minx, float maxx, float miny, float maxy) 
    {
        Transition(minx, miny, maxx, maxy);
    }

    public float[] getCameraMinMax()
    {
        return new float[] { this.minX, this.maxX, this.minY, this.maxY };
    }

    public bool getCameraTranslateX() {
        return this.translateX;
    }
    
    public bool getCameraTranslateY() {
        return this.translateY;
    }

    public GameObject getStartingBounds() {
        return this.startingBounds;
    }
}
