using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovements : MonoBehaviour
{
    public float direction = 1.5f;
    public float speed = 2f;
    private Vector2 startPosition;

    private void Start(){
        //startPosition is set equal to transform position to track the enemy
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update(){

        //Vector2 is used to set the enemy movement from left to right
        Vector2 v = startPosition;
        v.x += direction * Mathf.Sin(Time.time * speed);
        transform.position = v;
    }
}
