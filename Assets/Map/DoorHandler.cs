using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorHandler : MonoBehaviour {
    private enum Side { Left, Right };

    [SerializeField] private Camera camera;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject newCameraBounds;
    [SerializeField] private Side doorSide;

    private CameraMovement camMove;
    private CameraBoundsHandler boundsHandler;

    // Start is called before the first frame update
    void Start() { 
        boundsHandler = newCameraBounds.GetComponent<CameraBoundsHandler>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        float playerTranslateX = doorSide == Side.Left ? -3 : doorSide == Side.Right ? 3 : 0;
        player.transform.position += new Vector3(playerTranslateX, 0, 0);
        boundsHandler.SetCameraBounds(camera);
    }
}
