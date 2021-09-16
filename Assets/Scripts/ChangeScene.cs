using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


//This script is used to load in a new scene 
public class ChangeScene : MonoBehaviour
{
    //the scene name is passed through into the function and then loaded
    //using SceneManager
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
