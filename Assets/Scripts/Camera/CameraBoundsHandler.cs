using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Attached to each camera bounds, which have a box collider that determine where the camera can move
public class CameraBoundsHandler : MonoBehaviour {

    private CameraMovement cameraMovement;
    [SerializeField] private bool translateX, translateY; // Can be used to specify how the camera should move within these bounds

    public void SetCameraBounds(Camera camera) {
        cameraMovement = camera.GetComponent<CameraMovement>();
        float minX, minY, maxX, maxY;
        Collider2D col = this.GetComponent<Collider2D>();

        // Get the center of the camera bounds collider and add or subtract half the width (bounds.extents) to find
        // the +/- x edges, then add/subtract half camera width (size * aspectRatio / 2) to find the edges of the 
        // cameras position to stay within that bounding box
        minX = col.bounds.center.x - col.bounds.extents.x + (camera.orthographicSize * camera.aspect);
        maxX = col.bounds.center.x + col.bounds.extents.x - (camera.orthographicSize * camera.aspect);

        minY = col.bounds.center.y - col.bounds.extents.y + camera.orthographicSize;
        maxY = col.bounds.center.y + col.bounds.extents.y - camera.orthographicSize;

        cameraMovement.Transition(minX, minY, maxX, maxY);
        cameraMovement.SetTranslation(translateX, translateY);
    }

    public CameraBoundsHandler get()
    {
        return this;
    }
}
