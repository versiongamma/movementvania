using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Basic class that handles door collisions, mainly calling functions from other classes when it is triggered.
public class DoorHandler : MonoBehaviour {
    private enum Side { Left, Right };

    [SerializeField] private Camera mainCamera;
    private GameObject player;
    [SerializeField] private GameObject newCameraBounds;
    [SerializeField] private Side doorSide;
    [SerializeField] private float travelDistance = 5; // In case the doors have to be further apart, the distance 
                                                       // it teleports the player can be lengthened

    private CameraMovement camMove;
    private CameraBoundsHandler boundsHandler;

    void Start() { 
        boundsHandler = newCameraBounds.GetComponent<CameraBoundsHandler>();
        player = GameObject.Find("Player");
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.name == "Player") {
            player.GetComponent<PlayerMovement>().EndMovement();
            float playerTranslateX = doorSide == Side.Left ? -travelDistance : doorSide == Side.Right ? travelDistance : 0;
            player.transform.position += new Vector3(playerTranslateX, 0, 0);
            boundsHandler.SetCameraBounds(mainCamera);
        }
    }
}
