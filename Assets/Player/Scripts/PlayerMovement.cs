using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    Rigidbody2D rigidbody;
    [SerializeField] private float movmentSpeed = 6;
    [SerializeField] private float jumpPower = 10;

    // Start is called before the first frame update
    void Start() {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update() {
        float horizontalV = Input.GetAxis("Horizontal") * movmentSpeed;
        float verticalV = rigidbody.velocity.y;

        if(Input.GetKeyDown(KeyCode.Space)) {
            verticalV += jumpPower;
        }



        rigidbody.velocity = new Vector3(horizontalV, verticalV, 0);
    }
}
