using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    Rigidbody2D rb;
    [SerializeField] private float movmentSpeed = 8;
    [SerializeField] private float jumpPower = 10;

    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update() {
        float horizontalV = Mathf.Lerp(rb.velocity.x, Input.GetAxis("Horizontal") * movmentSpeed, 0.05f);
        float verticalV = rb.velocity.y;

        // Needs grounding later
        if(Input.GetKeyDown(KeyCode.Space)) {
            verticalV += jumpPower;
        }

        rb.velocity = new Vector3(horizontalV, verticalV, 0);
    }
}
