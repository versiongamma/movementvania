using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dropper : MonoBehaviour
{

    public float speedDrop;
    public float speedResetting;
    public float timeExtended;
    private bool dropping; 

    private float timeLeft; 
    public bool dropperExtended;
    private Quaternion orientation; 

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

}
