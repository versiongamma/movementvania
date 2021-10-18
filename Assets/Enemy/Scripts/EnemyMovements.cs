using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovements : MonoBehaviour
{
    public float direction;
    public float speed;

    // Update is called once per frame
    void Update(){
        transform.position += new Vector3(direction * speed * Time.deltaTime, 0, 0);
    }

    private void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.layer == 8 || other.gameObject.tag == "Door"){
            direction *= -1f;
        }

        if (other.gameObject.name == "Player") {
            other.gameObject.GetComponent<PlayerHealth>().TakeDamage(2);
        }
    }

    private void OnTriggerStay2D(Collider2D other){
        if (other.gameObject.name == "Player") {
            other.gameObject.GetComponent<PlayerHealth>().Knockback();
        }
    }
}
