using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorHandler : MonoBehaviour {
    private enum Side { Left, Right };

    [SerializeField] private Camera mainCamera;
    private GameObject player;
    [SerializeField] private GameObject newCameraBounds;
    [SerializeField] private Side doorSide;

    private CameraMovement camMove;
    private CameraBoundsHandler boundsHandler;

    // Start is called before the first frame update
    void Start() { 
        boundsHandler = newCameraBounds.GetComponent<CameraBoundsHandler>();
        player = GameObject.Find("Player");
    }

    private void OnTriggerEnter2D(Collider2D other) {
        player.GetComponent<PlayerMovement>().EndMovement();
        float playerTranslateX = doorSide == Side.Left ? -5 : doorSide == Side.Right ? 5 : 0;
        player.transform.position += new Vector3(playerTranslateX, 0, 0);
        boundsHandler.SetCameraBounds(mainCamera);
    }
}
