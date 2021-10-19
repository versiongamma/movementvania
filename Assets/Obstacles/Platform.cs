using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    //public variables for the movement of the patroller
    public float speed;
    public float startWaitTime;
    private float waitTime;
    private Rigidbody2D rb;

    //variables for storing the list of move spots
    public List<Transform> PlatformPoints;
    private int spotindex = 0;


    // Start is called before the first frame update
    void Start(){
        waitTime = startWaitTime;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update(){
        transform.position = Vector3.MoveTowards(transform.position, PlatformPoints[spotindex].position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, PlatformPoints[spotindex].position) < 0.2f){
            if (waitTime <= 0){
                spotindex++;
                waitTime = startWaitTime;
            }
            else{
                waitTime -= Time.deltaTime;
            }
        }

        //keeps the enemy moving in a loop
        int listSize = PlatformPoints.Count;
        if (spotindex == listSize){
            spotindex = 0;
        }
    }
}

