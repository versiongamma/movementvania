using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FullscreenToggleController : MonoBehaviour
{

    public Toggle fullScreenToggle;
    public void Awake()
    {
        if (Screen.fullScreen == true)
        {
            fullScreenToggle.isOn = true;
        } 
        else
        {
            fullScreenToggle.isOn = false;
        }
    }
    public void setFullscreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }
}
