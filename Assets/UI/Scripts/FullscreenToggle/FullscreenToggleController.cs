using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FullscreenToggleController : MonoBehaviour
{
    // Start is called before the first frame update
    public Toggle fullscreenToggle;
    public void updateFullscreen()
    {
        if (fullscreenToggle.enabled)
        {
            Screen.fullScreen = true;
        }
        else
        {
            Screen.fullScreen = false;
        }

        Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, Screen.fullScreen);
    }
}
