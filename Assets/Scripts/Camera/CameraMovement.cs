using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject startingBounds;
    private float cameraSmootingAmount = 0.4f;
    private float roomTransitionSmoothingAmount = 28f;

    protected float minX = float.MinValue;
    protected float maxX = float.MaxValue;
    protected float minY = float.MinValue;
    protected float maxY = float.MaxValue;

    private float newX, newY;

    void Start() {
        startingBounds.GetComponent<CameraBoundsHandler>().SetCameraBounds(this.GetComponent<Camera>());
    }
    
    void FixedUpdate() {
        newX = Mathf.Clamp(player.transform.position.x, minX, maxX); 
        newY = Mathf.Clamp(player.transform.position.y + 6, minY, maxY); 


                                                                                             // Invert camera smoothing so higher 
                                                                                             // value means more smoothing
        transform.position = Vector3.Slerp(transform.position, new Vector3(newX, newY, -10), 1 / cameraSmootingAmount * 10);
    }

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
        StartCoroutine(ResetSmoothing(smoothing, .3f));
    }

    // Resets the camera smoothing back to the default after a specified amount of time
    private IEnumerator ResetSmoothing(float smoothing, float delta) {
        yield return new WaitForSeconds(delta);
        cameraSmootingAmount = smoothing;
    }
}
