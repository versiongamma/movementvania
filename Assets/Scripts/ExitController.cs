using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitController : MonoBehaviour
{

    //When class is called this method will execute 
    //Calls for the application to be exited
    public void Exit()
    {
        Application.Quit();
    }
}
