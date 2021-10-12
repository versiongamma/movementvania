using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Dropper : MonoBehaviour
{

    private Rigidbody2D rb;
    public float speed;
    public Transform RespawnPoint;


    // Use this for initialization
    void Start(){
        rb = GetComponent<Rigidbody2D>();
        
    }

    void OnTriggerEnter2D(Collider2D col){
        if (col.gameObject.name.Equals("Player")){

            rb.isKinematic = false;
            
        }
    }

    private void OnCollisionEnter2D(Collision2D col){
        if(col.collider.name == "Player"){
            Respawn();
        }
    }

    public void Respawn(){
        Instantiate(rb, RespawnPoint.position, RespawnPoint.rotation);
    }

}
