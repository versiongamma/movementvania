using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResolutionDropdownController : MonoBehaviour
{
    public Dropdown resDropdown;
    Resolution[] resolutions;

    void Start()
    {
        //Gets all possible screen resolutuions depending on monitor
        resolutions = Screen.resolutions;
        resDropdown.ClearOptions();

        List<string> dropdownOptions = new List<string>();

        int currentResolution = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            //Store resolution options in a string
            string res = resolutions[i].width + " x " + resolutions[i].height + 
                         " " + resolutions[i].refreshRate + "Hz";
            dropdownOptions.Add(res);

            //Automatically sets game resolution to current screen resolution.
            if (resolutions[i].width == Screen.width &&
                resolutions[i].height == Screen.height)
            {
                currentResolution = i;
            }
        }

        resDropdown.AddOptions(dropdownOptions);
        resDropdown.value = currentResolution;
        resDropdown.RefreshShownValue();
    }

    public void setRes(int resIndex)
    {
        //Sets resolution, using index of dropdown menu options to choose which one.
        Resolution res = resolutions[resIndex];
        Screen.SetResolution(res.width, res.height, Screen.fullScreen);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
