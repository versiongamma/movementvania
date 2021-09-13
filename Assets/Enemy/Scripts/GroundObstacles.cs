using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundObstacles : MonoBehaviour {

    void OnCollisionEnter2D(Collision2D other) {
        Debug.Log("fdsjkabhfjkas");
        if (other.gameObject.name == "Player") {
            other.gameObject.GetComponent<PlayerHealth>().TakeDamage(20);
            Debug.Log("help me");
        }
    }
}
